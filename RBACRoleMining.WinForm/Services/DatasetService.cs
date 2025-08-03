using Rbac.DataGeneration.Implementations;
using Rbac.DataGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBACRoleMining.WinForm.Services
{
    public class DatasetService
    {
        private readonly IDatasetGenerator _generator;

        public DatasetService()
        {
            // כאן בעתיד נוכל להחליף את המימוש ב-SQL או JSON וכו'
            _generator = new CsvDatasetGenerator();
        }

        public List<(string User, string Permission)> GenerateDataset(
            int userCount, int permissionCount, int roleCount,
            int minRoleSize, int maxRoleSize)
        {
            return _generator.Generate(userCount, permissionCount, roleCount, minRoleSize, maxRoleSize);
        }
    }
}
