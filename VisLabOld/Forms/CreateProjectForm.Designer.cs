namespace VisLab.Forms
{
    partial class CreateProjectForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateProjectForm));
            this.tbxProjectName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxProjectLocation = new System.Windows.Forms.TextBox();
            this.btnBrowseProject = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tbxModelFile = new System.Windows.Forms.TextBox();
            this.btnBrowseModelFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCurrentModelName = new System.Windows.Forms.Label();
            this.tbxNewModelName = new System.Windows.Forms.TextBox();
            this.rbSelectCurrent = new System.Windows.Forms.RadioButton();
            this.rbSelectFromFile = new System.Windows.Forms.RadioButton();
            this.rbCreateNew = new System.Windows.Forms.RadioButton();
            this.dlgFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.ttpProjectLocation = new System.Windows.Forms.ToolTip(this.components);
            this.ttpModelFileName = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxProjectName
            // 
            this.tbxProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxProjectName.Location = new System.Drawing.Point(107, 21);
            this.tbxProjectName.Name = "tbxProjectName";
            this.tbxProjectName.Size = new System.Drawing.Size(356, 20);
            this.tbxProjectName.TabIndex = 0;
            this.tbxProjectName.Text = "<Project Name>";
            this.tbxProjectName.Click += new System.EventHandler(this.tbxProjectName_Click);
            this.tbxProjectName.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.tbxProjectName.Enter += new System.EventHandler(this.tbxProjectLocation_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Location:";
            // 
            // tbxProjectLocation
            // 
            this.tbxProjectLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxProjectLocation.Location = new System.Drawing.Point(107, 47);
            this.tbxProjectLocation.Name = "tbxProjectLocation";
            this.tbxProjectLocation.Size = new System.Drawing.Size(269, 20);
            this.tbxProjectLocation.TabIndex = 2;
            this.tbxProjectLocation.Text = "<Project Directory>";
            this.tbxProjectLocation.Click += new System.EventHandler(this.tbxProjectName_Click);
            this.tbxProjectLocation.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.tbxProjectLocation.Enter += new System.EventHandler(this.tbxProjectLocation_Enter);
            // 
            // btnBrowseProject
            // 
            this.btnBrowseProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseProject.Location = new System.Drawing.Point(382, 45);
            this.btnBrowseProject.Name = "btnBrowseProject";
            this.btnBrowseProject.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProject.TabIndex = 1;
            this.btnBrowseProject.Text = "Browse...";
            this.btnBrowseProject.UseVisualStyleBackColor = true;
            this.btnBrowseProject.Click += new System.EventHandler(this.btnBrowseProject_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(301, 184);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Create";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(382, 184);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbxModelFile
            // 
            this.tbxModelFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxModelFile.Location = new System.Drawing.Point(106, 45);
            this.tbxModelFile.Name = "tbxModelFile";
            this.tbxModelFile.Size = new System.Drawing.Size(260, 20);
            this.tbxModelFile.TabIndex = 3;
            this.tbxModelFile.Text = "<Model File>";
            this.tbxModelFile.Click += new System.EventHandler(this.tbxProjectName_Click);
            this.tbxModelFile.EnabledChanged += new System.EventHandler(this.tbxModelFile_EnabledChanged);
            this.tbxModelFile.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.tbxModelFile.Enter += new System.EventHandler(this.tbxProjectLocation_Enter);
            // 
            // btnBrowseModelFile
            // 
            this.btnBrowseModelFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseModelFile.Location = new System.Drawing.Point(372, 43);
            this.btnBrowseModelFile.Name = "btnBrowseModelFile";
            this.btnBrowseModelFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseModelFile.TabIndex = 2;
            this.btnBrowseModelFile.Text = "Browse...";
            this.btnBrowseModelFile.UseVisualStyleBackColor = true;
            this.btnBrowseModelFile.Click += new System.EventHandler(this.btnBrowseModelFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCurrentModelName);
            this.groupBox1.Controls.Add(this.tbxNewModelName);
            this.groupBox1.Controls.Add(this.rbSelectCurrent);
            this.groupBox1.Controls.Add(this.btnBrowseModelFile);
            this.groupBox1.Controls.Add(this.rbSelectFromFile);
            this.groupBox1.Controls.Add(this.tbxModelFile);
            this.groupBox1.Controls.Add(this.rbCreateNew);
            this.groupBox1.Location = new System.Drawing.Point(12, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 98);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Model";
            // 
            // lblCurrentModelName
            // 
            this.lblCurrentModelName.AutoSize = true;
            this.lblCurrentModelName.Location = new System.Drawing.Point(103, 74);
            this.lblCurrentModelName.Name = "lblCurrentModelName";
            this.lblCurrentModelName.Size = new System.Drawing.Size(0, 13);
            this.lblCurrentModelName.TabIndex = 15;
            // 
            // tbxNewModelName
            // 
            this.tbxNewModelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxNewModelName.Location = new System.Drawing.Point(106, 19);
            this.tbxNewModelName.Name = "tbxNewModelName";
            this.tbxNewModelName.Size = new System.Drawing.Size(260, 20);
            this.tbxNewModelName.TabIndex = 1;
            this.tbxNewModelName.Text = "<Model Name>";
            this.tbxNewModelName.Click += new System.EventHandler(this.tbxProjectName_Click);
            this.tbxNewModelName.EnabledChanged += new System.EventHandler(this.tbxNewModelName_EnabledChanged);
            this.tbxNewModelName.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.tbxNewModelName.Enter += new System.EventHandler(this.tbxProjectLocation_Enter);
            // 
            // rbSelectCurrent
            // 
            this.rbSelectCurrent.AutoSize = true;
            this.rbSelectCurrent.Location = new System.Drawing.Point(7, 72);
            this.rbSelectCurrent.Name = "rbSelectCurrent";
            this.rbSelectCurrent.Size = new System.Drawing.Size(91, 17);
            this.rbSelectCurrent.TabIndex = 4;
            this.rbSelectCurrent.Text = "Select current";
            this.rbSelectCurrent.UseVisualStyleBackColor = true;
            this.rbSelectCurrent.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbSelectFromFile
            // 
            this.rbSelectFromFile.AutoSize = true;
            this.rbSelectFromFile.Location = new System.Drawing.Point(7, 46);
            this.rbSelectFromFile.Name = "rbSelectFromFile";
            this.rbSelectFromFile.Size = new System.Drawing.Size(94, 17);
            this.rbSelectFromFile.TabIndex = 6;
            this.rbSelectFromFile.Text = "Select from file";
            this.rbSelectFromFile.UseVisualStyleBackColor = true;
            this.rbSelectFromFile.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbCreateNew
            // 
            this.rbCreateNew.AutoSize = true;
            this.rbCreateNew.Location = new System.Drawing.Point(7, 20);
            this.rbCreateNew.Name = "rbCreateNew";
            this.rbCreateNew.Size = new System.Drawing.Size(79, 17);
            this.rbCreateNew.TabIndex = 0;
            this.rbCreateNew.TabStop = true;
            this.rbCreateNew.Text = "Create new";
            this.rbCreateNew.UseVisualStyleBackColor = true;
            this.rbCreateNew.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.FileName = "openFileDialog1";
            // 
            // ttpProjectLocation
            // 
            this.ttpProjectLocation.AutoPopDelay = 5000;
            this.ttpProjectLocation.InitialDelay = 50;
            this.ttpProjectLocation.ReshowDelay = 100;
            // 
            // ttpModelFileName
            // 
            this.ttpModelFileName.AutoPopDelay = 5000;
            this.ttpModelFileName.InitialDelay = 50;
            this.ttpModelFileName.ReshowDelay = 100;
            // 
            // CreateProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 224);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnBrowseProject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxProjectLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxProjectName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 380);
            this.MinimumSize = new System.Drawing.Size(487, 252);
            this.Name = "CreateProjectForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Project";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowseProject;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnBrowseModelFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSelectCurrent;
        private System.Windows.Forms.RadioButton rbSelectFromFile;
        private System.Windows.Forms.RadioButton rbCreateNew;
        private System.Windows.Forms.Label lblCurrentModelName;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowser;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.ToolTip ttpProjectLocation;
        private System.Windows.Forms.ToolTip ttpModelFileName;
        internal System.Windows.Forms.TextBox tbxProjectName;
        internal System.Windows.Forms.TextBox tbxProjectLocation;
        internal System.Windows.Forms.TextBox tbxNewModelName;
        internal System.Windows.Forms.TextBox tbxModelFile;
    }
}