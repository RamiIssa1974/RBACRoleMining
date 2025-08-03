using System.Diagnostics;
using Rbac.RoleMining.Core.Models;

namespace Rbac.RoleMining.Core.Algorithms
{
    public class GreedyAlgorithm
    {
        public RoleMiningResult Run(UserPermissionMatrix inputMatrix)
        {
            var stopwatch = Stopwatch.StartNew();

            int userCount = inputMatrix.Users.Count;
            int permCount = inputMatrix.Permissions.Count;
            bool[,] matrix = (bool[,])inputMatrix.Matrix.Clone();

            bool[,] uncovered = new bool[userCount, permCount];
            int totalOnes = 0;

            for (int i = 0; i < userCount; i++)
            {
                for (int j = 0; j < permCount; j++)
                {
                    uncovered[i, j] = matrix[i, j];
                    if (matrix[i, j]) totalOnes++;
                }
            }

            var result = new RoleMiningResult
            {
                TotalPermissionCount = totalOnes
            };

            var roles = new List<Role>();
            var assignments = new List<RoleAssignment>();

            while (true)
            {
                var candidates = GeneratePermissionSets(uncovered, userCount, permCount);
                var best = SelectBestCandidate(candidates, uncovered);

                if (best == null || best.Users.Count == 0 || best.PermissionIndices.Count == 0)
                    break;

                string roleName = $"Role{roles.Count + 1}";
                var role = new Role(roleName);
                foreach (int p in best.PermissionIndices)
                    role.PermissionIndices.Add(p);

                roles.Add(role);

                foreach (int userIndex in best.Users)
                    assignments.Add(new RoleAssignment(userIndex, roleName));

                foreach (int u in best.Users)
                    foreach (int p in best.PermissionIndices)
                        uncovered[u, p] = false;
            }

            stopwatch.Stop();

            result.Roles = roles;
            result.Assignments = assignments;
            result.CoveredPermissionCount = CountCovered(uncovered, matrix);
            result.ExecutionTime = stopwatch.Elapsed;

            return result;
        }

        private int CountCovered(bool[,] uncovered, bool[,] original)
        {
            int count = 0;
            for (int i = 0; i < uncovered.GetLength(0); i++)
            {
                for (int j = 0; j < uncovered.GetLength(1); j++)
                {
                    if (original[i, j] && !uncovered[i, j])
                        count++;
                }
            }
            return count;
        }


        private class CandidateRole
        {
            public HashSet<int> PermissionIndices { get; set; } = new();
            public List<int> Users { get; set; } = new();
        }

        private List<CandidateRole> GeneratePermissionSets(bool[,] uncovered, int userCount, int permCount)
        {
            var seen = new Dictionary<string, CandidateRole>();

            for (int i = 0; i < userCount; i++)
            {
                var perms = new List<int>();
                for (int j = 0; j < permCount; j++)
                    if (uncovered[i, j])
                        perms.Add(j);

                if (perms.Count == 0) continue;

                string key = string.Join(",", perms);
                if (!seen.ContainsKey(key))
                {
                    seen[key] = new CandidateRole
                    {
                        PermissionIndices = new HashSet<int>(perms)
                    };
                }
                seen[key].Users.Add(i);
            }

            return seen.Values.ToList();
        }

        private CandidateRole? SelectBestCandidate(List<CandidateRole> candidates, bool[,] uncovered)
        {
            int maxCover = 0;
            CandidateRole? best = null;

            foreach (var candidate in candidates)
            {
                int cover = 0;
                foreach (int u in candidate.Users)
                {
                    foreach (int p in candidate.PermissionIndices)
                    {
                        if (uncovered[u, p])
                            cover++;
                    }
                }

                if (cover > maxCover)
                {
                    maxCover = cover;
                    best = candidate;
                }
            }

            return best;
        }
    }
}