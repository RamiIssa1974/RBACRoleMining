namespace RBACRoleMining.WinForm
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
            lblNumOfUsers = new Label();
            lblNumberOfPermissions = new Label();
            lblNumOfRoles = new Label();
            lblMinRoleSize = new Label();
            btnGenerateDataset = new Button();
            txtUsersCount = new TextBox();
            txtPermissionsCount = new TextBox();
            txtRolesCount = new TextBox();
            txtMinRoleSize = new TextBox();
            txtOutput = new TextBox();
            btnBack = new Button();
            lblMaxRoleSize = new Label();
            txtMaxRoleSize = new TextBox();
            groupBox1 = new GroupBox();
            txtOutputFilePath = new TextBox();
            lblOutputFilePath = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // lblNumOfUsers
            // 
            lblNumOfUsers.AutoSize = true;
            lblNumOfUsers.Location = new Point(20, 40);
            lblNumOfUsers.Name = "lblNumOfUsers";
            lblNumOfUsers.Size = new Size(99, 15);
            lblNumOfUsers.TabIndex = 0;
            lblNumOfUsers.Text = "Number of Users:";
            // 
            // lblNumberOfPermissions
            // 
            lblNumberOfPermissions.AutoSize = true;
            lblNumberOfPermissions.Location = new Point(18, 75);
            lblNumberOfPermissions.Name = "lblNumberOfPermissions";
            lblNumberOfPermissions.Size = new Size(134, 15);
            lblNumberOfPermissions.TabIndex = 1;
            lblNumberOfPermissions.Text = "Number of Permissions:";
            // 
            // lblNumOfRoles
            // 
            lblNumOfRoles.AutoSize = true;
            lblNumOfRoles.Location = new Point(18, 112);
            lblNumOfRoles.Name = "lblNumOfRoles";
            lblNumOfRoles.Size = new Size(101, 15);
            lblNumOfRoles.TabIndex = 2;
            lblNumOfRoles.Text = "Number Of Roles:";
            // 
            // lblMinRoleSize
            // 
            lblMinRoleSize.AutoSize = true;
            lblMinRoleSize.Location = new Point(18, 151);
            lblMinRoleSize.Name = "lblMinRoleSize";
            lblMinRoleSize.Size = new Size(76, 15);
            lblMinRoleSize.TabIndex = 3;
            lblMinRoleSize.Text = "Min role size:";
            // 
            // btnGenerateDataset
            // 
            btnGenerateDataset.Image = Properties.Resources.generate;
            btnGenerateDataset.ImageAlign = ContentAlignment.MiddleLeft;
            btnGenerateDataset.Location = new Point(18, 227);
            btnGenerateDataset.Name = "btnGenerateDataset";
            btnGenerateDataset.Size = new Size(177, 67);
            btnGenerateDataset.TabIndex = 4;
            btnGenerateDataset.Text = "Generate Dataset";
            btnGenerateDataset.TextAlign = ContentAlignment.MiddleRight;
            btnGenerateDataset.UseVisualStyleBackColor = true;
            btnGenerateDataset.Click += btnGenerateDataset_Click;
            // 
            // txtUsersCount
            // 
            txtUsersCount.Location = new Point(151, 37);
            txtUsersCount.Name = "txtUsersCount";
            txtUsersCount.Size = new Size(100, 23);
            txtUsersCount.TabIndex = 5;
            txtUsersCount.Text = "500";
            txtUsersCount.TextChanged += txtUsersCount_TextChanged;
            // 
            // txtPermissionsCount
            // 
            txtPermissionsCount.Location = new Point(151, 72);
            txtPermissionsCount.Name = "txtPermissionsCount";
            txtPermissionsCount.Size = new Size(100, 23);
            txtPermissionsCount.TabIndex = 6;
            txtPermissionsCount.Text = "100";
            txtPermissionsCount.TextChanged += txtPermissionsCount_TextChanged;
            // 
            // txtRolesCount
            // 
            txtRolesCount.Location = new Point(151, 109);
            txtRolesCount.Name = "txtRolesCount";
            txtRolesCount.Size = new Size(100, 23);
            txtRolesCount.TabIndex = 7;
            txtRolesCount.Text = "15";
            txtRolesCount.TextChanged += txtRolesCount_TextChanged;
            // 
            // txtMinRoleSize
            // 
            txtMinRoleSize.Location = new Point(151, 148);
            txtMinRoleSize.Name = "txtMinRoleSize";
            txtMinRoleSize.Size = new Size(100, 23);
            txtMinRoleSize.TabIndex = 8;
            txtMinRoleSize.Text = "3";
            // 
            // txtOutput
            // 
            txtOutput.Location = new Point(313, 21);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.ScrollBars = ScrollBars.Vertical;
            txtOutput.Size = new Size(445, 349);
            txtOutput.TabIndex = 9;
            // 
            // btnBack
            // 
            btnBack.AccessibleDescription = "Back to Main Page";
            btnBack.Image = Properties.Resources.back_arrow;
            btnBack.ImageAlign = ContentAlignment.MiddleRight;
            btnBack.Location = new Point(654, 376);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(104, 73);
            btnBack.TabIndex = 10;
            btnBack.Text = "Back";
            btnBack.TextAlign = ContentAlignment.MiddleLeft;
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // lblMaxRoleSize
            // 
            lblMaxRoleSize.AutoSize = true;
            lblMaxRoleSize.Location = new Point(18, 182);
            lblMaxRoleSize.Name = "lblMaxRoleSize";
            lblMaxRoleSize.Size = new Size(77, 15);
            lblMaxRoleSize.TabIndex = 3;
            lblMaxRoleSize.Text = "Max role size:";
            // 
            // txtMaxRoleSize
            // 
            txtMaxRoleSize.Location = new Point(151, 182);
            txtMaxRoleSize.Name = "txtMaxRoleSize";
            txtMaxRoleSize.Size = new Size(100, 23);
            txtMaxRoleSize.TabIndex = 8;
            txtMaxRoleSize.Text = "7";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtOutputFilePath);
            groupBox1.Controls.Add(lblOutputFilePath);
            groupBox1.Controls.Add(txtMaxRoleSize);
            groupBox1.Controls.Add(txtMinRoleSize);
            groupBox1.Controls.Add(txtRolesCount);
            groupBox1.Controls.Add(txtPermissionsCount);
            groupBox1.Controls.Add(txtUsersCount);
            groupBox1.Controls.Add(btnGenerateDataset);
            groupBox1.Controls.Add(lblMaxRoleSize);
            groupBox1.Controls.Add(lblMinRoleSize);
            groupBox1.Controls.Add(lblNumOfRoles);
            groupBox1.Controls.Add(lblNumberOfPermissions);
            groupBox1.Controls.Add(lblNumOfUsers);
            groupBox1.Location = new Point(24, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(271, 415);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Dataset Configuration";
            // 
            // txtOutputFilePath
            // 
            txtOutputFilePath.Location = new Point(26, 335);
            txtOutputFilePath.Name = "txtOutputFilePath";
            txtOutputFilePath.Size = new Size(225, 23);
            txtOutputFilePath.TabIndex = 10;
            txtOutputFilePath.Text = "C:\\\\RBAC\\\\data.csv";
            // 
            // lblOutputFilePath
            // 
            lblOutputFilePath.AutoSize = true;
            lblOutputFilePath.Location = new Point(26, 317);
            lblOutputFilePath.Name = "lblOutputFilePath";
            lblOutputFilePath.Size = new Size(93, 15);
            lblOutputFilePath.TabIndex = 9;
            lblOutputFilePath.Text = "Output File Path";
            // 
            // DatasetGeneratorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 461);
            Controls.Add(groupBox1);
            Controls.Add(btnBack);
            Controls.Add(txtOutput);
            Name = "DatasetGeneratorForm";
            Text = "DatasetGeneratorForm";
            Load += DatasetGeneratorForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNumOfUsers;
        private Label lblNumberOfPermissions;
        private Label lblNumOfRoles;
        private Label lblMinRoleSize;
        private Button btnGenerateDataset;
        private TextBox txtUsersCount;
        private TextBox txtPermissionsCount;
        private TextBox txtRolesCount;
        private TextBox txtMinRoleSize;
        private TextBox txtOutput;
        private Button btnBack;
        private Label lblMaxRoleSize;
        private TextBox txtMaxRoleSize;
        private GroupBox groupBox1;
        private TextBox txtOutputFilePath;
        private Label lblOutputFilePath;
    }
}