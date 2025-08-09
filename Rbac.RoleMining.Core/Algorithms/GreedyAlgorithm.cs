using System.Diagnostics;
using Rbac.RoleMining.Core.Models;

namespace Rbac.RoleMining.Core.Algorithms
{
    public class GreedyAlgorithm
    {
        /// <summary>
        /// מפעיל את אלגוריתם ה-Greedy על מטריצת המשתמשים-הרשאות ומחזיר את התוצאה.
        /// האלגוריתם בוחר בכל צעד את ה"Role" שמכסה את מספר ההרשאות הגדול ביותר שעדיין לא כוסו.
        /// </summary>
        public RoleMiningResult Run(UserPermissionMatrix inputMatrix)
        {
            var stopwatch = Stopwatch.StartNew(); // התחלת מדידת זמן ביצוע

            int userCount = inputMatrix.Users.Count;      // מספר המשתמשים
            int permCount = inputMatrix.Permissions.Count; // מספר ההרשאות
            bool[,] matrix = (bool[,])inputMatrix.Matrix.Clone(); // שיכפול המטריצה המקורית כדי לא לשנות אותה

            bool[,] uncovered = new bool[userCount, permCount]; // מטריצה של הרשאות שעדיין לא כוסו
            int totalOnes = 0; // ספירת סך כל ההרשאות הפעילות

            // העתקת הערכים המקוריים למטריצת uncovered + ספירת כמות ההרשאות
            for (int i = 0; i < userCount; i++)
            {
                for (int j = 0; j < permCount; j++)
                {
                    uncovered[i, j] = matrix[i, j];
                    if (matrix[i, j]) totalOnes++;
                }
            }

            // הכנת אובייקט התוצאה עם מספר ההרשאות הכולל
            var result = new RoleMiningResult
            {
                TotalPermissionCount = totalOnes
            };

            var roles = new List<Role>();                 // רשימת התפקידים שנוצרים
            var assignments = new List<RoleAssignment>(); // רשימת השיוכים של משתמשים לתפקידים

            // לולאה עד שכל ההרשאות כוסו או שאין מועמדים נוספים
            while (true)
            {
                // יצירת רשימת מועמדים (Permission Sets) מהרשאות שעדיין לא כוסו
                var candidates = GeneratePermissionSets(uncovered, userCount, permCount);

                // בחירת המועמד שמכסה את מירב ההרשאות
                var best = SelectBestCandidate(candidates, uncovered);

                // אם אין מועמד טוב - יציאה מהלולאה
                if (best == null || best.Users.Count == 0 || best.PermissionIndices.Count == 0)
                    break;

                // יצירת שם תפקיד חדש
                string roleName = $"Role{roles.Count + 1}";
                var role = new Role(roleName);
                foreach (int p in best.PermissionIndices)
                    role.PermissionIndices.Add(p); // הוספת ההרשאות לתפקיד

                roles.Add(role);

                // שיוך המשתמשים לתפקיד
                foreach (int userIndex in best.Users)
                    assignments.Add(new RoleAssignment(userIndex, roleName));

                // סימון ההרשאות של התפקיד הזה כ"מכוסות"
                foreach (int u in best.Users)
                    foreach (int p in best.PermissionIndices)
                        uncovered[u, p] = false;
            }

            stopwatch.Stop(); // סיום מדידת הזמן

            // מילוי תוצאות האלגוריתם
            result.Roles = roles;
            result.Assignments = assignments;
            result.CoveredPermissionCount = CountCovered(uncovered, matrix); // ספירת כמות ההרשאות שכוסו
            result.ExecutionTime = stopwatch.Elapsed;

            return result;
        }

        /// <summary>
        /// סופר כמה הרשאות מתוך המטריצה המקורית כוסו (כלומר אינן ב-uncovered).
        /// </summary>
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

        /// <summary>
        /// מחלקה פנימית שמייצגת מועמד לתפקיד: קבוצה של הרשאות ומשתמשים שמחזיקים בהן.
        /// </summary>
        private class CandidateRole
        {
            public HashSet<int> PermissionIndices { get; set; } = new();
            public List<int> Users { get; set; } = new();
        }

        /// <summary>
        /// יוצרת רשימת קבוצות הרשאות (Permission Sets) מתוך המטריצה של ההרשאות שעדיין לא כוסו.
        /// כל קבוצה מייצגת סט של הרשאות זהות שמשותפות לקבוצת משתמשים.
        /// </summary>
        private List<CandidateRole> GeneratePermissionSets(bool[,] uncovered, int userCount, int permCount)
        {
            var seen = new Dictionary<string, CandidateRole>();

            for (int i = 0; i < userCount; i++)
            {
                var perms = new List<int>();

                // איסוף כל ההרשאות שעדיין לא כוסו למשתמש זה
                for (int j = 0; j < permCount; j++)
                    if (uncovered[i, j])
                        perms.Add(j);

                if (perms.Count == 0) continue; // אין למשתמש הרשאות שעדיין לא כוסו

                // יצירת מפתח ייחודי לקבוצה (כדי לאחד משתמשים עם אותו סט הרשאות)
                string key = string.Join(",", perms);

                // אם זה סט חדש - יצירת מועמד חדש
                if (!seen.ContainsKey(key))
                {
                    seen[key] = new CandidateRole
                    {
                        PermissionIndices = new HashSet<int>(perms)
                    };
                }

                // הוספת המשתמש לרשימת המשתמשים של הקבוצה
                seen[key].Users.Add(i);
            }

            return seen.Values.ToList();
        }

        /// בוחרת את המועמד שמכסה את מספר ההרשאות הגדול ביותר שעדיין לא כוסו.
        
        private CandidateRole? SelectBestCandidate(List<CandidateRole> candidates, bool[,] uncovered)
        {
            int maxCover = 0;
            CandidateRole? best = null;

            foreach (var candidate in candidates)
            {
                int cover = 0;

                // חישוב כמה הרשאות חדשות מכסה המועמד
                foreach (int u in candidate.Users)
                {
                    foreach (int p in candidate.PermissionIndices)
                    {
                        if (uncovered[u, p])
                            cover++;
                    }
                }

                // שמירת המועמד הטוב ביותר
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
