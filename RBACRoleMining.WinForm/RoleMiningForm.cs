
using Rbac.RoleMining.Core.Algorithms;
using Rbac.RoleMining.Core.Models;
using Rbac.RoleMining.Core.UI;   // << add this (for ResultFormatter)
using System.Data;
using System.Linq;               // (safe to keep if you use LINQ anywhere)

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
                var matrix = UserPermissionMatrix.LoadFromCsv(path);

                var greedy = new GreedyAlgorithm();
                var result = greedy.Run(matrix);

                // Use the unified formatter (includes Assignments)
                txtResult.Clear();
                txtResult.Text = ResultFormatter.Format("Greedy Algorithm", result, matrix);
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

                // Use the unified formatter (includes Assignments)
                txtResult.Clear();
                txtResult.Text = ResultFormatter.Format("CRM Algorithm", result, matrix);
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

                var matrix = UserPermissionMatrix.LoadFromCsv(path);

                // Clone for each algorithm
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

                // If you want assignments here too, say the word and I'll add a compact section using ResultFormatter.
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

//using Rbac.RoleMining.Core.Algorithms;
//using Rbac.RoleMining.Core.Models;
//using System.Data;

//namespace RBACRoleMining.WinForm
//{
//    public partial class RoleMiningForm : Form
//    {
//        public RoleMiningForm()
//        {
//            InitializeComponent();
//        }

//        private void btnRunGreedy_Click(object sender, EventArgs e)
//        {
//            string path = txtFilePath.Text.Trim();

//            if (!File.Exists(path))
//            {
//                MessageBox.Show("File not found: " + path);
//                return;
//            }

//            try
//            {
//                // Load matrix
//                var matrix = UserPermissionMatrix.LoadFromCsv(path);

//                // Run Greedy
//                var greedy = new GreedyAlgorithm();
//                var result = greedy.Run(matrix);

//                // Show results
//                txtResult.Clear();
//                txtResult.AppendText($"[Greedy Algorithm]\r\n");
//                txtResult.AppendText($"Roles found: {result.RoleCount}\r\n");
//                txtResult.AppendText($"Coverage: {result.CoveragePercentage:F2}%\r\n");
//                txtResult.AppendText($"Execution Time: {result.ExecutionTime.TotalMilliseconds:F4} ms\r\n");
//                txtResult.AppendText("Roles:\r\n");

//                foreach (var role in result.Roles)
//                {
//                    var permNames = role.PermissionIndices.Select(i => matrix.Permissions[i]);
//                    txtResult.AppendText($"- {role.Name}: {string.Join(", ", permNames)}\r\n");
//                }

//                // NEW: Assignments per user (Ux: {RoleA, RoleB, ...})
//                txtResult.AppendText("\r\nAssignments:\r\n");
//                var assignmentsByUser = result.Assignments
//                    .GroupBy(a => a.UserIndex)
//                    .OrderBy(g => g.Key);

//                foreach (var g in assignmentsByUser)
//                {
//                    string userName = (g.Key >= 0 && g.Key < matrix.Users.Count)
//                        ? matrix.Users[g.Key]
//                        : $"User#{g.Key}";

//                    var roleNames = g.Select(a => a.RoleName)
//                                     .Distinct()
//                                     .OrderBy(n => n);
//                    txtResult.AppendText($"- {userName}: {{{string.Join(", ", roleNames)}}}\r\n");
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error: " + ex.Message);
//            }
//        }

//        private void btnBack_Click(object sender, EventArgs e)
//        {
//            Close();
//        }

//        private void btnRunCRM_Click(object sender, EventArgs e)
//        {
//            string path = txtFilePath.Text.Trim();

//            if (!File.Exists(path))
//            {
//                MessageBox.Show("File not found: " + path);
//                return;
//            }

//            try
//            {
//                var matrix = UserPermissionMatrix.LoadFromCsv(path);

//                var crm = new CRMAlgorithm();
//                var result = crm.Run(matrix);

