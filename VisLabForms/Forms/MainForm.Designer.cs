namespace VisLab.Forms
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
            this.cboWindowMode = new System.Windows.Forms.ComboBox();
            this.mnMainStrip = new System.Windows.Forms.MenuStrip();
            this.mntProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mntNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mntOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mntClose = new System.Windows.Forms.ToolStripMenuItem();
            this.sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mntRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.mntModel = new System.Windows.Forms.ToolStripMenuItem();
            this.mntRunSimulation = new System.Windows.Forms.ToolStripMenuItem();
            this.mntStopSimulation = new System.Windows.Forms.ToolStripMenuItem();
            this.mntReports = new System.Windows.Forms.ToolStripMenuItem();
            this.mntSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mntBrowseSnapshot = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.mnMainStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboWindowMode
            // 
            this.cboWindowMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboWindowMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWindowMode.FormattingEnabled = true;
            this.cboWindowMode.Items.AddRange(new object[] {
            "Standalone",
            "Modeless",
            "Modal",
            "Topmost"});
            this.cboWindowMode.Location = new System.Drawing.Point(362, 27);
            this.cboWindowMode.Name = "cboWindowMode";
            this.cboWindowMode.Size = new System.Drawing.Size(121, 21);
            this.cboWindowMode.TabIndex = 7;
            this.cboWindowMode.SelectedIndexChanged += new System.EventHandler(this.cboWindowMode_SelectedIndexChanged);
            // 
            // mnMainStrip
            // 
            this.mnMainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mntProject,
            this.mntModel,
            this.mntReports,
            this.mntSettings});
            this.mnMainStrip.Location = new System.Drawing.Point(0, 0);
            this.mnMainStrip.Name = "mnMainStrip";
            this.mnMainStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnMainStrip.Size = new System.Drawing.Size(495, 24);
            this.mnMainStrip.TabIndex = 8;
            this.mnMainStrip.Text = "menuStrip1";
            // 
            // mntProject
            // 
            this.mntProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mntNew,
            this.mntOpen,
            this.mntClose,
            this.sep1,
            this.mntRecent});
            this.mntProject.Name = "mntProject";
            this.mntProject.Size = new System.Drawing.Size(56, 20);
            this.mntProject.Text = "&Project";
            // 
            // mntNew
            // 
            this.mntNew.Name = "mntNew";
            this.mntNew.Size = new System.Drawing.Size(152, 22);
            this.mntNew.Text = "&New";
            this.mntNew.Click += new System.EventHandler(this.mntNew_Click);
            // 
            // mntOpen
            // 
            this.mntOpen.Name = "mntOpen";
            this.mntOpen.Size = new System.Drawing.Size(152, 22);
            this.mntOpen.Text = "&Open";
            this.mntOpen.Click += new System.EventHandler(this.mntOpen_Click);
            // 
            // mntClose
            // 
            this.mntClose.Enabled = false;
            this.mntClose.Name = "mntClose";
            this.mntClose.Size = new System.Drawing.Size(152, 22);
            this.mntClose.Text = "&Close";
            // 
            // sep1
            // 
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(149, 6);
            // 
            // mntRecent
            // 
            this.mntRecent.Name = "mntRecent";
            this.mntRecent.Size = new System.Drawing.Size(152, 22);
            this.mntRecent.Text = "&Recent";
            // 
            // mntModel
            // 
            this.mntModel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mntRunSimulation,
            this.mntStopSimulation});
            this.mntModel.Name = "mntModel";
            this.mntModel.Size = new System.Drawing.Size(53, 20);
            this.mntModel.Text = "&Model";
            // 
            // mntRunSimulation
            // 
            this.mntRunSimulation.Name = "mntRunSimulation";
            this.mntRunSimulation.Size = new System.Drawing.Size(158, 22);
            this.mntRunSimulation.Text = "Run Simulation";
            this.mntRunSimulation.Click += new System.EventHandler(this.mntRunSimulation_Click);
            // 
            // mntStopSimulation
            // 
            this.mntStopSimulation.Name = "mntStopSimulation";
            this.mntStopSimulation.Size = new System.Drawing.Size(158, 22);
            this.mntStopSimulation.Text = "Stop Simulation";
            this.mntStopSimulation.Click += new System.EventHandler(this.mntStopSimulation_Click);
            // 
            // mntReports
            // 
            this.mntReports.Name = "mntReports";
            this.mntReports.Size = new System.Drawing.Size(59, 20);
            this.mntReports.Text = "&Reports";
            // 
            // mntSettings
            // 
            this.mntSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mntBrowseSnapshot});
            this.mntSettings.Name = "mntSettings";
            this.mntSettings.Size = new System.Drawing.Size(61, 20);
            this.mntSettings.Text = "&Settings";
            // 
            // mntBrowseSnapshot
            // 
            this.mntBrowseSnapshot.Name = "mntBrowseSnapshot";
            this.mntBrowseSnapshot.Size = new System.Drawing.Size(175, 22);
            this.mntBrowseSnapshot.Text = "Browse snapshot ...";
            this.mntBrowseSnapshot.Click += new System.EventHandler(this.mntBrowseSnapshot_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(154, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 63);
            this.label1.TabIndex = 9;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(22, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 63);
            this.label2.TabIndex = 10;
            this.label2.Text = "label2";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(408, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(408, 95);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Backup";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(408, 124);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "Restore";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(374, 151);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "Network Location";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(408, 180);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 15;
            this.button5.Text = "Model";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 262);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboWindowMode);
            this.Controls.Add(this.mnMainStrip);
            this.MainMenuStrip = this.mnMainStrip;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mnMainStrip.ResumeLayout(false);
            this.mnMainStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboWindowMode;
        private System.Windows.Forms.MenuStrip mnMainStrip;
        private System.Windows.Forms.ToolStripMenuItem mntProject;
        private System.Windows.Forms.ToolStripMenuItem mntNew;
        private System.Windows.Forms.ToolStripMenuItem mntOpen;
        private System.Windows.Forms.ToolStripMenuItem mntClose;
        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        internal System.Windows.Forms.ToolStripMenuItem mntRecent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem mntModel;
        private System.Windows.Forms.ToolStripMenuItem mntRunSimulation;
        private System.Windows.Forms.ToolStripMenuItem mntStopSimulation;
        private System.Windows.Forms.ToolStripMenuItem mntReports;
        private System.Windows.Forms.ToolStripMenuItem mntSettings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem mntBrowseSnapshot;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}