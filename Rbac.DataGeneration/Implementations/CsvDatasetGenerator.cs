using Rbac.DataGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.DataGeneration.Implementations
{

    public class CsvDatasetGenerator : IDatasetGenerator
    {
        private readonly Random _random = new();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userCount"></param>
        /// <param name="permissionCount"></param>
        /// <param name="roleCount"></param>
        /// <param name="minRoleSize"></param>
        /// <param name="maxRoleSize"></param>
        /// <returns></returns>
        public List<(string User, string Permission)> Generate(int userCount,
                                                               int permissionCount,
                                                               int roleCount,
                                                               int minRoleSize,
                                                               int maxRoleSize)
        {
            // יצירת רשימת הרשאות
            var permissions = Enumerable.Range(1, permissionCount)
                                        .Select(i => $"P{i}")
                                        .ToList();

            // יצירת תפקידים – כל תפקיד הוא קבוצה של הרשאות
            var roles = new List<HashSet<string>>();
            for (int i = 0; i < roleCount; i++)
            {
                var size = _random.Next(minRoleSize, maxRoleSize + 1);
                var rolePerms = permissions.OrderBy(_ => _random.Next()).Take(size).ToHashSet();
                roles.Add(rolePerms);
            }

            // הקצאת תפקידים למשתמשים
            var userPermissions = new List<(string, string)>();
            for (int u = 1; u <= userCount; u++)
            {
                var user = $"user{u}";
                var assignedRoles = roles.OrderBy(_ => _random.Next())
                                         .Take(_random.Next(1, 4)) // כל משתמש יקבל 1–3 תפקידים
                                         .ToList();

                foreach (var role in assignedRoles)
                {
                    foreach (var perm in role)
                    {
                        userPermissions.Add((user, perm));
                    }
                }
            }

            return userPermissions.Distinct().ToList(); // הסרת כפילויות
        }
    }
}
