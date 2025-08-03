using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace RBACRoleMining.WinForms
{
    public partial class DatasetGeneratorForm : Form
    {
        public DatasetGeneratorForm()
        {
            InitializeComponent();
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtOutput_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnGenerateDataset_Click(object sender, EventArgs e)
        {
       //     if (!int.TryParse(txtUsersCount.Text, out int users) ||
       //!int.TryParse(txtPermissionsCount.Text, out int perms) ||
       //!int.TryParse(txtRolesCount.Text, out int roles) ||
       //string.IsNullOrWhiteSpace(txtRoleSizeRange.Text))
       //     {
       //         txtOutput.Text = "Invalid input. Please enter valid numbers.";
       //         return;
       //     }

       //     var parts = txtRoleSizeRange.Text.Split('-');
       //     if (parts.Length != 2 || !int.TryParse(parts[0], out int minSize) || !int.TryParse(parts[1], out int maxSize))
       //     {
       //         txtOutput.Text = "Invalid role size range. Use format: 3-10";
       //         return;
       //     }

       //     var service = new DatasetService();
       //     var data = service.GenerateDataset(users, perms, roles, minSize, maxSize);

       //     txtOutput.Clear();
       //     foreach (var (user, permission) in data)
       //     {
       //         txtOutput.AppendText($"{user},{permission}{Environment.NewLine}");
       //     }
        }
    }
}
