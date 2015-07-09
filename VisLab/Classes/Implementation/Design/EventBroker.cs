using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using VisLab.Classes.Implementation.Design.Generics;
using System.Windows;

namespace VisLab.Classes.Implementation.Design
{
    public class EventBroker
    {
        public event EventHandler<EventArgs<Point>> Event;

        public void Method(object sender, EventArgs<Point> e)
        {
            if (Event != null) Event(sender, e);
        }
    }
}
