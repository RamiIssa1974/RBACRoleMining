using System.Diagnostics;
using Rbac.RoleMining.Core.Models;

namespace Rbac.RoleMining.Core.Algorithms
{
    /// <summary>
    /// CRMAlgorithm (Complete/Combinational Role Miner – simplified):
    /// Mines candidate roles by intersecting users' permission sets (pairs up to triplets),
    /// assigns those roles to matching users, and then adds residual single-permission roles
    /// to cover any remaining uncovered entries. Aims for full coverage, not strict optimality.
    /// </summary>
    public class CRMAlgorithm
    {
        /// <summary>
        /// Runs the role-mining pipeline:
        /// 1) Clone input matrix (safety).
        /// 2) Count total "1"s (ground-truth coverage target).
        /// 3) Build candidate roles via intersections of users' permission sets (size 2..maxGroupSize).
        /// 4) Assign each candidate role to all users that fully contain its permissions.
        /// 5) Add residual single-permission roles for any uncovered cells.
        /// 6) Report coverage and execution time.
        /// </summary>
        /// <param name="inputMatrix">Binary user-permission matrix (rows = users, cols = permissions).</param>
        /// <returns>RoleMiningResult containing roles, assignments, coverage stats and timing.</returns>
        public RoleMiningResult Run(UserPermissionMatrix inputMatrix)
        {
            // Measure end-to-end running time (for reporting/comparison)
            var stopwatch = Stopwatch.StartNew();

            // Extract sizes and clone input to avoid mutating the caller's matrix
            int userCount = inputMatrix.Users.Count;
            int permCount = inputMatrix.Permissions.Count;
            bool[,] matrix = (bool[,])inputMatrix.Matrix.Clone();

            var result = new RoleMiningResult();

            // Count the total number of 1's in UP (user-permission) – used as the coverage target
            int totalOnes = 0;
            for (int i = 0; i < userCount; i++)
                for (int j = 0; j < permCount; j++)
                    if (matrix[i, j]) totalOnes++;

            result.TotalPermissionCount = totalOnes;

            // Accumulators for output roles and user→role assignments
            var roles = new List<Role>();
            var assignments = new List<RoleAssignment>();

            // Tracks which (user,permission) cells are covered by formed roles
            bool[,] covered = new bool[userCount, permCount];

            // Step 1: extract each user's permission set (as indices)
            var permissionSets = ExtractAllPermissionSets(matrix, userCount, permCount);

            // Step 2: generate candidate roles by intersecting user permission sets
            // NOTE: intersections are limited to pairs and triplets to cap complexity
            var candidates = GenerateIntersections(permissionSets, 3); // up to triplets

            int roleCounter = 1;

            // Step 3: for each candidate permission set, find all users that "contain" it, then assign role
            foreach (var permSet in candidates)
            {
                var coveredUsers = new List<int>();

                // Check which users have all permissions in the candidate set
                for (int i = 0; i < userCount; i++)
                {
                    bool matches = true;
                    foreach (int p in permSet)
                    {
                        // If user i lacks any permission in the candidate, it's not a match
                        if (!matrix[i, p])
                        {
                            matches = false;
                            break;
                        }
                    }

                    if (matches)
                    {
                        coveredUsers.Add(i);
                    }
                }

                // Skip candidates that match no users (no value as a role)
                if (coveredUsers.Count == 0)
                    continue;

                // Create a role with the candidate permission set
                string roleName = $"Role{roleCounter++}";
                var role = new Role(roleName)
                {
                    PermissionIndices = new HashSet<int>(permSet)
                };
                roles.Add(role);

                // Assign this role to all matching users, and mark covered cells
                foreach (int userIndex in coveredUsers)
                {
                    assignments.Add(new RoleAssignment(userIndex, roleName));
                    foreach (int p in permSet)
                    {
                        covered[userIndex, p] = true;
                    }
                }
            }

            // Step 4: residual coverage
            // For any (user,permission) still uncovered, create a single-permission role and assign it.
            // This guarantees 100% coverage of the original UP matrix.
            for (int i = 0; i < userCount; i++)
            {
                for (int j = 0; j < permCount; j++)
                {
                    // If the original cell is 1 but has not yet been covered by any role
                    if (matrix[i, j] && !covered[i, j])
                    {
                        string roleName = $"Role{roleCounter++}";
                        var role = new Role(roleName);
                        role.PermissionIndices.Add(j); // single-permission role
                        roles.Add(role);

                        // Assign the new single-permission role to the user
                        assignments.Add(new RoleAssignment(i, roleName));

                        // Mark cell covered
                        covered[i, j] = true;
                    }
                }
            }

            // Compute final coverage count (# of original 1's that are now covered by some role)
            int coveredCount = 0;
            for (int i = 0; i < userCount; i++)
                for (int j = 0; j < permCount; j++)
                    if (matrix[i, j] && covered[i, j])
                        coveredCount++;

            stopwatch.Stop();

            // Prepare result payload
            result.Roles = roles;
            result.Assignments = assignments;
            result.CoveredPermissionCount = coveredCount;
            result.ExecutionTime = stopwatch.Elapsed;

            return result;
        }

