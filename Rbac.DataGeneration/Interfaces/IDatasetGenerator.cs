using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.DataGeneration.Interfaces
{
    public interface IDatasetGenerator
    {
        /// <summary>
        /// Generate a synthetic dataset and return a list of user-permission pairs
        /// </summary>
        /// <param name="userCount"></param>
        /// <param name="permissionCount"></param>
        /// <param name="roleCount"></param>
        /// <param name="minRoleSize"></param>
        /// <param name="maxRoleSize"></param>
        /// <returns></returns>
        List<(string User, string Permission)> Generate(
            int userCount,
            int permissionCount,
            int roleCount,
            int minRoleSize,
            int maxRoleSize);
    }
}
