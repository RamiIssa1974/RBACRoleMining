using RBACRoleMining.WinForm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBACRoleMining.WinForm
{
    public partial class DatasetGeneratorForm : Form
    {
        public DatasetGeneratorForm()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGenerateDataset_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtUsersCount.Text, out int users) ||
                !int.TryParse(txtPermissionsCount.Text, out int perms) ||
                !int.TryParse(txtRolesCount.Text, out int roles) ||
                !int.TryParse(txtMinRoleSize.Text, out int minSize) ||
                !int.TryParse(txtMaxRoleSize.Text, out int maxSize))
            {
                txtOutput.Text = "Invalid input. Please enter valid numbers.";
                return;
            }

            if (minSize < 3 || maxSize > 10)
            {
                txtOutput.Text = "Invalid role size range. Use format: 3-10";
                return;
            }

            var service = new DatasetService();
            var data = service.GenerateDataset(users, perms, roles, minSize, maxSize);

            txtOutput.Clear();
            txtOutput.AppendText($"user permission count: {data.Count}{Environment.NewLine}");
            txtOutput.AppendText($"actual density: {users / data.Count}{Environment.NewLine}");
            foreach (var (user, permission) in data)
            {
                txtOutput.AppendText($"{user},{permission}{Environment.NewLine}");
            }

            var path = txtOutputFilePath.Text;
            if (!string.IsNullOrEmpty(path) && data != null && data.Count > 0)
            {
                SaveDataToCsvFile(data, path);
            }
        }

        private void SaveDataToCsvFile(List<(string User, string Permission)> data, string path)
        {
            string folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            try
            {
                var lines = data.Select(d => $"{d.User},{d.Permission}");
                File.WriteAllLines(path, lines);
                MessageBox.Show("File saved successfully to:\n" + path, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving file:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DatasetGeneratorForm_Load(object sender, EventArgs e)
        {
            txtUsersCount.Text = "500";
            txtPermissionsCount.Text = "100";
            txtRolesCount.Text = "15";
            txtMinRoleSize.Text = "3"; // אחוזים
            txtMaxRoleSize.Text = "7"; // אחוזים
        }


        private void UpdateOutputFilePath()
        {
            if (int.TryParse(txtUsersCount.Text, out int users) &&
                int.TryParse(txtPermissionsCount.Text, out int perms) &&
                int.TryParse(txtRolesCount.Text, out int roles))
            {
                string filePath = $"C:\\RBAC\\data_{users}_{perms}_{roles}.csv";
                txtOutputFilePath.Text = filePath;                
            }
        }

        private void txtUsersCount_TextChanged(object sender, EventArgs e)
        {
            UpdateOutputFilePath();
        }

        private void txtPermissionsCount_TextChanged(object sender, EventArgs e)
        {
            UpdateOutputFilePath();
        }

        private void txtRolesCount_TextChanged(object sender, EventArgs e)
        {
            UpdateOutputFilePath();
        }
    }
}
