using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisLab.Classes;
using System.IO;

namespace VisLab.Forms
{
    public partial class BrowseDataForm : Form
    {
        public BrowseDataForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            dlgOpenFile.Filter = "Data file|*.data";

            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetFileNameWithoutExtension(dlgOpenFile.FileName).EndsWith("snapshot"))
                    lbxList.DataSource = DirectoryPacker.Browse(dlgOpenFile.FileName).ToList();

                if (Path.GetFileNameWithoutExtension(dlgOpenFile.FileName).EndsWith("tree"))
                {
                    var node = ExperimentsTree.ExperimentsTreeNode.Load(dlgOpenFile.FileName);

                    var treeNode = tvList.Nodes.Add(node.Id.ToString());

                    FillTreeView(treeNode, node);
                }
            }
        }

        private void FillTreeView(TreeNode tNode, ExperimentsTree.ExperimentsTreeNode snNode)
        {
            for (int i = 0; i < snNode.ChildsCount; i++)
            {
                var newTNode = tNode.Nodes.Add(snNode[i].Id.ToString());

                if (snNode[i].ChildsCount > 0) FillTreeView(newTNode, snNode[i]);
            }
        }
    }
}
