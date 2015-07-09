using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Controls;
using System.ComponentModel;
using VisLab.Controls;
using VisLab.Classes.Implementation.Design.Generics;
using VisLab.Classes.Implementation.Analysis.Boundaries.Controls;

namespace VisLab.Classes.Implementation.Design
{
    public enum LinkDirection { ldRight, ldLeft, ldTop, ldBottom }

    public static class LinkDirectionHelper
    {
        public static LinkDirection Invert(LinkDirection direction)
        {
            switch (direction)
            {
                case LinkDirection.ldRight: return LinkDirection.ldLeft;
                case LinkDirection.ldLeft: return LinkDirection.ldRight;
                case LinkDirection.ldTop: return LinkDirection.ldBottom;
                case LinkDirection.ldBottom:
                default: return LinkDirection.ldTop;
            }
        }
    }

    public class VisualNavigatorNode
    {
        public UserControl Control { get; set; }
        private VisualNavigatorNode[] arr = new VisualNavigatorNode[4];

        public VisualNavigatorNode GetLinkedNode(LinkDirection direction)
        {
            return arr[(int) direction];
        }

        public bool HasLinkedNode(LinkDirection direction)
        {
            return arr[(int)direction] != null;
        }

        public void Link(LinkDirection direction, VisualNavigatorNode node)
        {
            arr[(int)direction] = node;

            // link back
            if (node != null)
            {
                var invertDirection = LinkDirectionHelper.Invert(direction);
                var invertDirectionNode = node.GetLinkedNode(invertDirection);
                if (invertDirectionNode != this) node.Link(invertDirection, this);
            }
        }
    }

    public class VisualNavigator : INotifyPropertyChanged
    {
        private Dictionary<UserControl, VisualNavigatorNode> dict = new Dictionary<UserControl, VisualNavigatorNode>();

        private VisualNavigatorNode focusedNode;
        public VisualNavigatorNode FocusedNode
        {
            get { return focusedNode; }
            set
            {
                focusedNode = value;
                OnPropertyChanged("FocusedNode");

                OnPropertyChanged("CanNavigateRight");
                OnPropertyChanged("CanNavigateLeft");
                OnPropertyChanged("CanNavigateTop");
                OnPropertyChanged("CanNavigateBottom");

                OnPropertyChanged("TopDirectionModelName");
                OnPropertyChanged("BottomDirectionModelName");
                OnPropertyChanged("LeftDirectionModelName");
                OnPropertyChanged("RightDirectionModelName");

                if (value != null) OnFocusChanged(value.Control);
            }
        }

        public event EventHandler<EventArgs<UserControl>> FocusChanged;
        private void OnFocusChanged(UserControl control)
        {
            if (FocusChanged != null) FocusChanged(this, new EventArgs<UserControl>(control));
        }

