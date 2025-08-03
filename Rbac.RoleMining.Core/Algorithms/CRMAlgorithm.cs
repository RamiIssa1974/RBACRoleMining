using System.Diagnostics;
using Rbac.RoleMining.Core.Models;

namespace Rbac.RoleMining.Core.Algorithms
{
    public class CRMAlgorithm
    {
        public RoleMiningResult Run(UserPermissionMatrix inputMatrix)
        {
            var stopwatch = Stopwatch.StartNew();

            int userCount = inputMatrix.Users.Count;
            int permCount = inputMatrix.Permissions.Count;
            bool[,] matrix = (bool[,])inputMatrix.Matrix.Clone();

            var result = new RoleMiningResult();
            int totalOnes = 0;
            for (int i = 0; i < userCount; i++)
                for (int j = 0; j < permCount; j++)
                    if (matrix[i, j]) totalOnes++;

            result.TotalPermissionCount = totalOnes;

            var roles = new List<Role>();
            var assignments = new List<RoleAssignment>();
            bool[,] covered = new bool[userCount, permCount];

            var permissionSets = ExtractAllPermissionSets(matrix, userCount, permCount);
            var candidates = GenerateIntersections(permissionSets, 3); // up to triplets

            int roleCounter = 1;

            foreach (var permSet in candidates)
            {
                var coveredUsers = new List<int>();

                for (int i = 0; i < userCount; i++)
                {
                    bool matches = true;
                    foreach (int p in permSet)
                    {
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

                if (coveredUsers.Count == 0)
                    continue;

                string roleName = $"Role{roleCounter++}";
                var role = new Role(roleName)
                {
                    PermissionIndices = new HashSet<int>(permSet)
                };
                roles.Add(role);

                foreach (int userIndex in coveredUsers)
                {
                    assignments.Add(new RoleAssignment(userIndex, roleName));
                    foreach (int p in permSet)
                    {
                        covered[userIndex, p] = true;
                    }
                }
            }

            // Add residual roles for uncovered permissions
            for (int i = 0; i < userCount; i++)
            {
                for (int j = 0; j < permCount; j++)
                {
                    if (matrix[i, j] && !covered[i, j])
                    {
                        string roleName = $"Role{roleCounter++}";
                        var role = new Role(roleName);
                        role.PermissionIndices.Add(j);
                        roles.Add(role);
                        assignments.Add(new RoleAssignment(i, roleName));
                        covered[i, j] = true;
                    }
                }
            }

            int coveredCount = 0;
            for (int i = 0; i < userCount; i++)
                for (int j = 0; j < permCount; j++)
                    if (matrix[i, j] && covered[i, j])
                        coveredCount++;

            stopwatch.Stop();

            result.Roles = roles;
            result.Assignments = assignments;
            result.CoveredPermissionCount = coveredCount;
            result.ExecutionTime = stopwatch.Elapsed;

            return result;
        }

        private List<HashSet<int>> ExtractAllPermissionSets(bool[,] matrix, int userCount, int permCount)
        {
            var sets = new List<HashSet<int>>();
            for (int i = 0; i < userCount; i++)
            {
                var permSet = new HashSet<int>();
                for (int j = 0; j < permCount; j++)
                    if (matrix[i, j])
                        permSet.Add(j);

                if (permSet.Count > 0)
                    sets.Add(permSet);
            }
            return sets;
        }

        private List<HashSet<int>> GenerateIntersections(List<HashSet<int>> sets, int maxGroupSize)
        {
            var result = new HashSet<string>();
            var intersections = new List<HashSet<int>>();

            int n = sets.Count;

            void Recurse(List<int> indices, int start)
            {
                if (indices.Count >= 2 && indices.Count <= maxGroupSize)
                {
                    var inter = new HashSet<int>(sets[indices[0]]);
                    for (int k = 1; k < indices.Count; k++)
                        inter.IntersectWith(sets[indices[k]]);

                    if (inter.Count > 0)
                    {
                        string key = string.Join(",", inter.OrderBy(x => x));
                        if (result.Add(key))
                            intersections.Add(inter);
                    }
                }

                if (indices.Count == maxGroupSize)
                    return;

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