//                txtResult.Clear();
//                txtResult.AppendText($"[CRM Algorithm]\r\n");
//                txtResult.AppendText($"Roles found: {result.RoleCount}\r\n");
//                txtResult.AppendText($"Coverage: {result.CoveragePercentage:F2}%\r\n");
//                txtResult.AppendText($"Execution Time: {result.ExecutionTime.TotalMilliseconds:F4} ms\r\n");
//                txtResult.AppendText("Roles:\r\n");

//                foreach (var role in result.Roles)
//                {
//                    var permNames = role.PermissionIndices.Select(i => matrix.Permissions[i]);
//                    txtResult.AppendText($"- {role.Name}: {string.Join(", ", permNames)}\r\n");
//                }

//                // NEW: Assignments per user (Ux: {RoleA, RoleB, ...})
//                txtResult.AppendText("\r\nAssignments:\r\n");
//                var assignmentsByUser = result.Assignments
//                    .GroupBy(a => a.UserIndex)
//                    .OrderBy(g => g.Key);

//                foreach (var g in assignmentsByUser)
//                {
//                    string userName = (g.Key >= 0 && g.Key < matrix.Users.Count)
//                        ? matrix.Users[g.Key]
//                        : $"User#{g.Key}";

//                    var roleNames = g.Select(a => a.RoleName)
//                                     .Distinct()
//                                     .OrderBy(n => n);
//                    txtResult.AppendText($"- {userName}: {{{string.Join(", ", roleNames)}}}\r\n");
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error: " + ex.Message);
//            }
//        }

//        private async void btnRunAndCompare_Click(object sender, EventArgs e)
//        {
//            string path = txtFilePath.Text.Trim();

//            if (!File.Exists(path))
//            {
//                MessageBox.Show("File not found: " + path);
//                return;
//            }

//            try
//            {
//                txtResult.Clear();
//                txtResult.AppendText("Running both algorithms, please wait...\r\n");

//                // Load once
//                var matrix = UserPermissionMatrix.LoadFromCsv(path);

//                // Clone for each algorithm
//                var matrixGreedy = new UserPermissionMatrix
//                {
//                    Users = new List<string>(matrix.Users),
//                    Permissions = new List<string>(matrix.Permissions),
//                    Matrix = (bool[,])matrix.Matrix.Clone()
//                };

//                var matrixCRM = new UserPermissionMatrix
//                {
//                    Users = new List<string>(matrix.Users),
//                    Permissions = new List<string>(matrix.Permissions),
//                    Matrix = (bool[,])matrix.Matrix.Clone()
//                };

//                RoleMiningResult? resultGreedy = null;
//                RoleMiningResult? resultCRM = null;

//                var taskGreedy = Task.Run(() =>
//                {
//                    var greedy = new GreedyAlgorithm();
//                    resultGreedy = greedy.Run(matrixGreedy);
//                });

//                var taskCRM = Task.Run(() =>
//                {
//                    var crm = new CRMAlgorithm();
//                    resultCRM = crm.Run(matrixCRM);
//                });

//                await Task.WhenAll(taskGreedy, taskCRM);

//                txtResult.Clear();
//                txtResult.AppendText("[Comparison Result]\r\n\r\n");

//                txtResult.AppendText($"[Greedy Algorithm]\r\n");
//                txtResult.AppendText($"Roles: {resultGreedy?.RoleCount}\r\n");
//                txtResult.AppendText($"Coverage: {resultGreedy?.CoveragePercentage:F2}%\r\n");
//                txtResult.AppendText($"Time: {resultGreedy?.ExecutionTime.TotalMilliseconds:F2} ms\r\n\r\n");

//                txtResult.AppendText($"[CRM Algorithm]\r\n");
//                txtResult.AppendText($"Roles: {resultCRM?.RoleCount}\r\n");
//                txtResult.AppendText($"Coverage: {resultCRM?.CoveragePercentage:F2}%\r\n");
//                txtResult.AppendText($"Time: {resultCRM?.ExecutionTime.TotalMilliseconds:F2} ms\r\n");

//                // (Optional) If you also want assignments in comparison, we can append similarly.
//                // Keeping comparison concise for now per your request.
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error: " + ex.Message);
//            }
//        }

//        private async void btnRunCompareAllCsvs_Click(object sender, EventArgs e)
//        {
//            string path = txtFilePath.Text.Trim();
//            if (!File.Exists(path))
//            {
//                MessageBox.Show("File not found: " + path);
//                return;
//            }