        /// <summary>
        /// Extracts each user's set of permission indices (columns where matrix[i,j] == true).
        /// Only non-empty sets are returned.
        /// </summary>
        /// <param name="matrix">Binary user-permission matrix.</param>
        /// <param name="userCount">Number of users (rows).</param>
        /// <param name="permCount">Number of permissions (columns).</param>
        /// <returns>List of permission-index sets (one per user with at least one permission).</returns>
        private List<HashSet<int>> ExtractAllPermissionSets(bool[,] matrix, int userCount, int permCount)
        {
            var sets = new List<HashSet<int>>();
            for (int i = 0; i < userCount; i++)
            {
                var permSet = new HashSet<int>();
                for (int j = 0; j < permCount; j++)
                    if (matrix[i, j])
                        permSet.Add(j);

                // Only keep non-empty permission sets (users who have at least one permission)
                if (permSet.Count > 0)
                    sets.Add(permSet);
            }
            return sets;
        }

        /// <summary>
        /// Generates candidate permission sets by intersecting user permission sets.
        /// Intersections are computed for combinations of size 2..maxGroupSize.
        /// De-duplicates by string key of sorted indices.
        ///
        /// Note: Complexity grows combinatorially with maxGroupSize and #sets;
        /// limiting to 3 (triplets) keeps it practical for medium inputs.
        /// </summary>
        /// <param name="sets">Per-user permission sets.</param>
        /// <param name="maxGroupSize">Maximum combination size to intersect (e.g., 3).</param>
        /// <returns>List of distinct non-empty intersections as permission-index sets.</returns>
        private List<HashSet<int>> GenerateIntersections(List<HashSet<int>> sets, int maxGroupSize)
        {
            // Use a string key to avoid duplicate intersections (same permission set found multiple ways)
            var result = new HashSet<string>();
            var intersections = new List<HashSet<int>>();

            int n = sets.Count;

            // Backtracking over combinations of user indices
            void Recurse(List<int> indices, int start)
            {
                // Only materialize intersections for combo sizes in [2, maxGroupSize]
                if (indices.Count >= 2 && indices.Count <= maxGroupSize)
                {
                    // Compute intersection of the selected users' permission sets
                    var inter = new HashSet<int>(sets[indices[0]]);
                    for (int k = 1; k < indices.Count; k++)
                        inter.IntersectWith(sets[indices[k]]);

                    // Keep only non-empty intersections; deduplicate via deterministic key
                    if (inter.Count > 0)
                    {
                        string key = string.Join(",", inter.OrderBy(x => x));
                        if (result.Add(key))
                            intersections.Add(inter);
                    }
                }

                // Stop recursion if we already reached the maximum size
                if (indices.Count == maxGroupSize)
                    return;

                // Extend the combination
                for (int i = start; i < n; i++)
                {
                    indices.Add(i);
                    Recurse(indices, i + 1);
                    indices.RemoveAt(indices.Count - 1);
                }
            }

            Recurse(new List<int>(), 0);
            return intersections;
        }
    }
}
