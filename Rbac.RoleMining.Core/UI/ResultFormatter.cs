using System;
using System.Data;
using System.Linq;
using System.Text;
using Rbac.RoleMining.Core.Models;

namespace Rbac.RoleMining.Core.UI
{
    public static class ResultFormatter
    {
        /// <summary>
        /// יוצר טקסט תקין להצגה: כותרת, סיכום, רשימת Roles והרשאותיהם, ובסוף Assignments לפי משתמש.
        /// ללא שינוי לוגיקה – אך כולל שמות משתמשים/הרשאות מתוך ה-UPM.
        /// </summary>
        public static string Format(string algorithmTitle, RoleMiningResult result, UserPermissionMatrix upm)
        {
            var sb = new StringBuilder();

            // Header
            sb.AppendLine($"[{algorithmTitle}]");
            sb.AppendLine($"Roles found: {result.Roles?.Count ?? 0}");

            double coverage = (result.TotalPermissionCount == 0)
                ? 100.0
                : 100.0 * (result.CoveredPermissionCount) / result.TotalPermissionCount;

            sb.AppendLine($"Coverage: {coverage:F2}%");
            sb.AppendLine($"Execution Time: {result.ExecutionTime.TotalMilliseconds:F4} ms");

            // Roles block
            sb.AppendLine("Roles:");
            if (result.Roles != null && result.Roles.Count > 0)
            {
                foreach (var role in result.Roles)
                {
                    // תרגום אינדקסי הרשאות לשמות ההרשאות מתוך המטריצה
                    var permNames = role.PermissionIndices
                                        .OrderBy(i => i)
                                        .Select(i => SafeGet(upm.Permissions, i, $"P{i + 1}"));
                    sb.AppendLine($"- {role.Name}: {string.Join(", ", permNames)}");
                }
            }
            else
            {
                sb.AppendLine("- (no roles)");
            }

            // Assignments per user
            sb.AppendLine();
            sb.AppendLine("Assignments (by user):");

            // בניית מילון משתמש→רשימת Roles
            var byUser = upm.Users
                            .Select((u, idx) => new { UserName = u, Index = idx })
                            .ToDictionary(x => x.Index, x => new System.Collections.Generic.List<string>());

            if (result.Assignments != null)
            {
                foreach (var a in result.Assignments)
                {
                    if (byUser.TryGetValue(a.UserIndex, out var list))
                        list.Add(a.RoleName);
                }
            }

            // הדפסה לפי סדר משתמשים
            for (int i = 0; i < upm.Users.Count; i++)
            {
                var userName = SafeGet(upm.Users, i, $"U{i + 1}");
                var rolesForUser = byUser[i].Distinct().OrderBy(x => x).ToList();
                var rolesText = rolesForUser.Count > 0 ? string.Join(", ", rolesForUser) : "(none)";
                sb.AppendLine($"{userName}: {{{rolesText}}}");
            }

            return sb.ToString();
        }

        private static string SafeGet(System.Collections.Generic.IList<string> list, int index, string fallback)
        {
            if (list == null) return fallback;
            if (index < 0 || index >= list.Count) return fallback;
            return list[index] ?? fallback;
        }
    }
}
