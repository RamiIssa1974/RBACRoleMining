using System;
using System.Collections.Generic;

namespace Rbac.RoleMining.Core.Models
{
    public class RoleMiningResult
    {
        /// <summary>
        /// רשימת התפקידים שזוהו על ידי האלגוריתם
        /// </summary>
        public List<Role> Roles { get; set; } = new();

        /// <summary>
        /// מיפוי בין אינדקס משתמש לרשימת תפקידים ששויכו אליו
        /// </summary>
        public List<RoleAssignment> Assignments { get; set; } = new();


        /// <summary>
        /// כמה הרשאות מתוך המטריצה כוסו ע"י התפקידים
        /// </summary>
        public int CoveredPermissionCount { get; set; }

        /// <summary>
        /// סך ההרשאות שהיו במטריצה (כל ה־true)
        /// </summary>
        public int TotalPermissionCount { get; set; }

        /// <summary>
        /// זמן הריצה של האלגוריתם
        /// </summary>
        public TimeSpan ExecutionTime { get; set; }

        public double CoveragePercentage =>
            TotalPermissionCount == 0 ? 0 : 100.0 * CoveredPermissionCount / TotalPermissionCount;

        public int RoleCount => Roles.Count;
    }
}
