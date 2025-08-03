using Rbac.RoleMining.Core.Algorithms;
using Rbac.RoleMining.Core.Models;
using System.Data;

namespace RBACRoleMining.WinForm
{
    public partial class RoleMiningForm : Form
    {
        public RoleMiningForm()
        {
            InitializeComponent();
        }

        private void btnRunGreedy_Click(object sender, EventArgs e)
        {
            string path = txtFilePath.Text.Trim();

            if (!File.Exists(path))
            {
                MessageBox.Show("File not found: " + path);
                return;
            }

            try
            {
                // שלב 1: טען מטריצה
                var matrix = UserPermissionMatrix.LoadFromCsv(path);

                // שלב 2: הרץ Greedy
                var greedy = new GreedyAlgorithm();
                var result = greedy.Run(matrix);

                // שלב 3: הצג תוצאות
                txtResult.Clear();
                txtResult.AppendText($"[Greedy Algorithm]\r\n");
                txtResult.AppendText($"Roles found: {result.RoleCount}\r\n");
                txtResult.AppendText($"Coverage: {result.CoveragePercentage:F2}%\r\n");
                txtResult.AppendText($"Execution Time: {result.ExecutionTime.TotalMilliseconds} ms\r\n");
                txtResult.AppendText("Roles:\r\n");

                foreach (var role in result.Roles)
                {
                    var permNames = role.PermissionIndices
                        .Select(i => matrix.Permissions[i]);
                    txtResult.AppendText($"- {role.Name}: {string.Join(", ", permNames)}\r\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRunCRM_Click(object sender, EventArgs e)
        {
            string path = txtFilePath.Text.Trim();

            if (!File.Exists(path))
            {
                MessageBox.Show("File not found: " + path);
                return;
            }

            try
            {
                var matrix = UserPermissionMatrix.LoadFromCsv(path);

                var crm = new CRMAlgorithm();
                var result = crm.Run(matrix);

                txtResult.Clear();
                txtResult.AppendText($"[CRM Algorithm]\r\n");
                txtResult.AppendText($"Roles found: {result.RoleCount}\r\n");
                txtResult.AppendText($"Coverage: {result.CoveragePercentage:F2}%\r\n");
                txtResult.AppendText($"Execution Time: {result.ExecutionTime.TotalMilliseconds} ms\r\n");
                txtResult.AppendText("Roles:\r\n");

                foreach (var role in result.Roles)
                {
                    var permNames = role.PermissionIndices
                        .Select(i => matrix.Permissions[i]);
                    txtResult.AppendText($"- {role.Name}: {string.Join(", ", permNames)}\r\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void btnRunAndCompare_Click(object sender, EventArgs e)
        {
            string path = txtFilePath.Text.Trim();

            if (!File.Exists(path))
            {
                MessageBox.Show("File not found: " + path);
                return;
            }

            try
            {
                txtResult.Clear();
                txtResult.AppendText("Running both algorithms, please wait...\r\n");

                // טען מטריצה מקורית פעם אחת
                var matrix = UserPermissionMatrix.LoadFromCsv(path);

                // צור עותקים עבור כל אלגוריתם
                var matrixGreedy = new UserPermissionMatrix
                {
                    Users = new List<string>(matrix.Users),
                    Permissions = new List<string>(matrix.Permissions),
                    Matrix = (bool[,])matrix.Matrix.Clone()
                };

                var matrixCRM = new UserPermissionMatrix
                {
                    Users = new List<string>(matrix.Users),
                    Permissions = new List<string>(matrix.Permissions),
                    Matrix = (bool[,])matrix.Matrix.Clone()
                };

                RoleMiningResult? resultGreedy = null;
                RoleMiningResult? resultCRM = null;

                var taskGreedy = Task.Run(() =>
                {
                    var greedy = new GreedyAlgorithm();
                    resultGreedy = greedy.Run(matrixGreedy);
                });

                var taskCRM = Task.Run(() =>
                {
                    var crm = new CRMAlgorithm();
                    resultCRM = crm.Run(matrixCRM);
                });

                await Task.WhenAll(taskGreedy, taskCRM);

                txtResult.Clear();
                txtResult.AppendText("[Comparison Result]\r\n\r\n");

                txtResult.AppendText($"[Greedy Algorithm]\r\n");
                txtResult.AppendText($"Roles: {resultGreedy?.RoleCount}\r\n");
                txtResult.AppendText($"Coverage: {resultGreedy?.CoveragePercentage:F2}%\r\n");
                txtResult.AppendText($"Time: {resultGreedy?.ExecutionTime.TotalMilliseconds:F2} ms\r\n\r\n");

                txtResult.AppendText($"[CRM Algorithm]\r\n");
                txtResult.AppendText($"Roles: {resultCRM?.RoleCount}\r\n");
                txtResult.AppendText($"Coverage: {resultCRM?.CoveragePercentage:F2}%\r\n");
                txtResult.AppendText($"Time: {resultCRM?.ExecutionTime.TotalMilliseconds:F2} ms\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private async void btnRunCompareAllCsvs_Click(object sender, EventArgs e)
        {
            string path = txtFilePath.Text.Trim();
            if (!File.Exists(path))
            {
                MessageBox.Show("File not found: " + path);
                return;
            }

            string folder = Path.GetDirectoryName(path);
            string[] csvFiles = Directory.GetFiles(folder, "*.csv");

            if (csvFiles.Length == 0)
            {
                MessageBox.Show("No CSV files found in folder.");
                return;
            }

            txtResult.Clear();
            txtResult.AppendText("Running comparison on all CSV files in folder...\r\n\r\n");

            var comparisonLines = new List<string>();
            comparisonLines.Add("File,  GreedyRoles,    GreedyTime(ms), CRMroles,   CRMTime(ms),    Coverage");

            foreach (string file in csvFiles)
            {
                string fileName = Path.GetFileName(file);
                txtResult.AppendText($"Processing {fileName}...\r\n");

                try
                {
                    var matrix = UserPermissionMatrix.LoadFromCsv(file);

                    // Duplicate matrix for both algorithms
                    var greedyMatrix = new UserPermissionMatrix
                    {
                        Users = new List<string>(matrix.Users),
                        Permissions = new List<string>(matrix.Permissions),
                        Matrix = (bool[,])matrix.Matrix.Clone()
                    };

                    var crmMatrix = new UserPermissionMatrix
                    {
                        Users = new List<string>(matrix.Users),
                        Permissions = new List<string>(matrix.Permissions),
                        Matrix = (bool[,])matrix.Matrix.Clone()
                    };

                    RoleMiningResult? resultGreedy = null;
                    RoleMiningResult? resultCRM = null;

                    var taskGreedy = Task.Run(() =>
                    {
                        var greedy = new GreedyAlgorithm();
                        resultGreedy = greedy.Run(greedyMatrix);
                    });

                    var taskCRM = Task.Run(() =>
                    {
                        var crm = new CRMAlgorithm();
                        resultCRM = crm.Run(crmMatrix);
                    });

                    await Task.WhenAll(taskGreedy, taskCRM);

                    comparisonLines.Add($"{fileName},   " +
                        $"{resultGreedy?.RoleCount},    " +
                        $"{resultGreedy?.ExecutionTime.TotalMilliseconds:F2},   " +
                        $"{resultCRM?.RoleCount},   " +
                        $"{resultCRM?.ExecutionTime.TotalMilliseconds:F2},   " +
                        $"{resultCRM?.CoveragePercentage:F2}%");
                }
                catch (Exception ex)
                {
                    comparisonLines.Add($"{fileName},ERROR,{ex.Message.Replace(',', ';')},,,");
                }
            }

            // Show summary
            txtResult.AppendText("\r\nComparison Summary:\r\n\r\n");
            foreach (var line in comparisonLines)
            {
                txtResult.AppendText(line + "\r\n");
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\RBAC";
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.Title = "Select Permission Matrix CSV File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;
                }
            }
        }
    }
}
