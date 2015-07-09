using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace VisLab.Classes
{
    public class ExperimentsTree
    {
        [Serializable]
        public class ExperimentsTreeNode : IDeserializationCallback
        {
            #region Metadata
            private int level = 0;
            public int Level { get { return level; } }

            private bool isOpen;
            public bool IsOpen { get { return isOpen; } }

            private ExperimentsTreeNode parentNode;
            public ExperimentsTreeNode ParentNode { get { return parentNode; } }

            private List<ExperimentsTreeNode> childNodes = new List<ExperimentsTreeNode>();
            public List<ExperimentsTreeNode> ChildNodes
            {
                get { return childNodes; }
            }

            public int ChildsCount { get { return childNodes.Count; } }

            public int DescendantsCount
            {
                get { return GetDescendantsCount(); }
            }
            #endregion

            #region Data
            private Guid id;
            public Guid Id { get { return id; } }
            #endregion

            public ExperimentsTreeNode this[int i]
            {
                get { return childNodes[i]; }
                set { childNodes[i] = value; }
            }

            public ExperimentsTreeNode(Guid id)
            {
                this.id = id;
            }

            public ExperimentsTreeNode() : this(Guid.NewGuid()) { }

            #region Methods
            public void Open()
            {
                if (parentNode != null)
                {
                    parentNode.CloseChilds();
                    isOpen = true;
                }
                else
                {
                    isOpen = true;
                    CloseChilds();
                }
            }

            private void CloseChilds()
            {
                foreach (var child in childNodes)
                {
                    if (child.isOpen)
                    {
                        child.CloseChilds();
                        child.isOpen = false;
                        break;
                    }
                }
            }

            private void AddChild(ExperimentsTreeNode child)
            {
                child.level = this.level + 1;
                child.parentNode = this;
                child.isOpen = true;

                childNodes.Add(child);
            }

            public Guid AddNewChild()
            {
                var child = new ExperimentsTreeNode();
                AddChild(child);
                return child.Id;
            }

            /// <summary>
            /// Depth-first search function
            /// </summary>
            /// <param name="id"></param>
            /// <returns>Experemnt object reference or NULL</returns>
            public ExperimentsTreeNode FindNode(Guid id)
            {
                int i = 0;
                ExperimentsTreeNode result = null;

                if (this.Id == id) return this;
                else
                {
                    if (childNodes.Count == 0) return null;
                    else
                    {
                        do
                        {
                            result = this.childNodes[i++].FindNode(id);
                        } while (result == null && i < childNodes.Count);

                        return result;
                    }
                }
            }

            public void Save(string fileName)
            {
                var bf = new BinaryFormatter();

                using (var fs = File.Create(fileName))
                {
                    bf.Serialize(fs, this);
                }
            }

            public static ExperimentsTreeNode Load(string fileName)
            {
                var bf = new BinaryFormatter();

                using (var fs = File.OpenRead(fileName))
                {
                    return (ExperimentsTreeNode)bf.Deserialize(fs);
                }
            }

            private int GetDescendantsCount()
            {
                int count = 0;

                if (this.ChildsCount == 0) return count;
                else
                {
                    foreach (var child in this.childNodes)
                    {
                        ++count;
                        count += child.GetDescendantsCount();
                    }

                    return count;
                }
            }
            #endregion

            public void OnDeserialization(object sender) { }
        }

        public ExperimentsTreeNode root;

        public ExperimentsTree(string fileName)
        {
            root = new ExperimentsTreeNode();
            root.Open();
            root.Save(fileName);
        }

        private ExperimentsTree() { }

        public void Save(string fileName)
        {
            root.Save(fileName);
        }

        public static ExperimentsTree Load(string fileName)
        {
            var tree = new ExperimentsTree();
            tree.root = ExperimentsTreeNode.Load(fileName);

            return tree;
        }
    }
}
