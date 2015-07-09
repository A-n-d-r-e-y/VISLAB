using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisLab.Classes;

namespace VisLab.Controls
{
    enum PlayerByttonType { pbtPlay, pbtStop, pbtPause, pbtMulti }

    class PlayerRoutedEventArgs : RoutedEventArgs
    {
        public PlayerByttonType ButtonType { get; private set; }

        public PlayerRoutedEventArgs(RoutedEvent routedEvent, PlayerByttonType buttonType)
            : base(routedEvent)
        {
            this.ButtonType = buttonType;
        }
    }

    /// <summary>
    /// Interaction logic for PlayerControl.xaml
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        public PlayerControl()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(PlayerControl));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        private void Control_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((sender as Shape).Name.Contains("Play"))  elPlay.OpacityMask = pathPlay.OpacityMask = Brushes.Black;
            if ((sender as Shape).Name.Contains("Stop")) elStop.OpacityMask = pathStop.OpacityMask = Brushes.Black;
            if ((sender as Shape).Name.Contains("Pause")) elPause.OpacityMask = pathPause.OpacityMask = Brushes.Black;
            if ((sender as Shape).Name.Contains("Multi")) elMulti.OpacityMask = pathMulti.OpacityMask = Brushes.Black;
        }

        private void Control_MouseLeave(object sender, MouseEventArgs e)
        {
            var br = new BrushConverter().ConvertFromString("#CA000000") as SolidColorBrush;

            if ((sender as Shape).Name.Contains("Play")) elPlay.OpacityMask = pathPlay.OpacityMask = br;
            if ((sender as Shape).Name.Contains("Stop")) elStop.OpacityMask = pathStop.OpacityMask = br;
            if ((sender as Shape).Name.Contains("Pause")) elPause.OpacityMask = pathPause.OpacityMask = br;
            if ((sender as Shape).Name.Contains("Multi")) elMulti.OpacityMask = pathMulti.OpacityMask = br;
        }

        private Brush mouseUpBrush;
        private Shape mouseUpEllipse;
        private void Control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var shape = (sender as Shape);

            if (shape.Name.Contains("Play"))
            {
                mouseUpBrush = elPlay.Fill;
                mouseUpEllipse = elPlay;

                elPlay.OpacityMask = pathPlay.OpacityMask = Brushes.Black;
                elPlay.Fill = Brushes.Gray;
            }
            if (shape.Name.Contains("Stop"))
            {
                mouseUpBrush = elStop.Fill;
                mouseUpEllipse = elStop;

                elStop.OpacityMask = pathStop.OpacityMask = Brushes.Black;
                elStop.Fill = Brushes.Gray;
            }
            if (shape.Name.Contains("Pause"))
            {
                mouseUpBrush = elPause.Fill;
                mouseUpEllipse = elPause;

                elPause.OpacityMask = pathPause.OpacityMask = Brushes.Black;
                elPause.Fill = Brushes.Gray;
            }
            if (shape.Name.Contains("Multi"))
            {
                mouseUpBrush = elMulti.Fill;
                mouseUpEllipse = elMulti;

                elMulti.OpacityMask = pathMulti.OpacityMask = Brushes.Black;
                elMulti.Fill = Brushes.Gray;
            }
        }

        private void Control_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (mouseUpEllipse != null)
            {
                var buttonType = PlayerByttonType.pbtStop;

                switch (mouseUpEllipse.Name)
                {
                    case "elPause":
                        buttonType = PlayerByttonType.pbtPause;
                        break;
                    case "elPlay":
                        buttonType = PlayerByttonType.pbtPlay;
                        break;
                    case "elMulti":
                        buttonType = PlayerByttonType.pbtMulti;
                        break;
                    case "elStop":
                        buttonType = PlayerByttonType.pbtStop;
                        break;
                }

                if (mouseUpBrush != null)
                {
                    mouseUpEllipse.Fill = mouseUpBrush;
                    mouseUpEllipse = null;
                    mouseUpBrush = null;
                }

                RaiseEvent(new PlayerRoutedEventArgs(PlayerControl.ClickEvent, buttonType));

            }
        }

        private void gridMain_MouseLeave(object sender, MouseEventArgs e)
        {
            if (mouseUpEllipse != null)
            {
                mouseUpEllipse.Fill = mouseUpBrush;
                mouseUpBrush = null;
                mouseUpEllipse = null;
            }
        }
    }
}
