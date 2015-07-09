using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VISSIM_COMSERVERLib;
using VisLab.Classes;
using System.IO;
using VisLab.Properties;
using vissim = VisLab.Classes.VissimSingleton;

namespace VisLab.Forms
{
    public partial class CreateProjectForm : Form
    {
        internal string checkedRadioButtonName;

        public CreateProjectForm()
        {
            InitializeComponent();

            this.rbCreateNew.Checked = true;

            this.SetStyle(ControlStyles.FixedHeight, true);
            //tbxModelLocation.DataBindings.Add("Enabled", 

            tbxNewModelName.DataBindings.Add("Enabled", rbCreateNew, "Checked");
            tbxModelFile.DataBindings.Add("Enabled", rbSelectFromFile, "Checked");
            lblCurrentModelName.DataBindings.Add("Enabled", rbSelectCurrent, "Checked");

            string modelName = vissim.Instance.GetInputFileName();
            if (modelName != ".inp")
            {
                lblCurrentModelName.Text = modelName.ToUpper();
                rbSelectCurrent.Checked = true;
            }
        }

        private void btnBrowseProject_Click(object sender, EventArgs e)
        {
            if (dlgFolderBrowser.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                tbxProjectLocation.Text = dlgFolderBrowser.SelectedPath;
                ttpProjectLocation.SetToolTip(this.tbxProjectLocation, dlgFolderBrowser.SelectedPath);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBrowseModelFile_Click(object sender, EventArgs e)
        {
            if (!rbSelectFromFile.Checked) rbSelectFromFile.Checked = true;

            dlgOpenFile.Filter = "VISSIM input file|*.inp";
            dlgOpenFile.FileName = "";

            if (dlgOpenFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                tbxModelFile.Text = dlgOpenFile.FileName;
                ttpModelFileName.SetToolTip(this.tbxModelFile, dlgOpenFile.FileName);
            }
        }

        private void tbxNewModelName_EnabledChanged(object sender, EventArgs e)
        {
            if (!tbxNewModelName.Enabled) tbxNewModelName.Text = "<Model File>";
            else tbxNewModelName.Focus();
        }

        private void tbxModelFile_EnabledChanged(object sender, EventArgs e)
        {
            if (!tbxModelFile.Enabled) tbxModelFile.Text = "<Model Name>";
            else tbxModelFile.Focus();
        }

        private void tbxProjectLocation_Enter(object sender, EventArgs e)
        {
            var tbx = (sender as TextBox);
            if (tbx.SelectionLength != tbx.Text.Length) tbx.SelectAll();
        }

        private void tbxProjectName_Click(object sender, EventArgs e)
        {
            var tbx = (sender as TextBox);

            if (tbx.Text.Contains("<") && tbx.Text.Contains(">"))
                tbx.SelectAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            checkedRadioButtonName = (sender as RadioButton).Name;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (((!tbxModelFile.Text.Contains("<") && !tbxModelFile.Text.Contains(">")) ||
                (!tbxNewModelName.Text.Contains("<") && !tbxNewModelName.Text.Contains(">")) ||
                rbSelectCurrent.Checked) &&
                !tbxProjectLocation.Text.Contains("<") && !tbxProjectLocation.Text.Contains(">") &&
                !tbxProjectName.Text.Contains("<") && !tbxProjectName.Text.Contains(">")) btnOk.Enabled = true;
            else btnOk.Enabled = false;
        }
    }
}