//            string folder = Path.GetDirectoryName(path);
//            string[] csvFiles = Directory.GetFiles(folder, "*.csv");

//            if (csvFiles.Length == 0)
//            {
//                MessageBox.Show("No CSV files found in folder.");
//                return;
//            }

//            txtResult.Clear();
//            txtResult.AppendText("Running comparison on all CSV files in folder...\r\n\r\n");

//            var comparisonLines = new List<string>();
//            comparisonLines.Add("File,  GreedyRoles,    GreedyTime(ms), CRMroles,   CRMTime(ms),    Coverage");

//            foreach (string file in csvFiles)
//            {
//                string fileName = Path.GetFileName(file);
//                txtResult.AppendText($"Processing {fileName}...\r\n");

//                try
//                {
//                    var matrix = UserPermissionMatrix.LoadFromCsv(file);

//                    // Duplicate matrix for both algorithms
//                    var greedyMatrix = new UserPermissionMatrix
//                    {
//                        Users = new List<string>(matrix.Users),
//                        Permissions = new List<string>(matrix.Permissions),
//                        Matrix = (bool[,])matrix.Matrix.Clone()
//                    };

//                    var crmMatrix = new UserPermissionMatrix
//                    {
//                        Users = new List<string>(matrix.Users),
//                        Permissions = new List<string>(matrix.Permissions),
//                        Matrix = (bool[,])matrix.Matrix.Clone()
//                    };

//                    RoleMiningResult? resultGreedy = null;
//                    RoleMiningResult? resultCRM = null;

//                    var taskGreedy = Task.Run(() =>
//                    {
//                        var greedy = new GreedyAlgorithm();
//                        resultGreedy = greedy.Run(greedyMatrix);
//                    });

//                    var taskCRM = Task.Run(() =>
//                    {
//                        var crm = new CRMAlgorithm();
//                        resultCRM = crm.Run(crmMatrix);
//                    });

//                    await Task.WhenAll(taskGreedy, taskCRM);

//                    comparisonLines.Add($"{fileName},   " +
//                        $"{resultGreedy?.RoleCount},    " +
//                        $"{resultGreedy?.ExecutionTime.TotalMilliseconds:F2},   " +
//                        $"{resultCRM?.RoleCount},   " +
//                        $"{resultCRM?.ExecutionTime.TotalMilliseconds:F2},   " +
//                        $"{resultCRM?.CoveragePercentage:F2}%");
//                }
//                catch (Exception ex)
//                {
//                    comparisonLines.Add($"{fileName},ERROR,{ex.Message.Replace(',', ';')},,,");
//                }
//            }

//            // Show summary
//            txtResult.AppendText("\r\nComparison Summary:\r\n\r\n");
//            foreach (var line in comparisonLines)
//            {
//                txtResult.AppendText(line + "\r\n");
//            }
//        }

//        private void btnBrowse_Click(object sender, EventArgs e)
//        {
//            using (OpenFileDialog openFileDialog = new OpenFileDialog())
//            {
//                openFileDialog.InitialDirectory = "C:\\RBAC";
//                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
//                openFileDialog.Title = "Select Permission Matrix CSV File";

//                if (openFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    txtFilePath.Text = openFileDialog.FileName;
//                }
//            }
//        }
//    }
//}
//**********************************************************************
//using Rbac.RoleMining.Core.Algorithms;
//using Rbac.RoleMining.Core.Models;
//using System.Data;

//namespace RBACRoleMining.WinForm
//{
//    public partial class RoleMiningForm : Form
//    {
//        public RoleMiningForm()
//        {
//            InitializeComponent();
//        }

//        private void btnRunGreedy_Click(object sender, EventArgs e)
//        {
//            string path = txtFilePath.Text.Trim();

//            if (!File.Exists(path))
//            {
//                MessageBox.Show("File not found: " + path);
//                return;
//            }

//            try
//            {
//                // שלב 1: טען מטריצה
//                var matrix = UserPermissionMatrix.LoadFromCsv(path);

