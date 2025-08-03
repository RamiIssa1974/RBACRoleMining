using System.Collections.Generic;

namespace Rbac.RoleMining.Core.Models
{
    public class Role
    {
        public string Name { get; set; }

        /// <summary>
        /// A set of permission indices (not names!) referring to columns in the matrix.
        /// </summary>
        public HashSet<int> PermissionIndices { get; set; } = new();

        public Role(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name}: [{string.Join(", ", PermissionIndices)}]";
        }
    }
}