        private bool isEnabled = true;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged("CanNavigateRight");
                OnPropertyChanged("CanNavigateLeft");
                OnPropertyChanged("CanNavigateTop");
                OnPropertyChanged("CanNavigateBottom");
            }
        }

        public bool CanNavigateRight { get { return (FocusedNode == null) ? false : FocusedNode.HasLinkedNode(LinkDirection.ldRight) && IsEnabled; } }
        public bool CanNavigateLeft { get { return (FocusedNode == null) ? false : FocusedNode.HasLinkedNode(LinkDirection.ldLeft) && IsEnabled; } }
        public bool CanNavigateTop { get { return (FocusedNode == null) ? false : FocusedNode.HasLinkedNode(LinkDirection.ldTop) && IsEnabled; } }
        public bool CanNavigateBottom { get { return (FocusedNode == null) ? false : FocusedNode.HasLinkedNode(LinkDirection.ldBottom) && IsEnabled; } }

        public string TopDirectionModelName
        {
            get { return GetNavigationHint(LinkDirection.ldTop); }
        }
        public string BottomDirectionModelName
        {
            get { return GetNavigationHint(LinkDirection.ldBottom); }
        }
        public string LeftDirectionModelName
        {
            get { return GetNavigationHint(LinkDirection.ldLeft); }
        }
        public string RightDirectionModelName
        {
            get { return GetNavigationHint(LinkDirection.ldRight); }
        }

        private string GetNavigationHint(LinkDirection direction)
        {
            bool canNavigate = (FocusedNode != null && FocusedNode.HasLinkedNode(direction));
            string text = string.Empty;

            if (canNavigate)
            {
                var control = FocusedNode.GetLinkedNode(direction).Control;

                if (control is TreeControl) text = (control as TreeControl).modelName;
                if (control is ProjectControl) text = "Project";
                if (control is ReportControl) text = "Report";
            }

            return text;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public VisualNavigator(UserControl firstControl)
        {
            AddLinkedControl(firstControl);

            FocusedNode = dict[firstControl];
        }

        public void Navigate(LinkDirection direction)
        {
            var targetNode = FocusedNode.GetLinkedNode(direction);

            // relink TreeControl (call link back)
            if (FocusedNode.Control is TreeControl && !(targetNode.Control is TreeControl) )
            {
                FocusedNode.Link(LinkDirection.ldLeft, FocusedNode.GetLinkedNode(LinkDirection.ldLeft));
                FocusedNode.Link(LinkDirection.ldRight, FocusedNode.GetLinkedNode(LinkDirection.ldRight));
            }

            FocusedNode = targetNode;
        }

        public string GetModelNameForHorizont()
        {
            var node = FocusedNode;
            int stop = 0;
            string result = null;

            while (node != null && !(node.Control is TreeControl) && stop++ < 3)
                node = node.GetLinkedNode(LinkDirection.ldLeft);

            if (node != null && node.Control is TreeControl) result = (node.Control as TreeControl).modelName;

            return result;
        }

        private void AddLinkedControl(UserControl control)
        {
            dict[control] = new VisualNavigatorNode()
            {
                Control = control
            };
        }

        public void LinkControl(LinkDirection direction, UserControl linkedUserControl)
        {
            if (!dict.ContainsKey(linkedUserControl)) AddLinkedControl(linkedUserControl);

            FocusedNode.Link(direction, dict[linkedUserControl]);

            switch (direction)
            {
                case LinkDirection.ldRight:
                    OnPropertyChanged("CanNavigateRight");
                    OnPropertyChanged("RightDirectionModelName");
                    break;
                case LinkDirection.ldLeft:
                    OnPropertyChanged("CanNavigateLeft");
                    OnPropertyChanged("LeftDirectionModelName");
                    break;
                case LinkDirection.ldTop:
                    OnPropertyChanged("CanNavigateTop");
                    OnPropertyChanged("TopDirectionModelName");
                    break;
                case LinkDirection.ldBottom:
                    OnPropertyChanged("CanNavigateBottom");
                    OnPropertyChanged("BottomDirectionModelName");
                    break;
                default:
                    break;
            }
        }

        public void InsertTreeControl(UserControl control)
        {
            VisualNavigatorNode newNode;

            if (!dict.ContainsKey(control))
            {
                newNode = new VisualNavigatorNode()
                {
                    Control = control
                };

                dict[control] = newNode;
            }
            else newNode = dict[control];

            // низ текущего прикрепляем у низу нового
            newNode.Link(LinkDirection.ldBottom, this.FocusedNode.GetLinkedNode(LinkDirection.ldBottom));
            OnPropertyChanged("TopDirectionModelName");

            // верх нового прикрепляем к низу текущего
            LinkControl(LinkDirection.ldBottom, control);
            OnPropertyChanged("BottomDirectionModelName");

            // левая сторона нового = левой стороне текущего
            newNode.Link(LinkDirection.ldLeft, this.FocusedNode.GetLinkedNode(LinkDirection.ldLeft));
            OnPropertyChanged("LeftDirectionModelName");

            // правая сторона нового = правой стороне текущего
            newNode.Link(LinkDirection.ldRight, this.FocusedNode.GetLinkedNode(LinkDirection.ldRight));
            OnPropertyChanged("RightDirectionModelName");

        }

        public void RemoveTreeControlByModelName(string modelName)
        {
            var node = (from item in dict
                       where item.Key is TreeControl && (item.Key as TreeControl).modelName.ToUpper() == modelName.ToUpper()
                       select item.Value).FirstOrDefault();

            // down of the higher attach to the top of the lower
            var upper = node.GetLinkedNode(LinkDirection.ldTop);


            //var bottom = node.GetLinkedNode(LinkDirection.ldBottom);
            //upper.Link(LinkDirection.ldBottom, bottom);

            ////////////////////

            foreach (var uc in dict.Where(p => p.Key == node.Control).Select(m => m.Key).ToList())
            {
                dict.Remove(uc);
            }

            foreach (var pair in dict.Where(p => p.Key != node.Control))
            {
                for (int i = 0; i < 4; i++)
                {
                    if (pair.Value.HasLinkedNode((LinkDirection)i))
                    {
                        var n = pair.Value.GetLinkedNode((LinkDirection)i);
                        if (n.Control == node.Control) pair.Value.Link((LinkDirection)i, upper);
                    }
                }
            }

            ////////////////////

            if (!(FocusedNode.Control is ProjectControl)) FocusedNode = upper;
        }

        public void Clear()
        {
            dict.Clear();
            FocusedNode = null;
        }

        public bool HasTreeControls()
        {
            return dict.Count(kvp =>
                {
                    return kvp.Key is TreeControl;
                }) > 0;
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
