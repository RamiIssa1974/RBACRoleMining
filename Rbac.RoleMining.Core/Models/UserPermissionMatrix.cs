using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rbac.RoleMining.Core.Models
{
    public class UserPermissionMatrix
    {
        public List<string> Users { get;  set; } = new();
        public List<string> Permissions { get;  set; } = new();
        public bool[,] Matrix { get;  set; }

        public static UserPermissionMatrix LoadFromCsv(string path)
        {
            var matrix = new UserPermissionMatrix();

            var userIndexMap = new Dictionary<string, int>();
            var permissionIndexMap = new Dictionary<string, int>();
            var entries = new List<(string user, string perm)>();

            foreach (var line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',');
                if (parts.Length != 2) continue;

                string user = parts[0].Trim();
                string perm = parts[1].Trim();

                entries.Add((user, perm));

                if (!userIndexMap.ContainsKey(user))
                {
                    userIndexMap[user] = userIndexMap.Count;
                    matrix.Users.Add(user);
                }

                if (!permissionIndexMap.ContainsKey(perm))
                {
                    permissionIndexMap[perm] = permissionIndexMap.Count;
                    matrix.Permissions.Add(perm);
                }
            }

            int userCount = matrix.Users.Count;
            int permissionCount = matrix.Permissions.Count;

            matrix.Matrix = new bool[userCount, permissionCount];

            foreach (var (user, perm) in entries)
            {
                int i = userIndexMap[user];
                int j = permissionIndexMap[perm];
                matrix.Matrix[i, j] = true;
            }

            return matrix;
        }

        public int GetUserIndex(string user) => Users.IndexOf(user);
        public int GetPermissionIndex(string perm) => Permissions.IndexOf(perm);
    }
}