//                // שלב 2: הרץ Greedy
//                var greedy = new GreedyAlgorithm();
//                var result = greedy.Run(matrix);

//                // שלב 3: הצג תוצאות
//                txtResult.Clear();
//                txtResult.AppendText($"[Greedy Algorithm]\r\n");
//                txtResult.AppendText($"Roles found: {result.RoleCount}\r\n");
//                txtResult.AppendText($"Coverage: {result.CoveragePercentage:F2}%\r\n");
//                txtResult.AppendText($"Execution Time: {result.ExecutionTime.TotalMilliseconds} ms\r\n");
//                txtResult.AppendText("Roles:\r\n");

//                foreach (var role in result.Roles)
//                {
//                    var permNames = role.PermissionIndices
//                        .Select(i => matrix.Permissions[i]);
//                    txtResult.AppendText($"- {role.Name}: {string.Join(", ", permNames)}\r\n");
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error: " + ex.Message);
//            }
//        }

//        private void btnBack_Click(object sender, EventArgs e)
//        {
//            Close();
//        }

//        private void btnRunCRM_Click(object sender, EventArgs e)
//        {
//            string path = txtFilePath.Text.Trim();

//            if (!File.Exists(path))
//            {
//                MessageBox.Show("File not found: " + path);
//                return;
//            }

//            try
//            {
//                var matrix = UserPermissionMatrix.LoadFromCsv(path);

//                var crm = new CRMAlgorithm();
//                var result = crm.Run(matrix);

//                txtResult.Clear();
//                txtResult.AppendText($"[CRM Algorithm]\r\n");
//                txtResult.AppendText($"Roles found: {result.RoleCount}\r\n");
//                txtResult.AppendText($"Coverage: {result.CoveragePercentage:F2}%\r\n");
//                txtResult.AppendText($"Execution Time: {result.ExecutionTime.TotalMilliseconds} ms\r\n");
//                txtResult.AppendText("Roles:\r\n");

//                foreach (var role in result.Roles)
//                {
//                    var permNames = role.PermissionIndices
//                        .Select(i => matrix.Permissions[i]);
//                    txtResult.AppendText($"- {role.Name}: {string.Join(", ", permNames)}\r\n");
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error: " + ex.Message);
//            }
//        }

//        private async void btnRunAndCompare_Click(object sender, EventArgs e)
//        {
//            string path = txtFilePath.Text.Trim();

//            if (!File.Exists(path))
//            {
//                MessageBox.Show("File not found: " + path);
//                return;
//            }

//            try
//            {
//                txtResult.Clear();
//                txtResult.AppendText("Running both algorithms, please wait...\r\n");

//                // טען מטריצה מקורית פעם אחת
//                var matrix = UserPermissionMatrix.LoadFromCsv(path);

//                // צור עותקים עבור כל אלגוריתם
//                var matrixGreedy = new UserPermissionMatrix
//                {
//                    Users = new List<string>(matrix.Users),
//                    Permissions = new List<string>(matrix.Permissions),
//                    Matrix = (bool[,])matrix.Matrix.Clone()
//                };

//                var matrixCRM = new UserPermissionMatrix
//                {
//                    Users = new List<string>(matrix.Users),
//                    Permissions = new List<string>(matrix.Permissions),
//                    Matrix = (bool[,])matrix.Matrix.Clone()
//                };

//                RoleMiningResult? resultGreedy = null;
//                RoleMiningResult? resultCRM = null;

//                var taskGreedy = Task.Run(() =>
//                {
//                    var greedy = new GreedyAlgorithm();
//                    resultGreedy = greedy.Run(matrixGreedy);
//                });

//                var taskCRM = Task.Run(() =>
//                {
//                    var crm = new CRMAlgorithm();
//                    resultCRM = crm.Run(matrixCRM);
//                });

//                await Task.WhenAll(taskGreedy, taskCRM);

//                txtResult.Clear();
//                txtResult.AppendText("[Comparison Result]\r\n\r\n");

//                txtResult.AppendText($"[Greedy Algorithm]\r\n");
//                txtResult.AppendText($"Roles: {resultGreedy?.RoleCount}\r\n");
//                txtResult.AppendText($"Coverage: {resultGreedy?.CoveragePercentage:F2}%\r\n");
//                txtResult.AppendText($"Time: {resultGreedy?.ExecutionTime.TotalMilliseconds:F2} ms\r\n\r\n");

