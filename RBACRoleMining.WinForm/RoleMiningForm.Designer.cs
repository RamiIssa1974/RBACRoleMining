namespace RBACRoleMining.WinForm
{
    partial class RoleMiningForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblFilePath = new Label();
            txtFilePath = new TextBox();
            btnRunGreedy = new Button();
            btnRunCRM = new Button();
            txtResult = new TextBox();
            btnBack = new Button();
            btnRunAndCompare = new Button();
            btnRunCompareAllCsvs = new Button();
            btnBrowse = new Button();
            SuspendLayout();
            // 
            // lblFilePath
            // 
            lblFilePath.AutoSize = true;
            lblFilePath.Location = new Point(46, 44);
            lblFilePath.Name = "lblFilePath";
            lblFilePath.Size = new Size(97, 15);
            lblFilePath.TabIndex = 0;
            lblFilePath.Text = "Dataset File Path:";
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(149, 41);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(285, 23);
            txtFilePath.TabIndex = 1;
            txtFilePath.Text = "C:\\\\RBAC\\\\data.csv";
            // 
            // btnRunGreedy
            // 
            btnRunGreedy.Image = Properties.Resources.algorithm1;
            btnRunGreedy.ImageAlign = ContentAlignment.MiddleLeft;
            btnRunGreedy.Location = new Point(12, 88);
            btnRunGreedy.Name = "btnRunGreedy";
            btnRunGreedy.Size = new Size(185, 64);
            btnRunGreedy.TabIndex = 2;
            btnRunGreedy.Text = "Run Greedy Algorithm";
            btnRunGreedy.TextAlign = ContentAlignment.MiddleRight;
            btnRunGreedy.UseVisualStyleBackColor = true;
            btnRunGreedy.Click += btnRunGreedy_Click;
            // 
            // btnRunCRM
            // 
            btnRunCRM.Image = Properties.Resources.algorithm1;
            btnRunCRM.ImageAlign = ContentAlignment.MiddleLeft;
            btnRunCRM.Location = new Point(203, 88);
            btnRunCRM.Name = "btnRunCRM";
            btnRunCRM.Size = new Size(177, 64);
            btnRunCRM.TabIndex = 2;
            btnRunCRM.Text = "Run CRM Algorithm";
            btnRunCRM.TextAlign = ContentAlignment.MiddleRight;
            btnRunCRM.UseVisualStyleBackColor = true;
            btnRunCRM.Click += btnRunCRM_Click;
            // 
            // txtResult
            // 
            txtResult.Location = new Point(46, 158);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.ScrollBars = ScrollBars.Vertical;
            txtResult.Size = new Size(726, 213);
            txtResult.TabIndex = 3;
            // 
            // btnBack
            // 
            btnBack.AccessibleDescription = "Back To Main Form";
            btnBack.Image = Properties.Resources.back_arrow;
            btnBack.ImageAlign = ContentAlignment.MiddleRight;
            btnBack.Location = new Point(657, 377);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(115, 72);
            btnBack.TabIndex = 4;
            btnBack.TabStop = false;
            btnBack.Text = "Back";
            btnBack.TextAlign = ContentAlignment.MiddleLeft;
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnRunAndCompare
            // 
            btnRunAndCompare.Image = Properties.Resources.compareSmall;
            btnRunAndCompare.ImageAlign = ContentAlignment.MiddleLeft;
            btnRunAndCompare.Location = new Point(386, 88);
            btnRunAndCompare.Name = "btnRunAndCompare";
            btnRunAndCompare.Size = new Size(168, 64);
            btnRunAndCompare.TabIndex = 5;
            btnRunAndCompare.Text = "Run and Compare";
            btnRunAndCompare.TextAlign = ContentAlignment.MiddleRight;
            btnRunAndCompare.UseVisualStyleBackColor = true;
            btnRunAndCompare.Click += btnRunAndCompare_Click;
            // 
            // btnRunCompareAllCsvs
            // 
            btnRunCompareAllCsvs.Image = Properties.Resources.compareAllSmall;
            btnRunCompareAllCsvs.ImageAlign = ContentAlignment.MiddleLeft;
            btnRunCompareAllCsvs.Location = new Point(560, 94);
            btnRunCompareAllCsvs.Name = "btnRunCompareAllCsvs";
            btnRunCompareAllCsvs.Size = new Size(212, 58);
            btnRunCompareAllCsvs.TabIndex = 6;
            btnRunCompareAllCsvs.Text = "Run and Compare All CSVs";
            btnRunCompareAllCsvs.TextAlign = ContentAlignment.MiddleRight;
            btnRunCompareAllCsvs.UseVisualStyleBackColor = true;
            btnRunCompareAllCsvs.Click += btnRunCompareAllCsvs_Click;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(440, 41);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(35, 23);
            btnBrowse.TabIndex = 7;
            btnBrowse.Text = "...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // RoleMiningForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 461);
            Controls.Add(btnBrowse);
            Controls.Add(btnRunCompareAllCsvs);
            Controls.Add(btnRunAndCompare);
            Controls.Add(btnBack);
            Controls.Add(txtResult);
            Controls.Add(btnRunCRM);
            Controls.Add(btnRunGreedy);
            Controls.Add(txtFilePath);
            Controls.Add(lblFilePath);
            Name = "RoleMiningForm";
            Text = "Role Mining Form";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFilePath;
        private TextBox txtFilePath;
        private Button btnRunGreedy;
        private Button btnRunCRM;
        private TextBox txtResult;
        private Button btnBack;
        private Button btnRunAndCompare;
        private Button btnRunCompareAllCsvs;
        private Button btnBrowse;
    }
}