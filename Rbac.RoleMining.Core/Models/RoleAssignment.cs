namespace Rbac.RoleMining.Core.Models
{
    public class RoleAssignment
    {
        /// <summary>
        /// אינדקס המשתמש במטריצה
        /// </summary>
        public int UserIndex { get; set; }

        /// <summary>
        /// שם התפקיד ששויך למשתמש
        /// </summary>
        public string RoleName { get; set; }

        public RoleAssignment(int userIndex, string roleName)
        {
            UserIndex = userIndex;
            RoleName = roleName;
        }

        public override string ToString()
        {
            return $"User[{UserIndex}] → {RoleName}";
        }
    }
}