//                txtResult.AppendText($"[CRM Algorithm]\r\n");
//                txtResult.AppendText($"Roles: {resultCRM?.RoleCount}\r\n");
//                txtResult.AppendText($"Coverage: {resultCRM?.CoveragePercentage:F2}%\r\n");
//                txtResult.AppendText($"Time: {resultCRM?.ExecutionTime.TotalMilliseconds:F2} ms\r\n");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error: " + ex.Message);
//            }
//        }
//        private async void btnRunCompareAllCsvs_Click(object sender, EventArgs e)
//        {
//            string path = txtFilePath.Text.Trim();
//            if (!File.Exists(path))
//            {
//                MessageBox.Show("File not found: " + path);
//                return;
//            }

//            string folder = Path.GetDirectoryName(path);
//            string[] csvFiles = Directory.GetFiles(folder, "*.csv");

//            if (csvFiles.Length == 0)
//            {
//                MessageBox.Show("No CSV files found in folder.");
//                return;
//            }

//            txtResult.Clear();
//            txtResult.AppendText("Running comparison on all CSV files in folder...\r\n\r\n");

//            var comparisonLines = new List<string>();
//            comparisonLines.Add("File,  GreedyRoles,    GreedyTime(ms), CRMroles,   CRMTime(ms),    Coverage");

//            foreach (string file in csvFiles)
//            {
//                string fileName = Path.GetFileName(file);
//                txtResult.AppendText($"Processing {fileName}...\r\n");

//                try
//                {
//                    var matrix = UserPermissionMatrix.LoadFromCsv(file);

//                    // Duplicate matrix for both algorithms
//                    var greedyMatrix = new UserPermissionMatrix
//                    {
//                        Users = new List<string>(matrix.Users),
//                        Permissions = new List<string>(matrix.Permissions),
//                        Matrix = (bool[,])matrix.Matrix.Clone()
//                    };

//                    var crmMatrix = new UserPermissionMatrix
//                    {
//                        Users = new List<string>(matrix.Users),
//                        Permissions = new List<string>(matrix.Permissions),
//                        Matrix = (bool[,])matrix.Matrix.Clone()
//                    };

//                    RoleMiningResult? resultGreedy = null;
//                    RoleMiningResult? resultCRM = null;

//                    var taskGreedy = Task.Run(() =>
//                    {
//                        var greedy = new GreedyAlgorithm();
//                        resultGreedy = greedy.Run(greedyMatrix);
//                    });

//                    var taskCRM = Task.Run(() =>
//                    {
//                        var crm = new CRMAlgorithm();
//                        resultCRM = crm.Run(crmMatrix);
//                    });

//                    await Task.WhenAll(taskGreedy, taskCRM);

//                    comparisonLines.Add($"{fileName},   " +
//                        $"{resultGreedy?.RoleCount},    " +
//                        $"{resultGreedy?.ExecutionTime.TotalMilliseconds:F2},   " +
//                        $"{resultCRM?.RoleCount},   " +
//                        $"{resultCRM?.ExecutionTime.TotalMilliseconds:F2},   " +
//                        $"{resultCRM?.CoveragePercentage:F2}%");
//                }
//                catch (Exception ex)
//                {
//                    comparisonLines.Add($"{fileName},ERROR,{ex.Message.Replace(',', ';')},,,");
//                }
//            }

//            // Show summary
//            txtResult.AppendText("\r\nComparison Summary:\r\n\r\n");
//            foreach (var line in comparisonLines)
//            {
//                txtResult.AppendText(line + "\r\n");
//            }
//        }

//        private void btnBrowse_Click(object sender, EventArgs e)
//        {
//            using (OpenFileDialog openFileDialog = new OpenFileDialog())
//            {
//                openFileDialog.InitialDirectory = "C:\\RBAC";
//                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
//                openFileDialog.Title = "Select Permission Matrix CSV File";

//                if (openFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    txtFilePath.Text = openFileDialog.FileName;
//                }
//            }
//        }
//    }
//}
