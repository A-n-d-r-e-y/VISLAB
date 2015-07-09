using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace VisLab.Classes
{
    enum PlayerByttonType { pbtPlay, pbtStop, pbtPause }

    class PlayerRoutedEventArgs : RoutedEventArgs
    {
        public PlayerByttonType ButtonType { get; private set; }

        public PlayerRoutedEventArgs(RoutedEvent routedEvent, PlayerByttonType buttonType)
            : base(routedEvent)
        {
            this.ButtonType = buttonType;
        }
    }
}
