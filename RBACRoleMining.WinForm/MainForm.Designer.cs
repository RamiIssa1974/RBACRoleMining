namespace RBACRoleMining.WinForm
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            btnOpenDatasetGenerator = new Button();
            btnExit = new Button();
            btnRunAlgorithms = new Button();
            SuspendLayout();
            // 
            // btnOpenDatasetGenerator
            // 
            btnOpenDatasetGenerator.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Italic);
            btnOpenDatasetGenerator.Image = Properties.Resources.ds_small;
            btnOpenDatasetGenerator.ImageAlign = ContentAlignment.MiddleLeft;
            btnOpenDatasetGenerator.Location = new Point(39, 46);
            btnOpenDatasetGenerator.Name = "btnOpenDatasetGenerator";
            btnOpenDatasetGenerator.Size = new Size(250, 61);
            btnOpenDatasetGenerator.TabIndex = 0;
            btnOpenDatasetGenerator.Text = "Create a Synthetic Dataset";
            btnOpenDatasetGenerator.TextAlign = ContentAlignment.MiddleRight;
            btnOpenDatasetGenerator.UseVisualStyleBackColor = true;
            btnOpenDatasetGenerator.Click += btnOpenDatasetGenerator_Click;
            // 
            // btnExit
            // 
            btnExit.AccessibleDescription = "Exit";
            btnExit.Font = new Font("Segoe UI", 14F);
            btnExit.ForeColor = Color.Red;
            btnExit.Image = Properties.Resources.exit_icon_small;
            btnExit.ImageAlign = ContentAlignment.MiddleRight;
            btnExit.Location = new Point(384, 207);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(111, 67);
            btnExit.TabIndex = 1;
            btnExit.Text = "Exit";
            btnExit.TextAlign = ContentAlignment.MiddleLeft;
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnRunAlgorithms
            // 
            btnRunAlgorithms.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Italic);
            btnRunAlgorithms.Image = Properties.Resources.algorithm;
            btnRunAlgorithms.ImageAlign = ContentAlignment.MiddleLeft;
            btnRunAlgorithms.Location = new Point(39, 122);
            btnRunAlgorithms.Name = "btnRunAlgorithms";
            btnRunAlgorithms.Size = new Size(250, 61);
            btnRunAlgorithms.TabIndex = 0;
            btnRunAlgorithms.Text = "Run Algorithms";
            btnRunAlgorithms.UseVisualStyleBackColor = true;
            btnRunAlgorithms.Click += btnRunAlgorithms_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(507, 286);
            Controls.Add(btnExit);
            Controls.Add(btnRunAlgorithms);
            Controls.Add(btnOpenDatasetGenerator);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "RBAC Role Mininf System - Main Page";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnOpenDatasetGenerator;
        private Button btnExit;
        private Button btnRunAlgorithms;
    }
}
