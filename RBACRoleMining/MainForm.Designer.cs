namespace RBACRoleMining.WinForms
{
    partial class MainForm
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
            this.btExit = new System.Windows.Forms.Button();
            this.btnOpenDatasetGenerator = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btExit
            // 
            this.btExit.BackColor = System.Drawing.SystemColors.Control;
            this.btExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExit.ForeColor = System.Drawing.Color.Red;
            this.btExit.Image = global::RBACRoleMining.Properties.Resources.exit_icon_small;
            this.btExit.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btExit.Location = new System.Drawing.Point(1033, 328);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(101, 65);
            this.btExit.TabIndex = 1;
            this.btExit.Text = "Exit";
            this.btExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btExit.UseVisualStyleBackColor = false;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btnOpenDatasetGenerator
            // 
            this.btnOpenDatasetGenerator.Image = global::RBACRoleMining.Properties.Resources.ds_small;
            this.btnOpenDatasetGenerator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenDatasetGenerator.Location = new System.Drawing.Point(47, 41);
            this.btnOpenDatasetGenerator.Name = "btnOpenDatasetGenerator";
            this.btnOpenDatasetGenerator.Size = new System.Drawing.Size(255, 79);
            this.btnOpenDatasetGenerator.TabIndex = 0;
            this.btnOpenDatasetGenerator.Text = "Create a Synthetic Dataset";
            this.btnOpenDatasetGenerator.UseVisualStyleBackColor = true;
            this.btnOpenDatasetGenerator.Click += new System.EventHandler(this.btnOpenDatasetGenerator_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1179, 414);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btnOpenDatasetGenerator);
            this.Name = "MainForm";
            this.Text = "RBAC Role Mininf System - Main Page";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenDatasetGenerator;
        private System.Windows.Forms.Button btExit;
    }
}

