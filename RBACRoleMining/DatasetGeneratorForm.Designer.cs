namespace RBACRoleMining.WinForms
{
    partial class DatasetGeneratorForm
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
            this.lblUsers = new System.Windows.Forms.Label();
            this.lblPermissions = new System.Windows.Forms.Label();
            this.lblRoles = new System.Windows.Forms.Label();
            this.lblRoleSize = new System.Windows.Forms.Label();
            this.txtUsersCount = new System.Windows.Forms.TextBox();
            this.txtPermissionsCount = new System.Windows.Forms.TextBox();
            this.txtRolesCount = new System.Windows.Forms.TextBox();
            this.txtRoleSizeRange = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnGenerateDataset = new System.Windows.Forms.Button();
            this.btBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUsers
            // 
            this.lblUsers.AutoSize = true;
            this.lblUsers.Location = new System.Drawing.Point(28, 34);
            this.lblUsers.Name = "lblUsers";
            this.lblUsers.Size = new System.Drawing.Size(89, 13);
            this.lblUsers.TabIndex = 1;
            this.lblUsers.Text = "Number of Users:";
            // 
            // lblPermissions
            // 
            this.lblPermissions.AutoSize = true;
            this.lblPermissions.Location = new System.Drawing.Point(28, 72);
            this.lblPermissions.Name = "lblPermissions";
            this.lblPermissions.Size = new System.Drawing.Size(111, 13);
            this.lblPermissions.TabIndex = 2;
            this.lblPermissions.Text = "Number of Permissins:";
            // 
            // lblRoles
            // 
            this.lblRoles.AutoSize = true;
            this.lblRoles.Location = new System.Drawing.Point(27, 107);
            this.lblRoles.Name = "lblRoles";
            this.lblRoles.Size = new System.Drawing.Size(89, 13);
            this.lblRoles.TabIndex = 3;
            this.lblRoles.Text = "Number of Roles:";
            // 
            // lblRoleSize
            // 
            this.lblRoleSize.AutoSize = true;
            this.lblRoleSize.Location = new System.Drawing.Point(28, 136);
            this.lblRoleSize.Name = "lblRoleSize";
            this.lblRoleSize.Size = new System.Drawing.Size(88, 13);
            this.lblRoleSize.TabIndex = 4;
            this.lblRoleSize.Text = "Role size Range:";
            // 
            // txtUsersCount
            // 
            this.txtUsersCount.Location = new System.Drawing.Point(167, 31);
            this.txtUsersCount.Name = "txtUsersCount";
            this.txtUsersCount.Size = new System.Drawing.Size(120, 20);
            this.txtUsersCount.TabIndex = 5;
            // 
            // txtPermissionsCount
            // 
            this.txtPermissionsCount.Location = new System.Drawing.Point(167, 69);
            this.txtPermissionsCount.Name = "txtPermissionsCount";
            this.txtPermissionsCount.Size = new System.Drawing.Size(120, 20);
            this.txtPermissionsCount.TabIndex = 6;
            // 
            // txtRolesCount
            // 
            this.txtRolesCount.Location = new System.Drawing.Point(167, 104);
            this.txtRolesCount.Name = "txtRolesCount";
            this.txtRolesCount.Size = new System.Drawing.Size(117, 20);
            this.txtRolesCount.TabIndex = 7;
            // 
            // txtRoleSizeRange
            // 
            this.txtRoleSizeRange.Location = new System.Drawing.Point(170, 133);
            this.txtRoleSizeRange.Name = "txtRoleSizeRange";
            this.txtRoleSizeRange.Size = new System.Drawing.Size(117, 20);
            this.txtRoleSizeRange.TabIndex = 8;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(31, 258);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(222, 99);
            this.txtOutput.TabIndex = 10;
            this.txtOutput.TextChanged += new System.EventHandler(this.txtOutput_TextChanged);
            // 
            // btnGenerateDataset
            // 
            this.btnGenerateDataset.Image = global::RBACRoleMining.Properties.Resources.generate;
            this.btnGenerateDataset.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerateDataset.Location = new System.Drawing.Point(28, 173);
            this.btnGenerateDataset.Name = "btnGenerateDataset";
            this.btnGenerateDataset.Size = new System.Drawing.Size(164, 70);
            this.btnGenerateDataset.TabIndex = 9;
            this.btnGenerateDataset.Text = "Generate Dataset";
            this.btnGenerateDataset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerateDataset.UseVisualStyleBackColor = true;
            this.btnGenerateDataset.Click += new System.EventHandler(this.btnGenerateDataset_Click);
            // 
            // btBack
            // 
            this.btBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBack.Image = global::RBACRoleMining.Properties.Resources.back_arrow;
            this.btBack.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btBack.Location = new System.Drawing.Point(12, 370);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(117, 68);
            this.btBack.TabIndex = 0;
            this.btBack.Text = "Back";
            this.btBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btBack.UseVisualStyleBackColor = true;
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            // 
            // DatasetGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 450);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnGenerateDataset);
            this.Controls.Add(this.txtRoleSizeRange);
            this.Controls.Add(this.txtRolesCount);
            this.Controls.Add(this.txtPermissionsCount);
            this.Controls.Add(this.txtUsersCount);
            this.Controls.Add(this.lblRoleSize);
            this.Controls.Add(this.lblRoles);
            this.Controls.Add(this.lblPermissions);
            this.Controls.Add(this.lblUsers);
            this.Controls.Add(this.btBack);
            this.Name = "DatasetGeneratorForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btBack;
        private System.Windows.Forms.Label lblUsers;
        private System.Windows.Forms.Label lblPermissions;
        private System.Windows.Forms.Label lblRoles;
        private System.Windows.Forms.Label lblRoleSize;
        private System.Windows.Forms.TextBox txtUsersCount;
        private System.Windows.Forms.TextBox txtPermissionsCount;
        private System.Windows.Forms.TextBox txtRolesCount;
        private System.Windows.Forms.TextBox txtRoleSizeRange;
        private System.Windows.Forms.Button btnGenerateDataset;
        private System.Windows.Forms.TextBox txtOutput;
    }
}