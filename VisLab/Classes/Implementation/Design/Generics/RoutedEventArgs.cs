using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace VisLab.Classes.Implementation.Design.Generics
{
    public class RoutedEventArgs<T> : RoutedEventArgs
    {
        public RoutedEventArgs(T value)
        {
            m_value = value;
        }

        public RoutedEventArgs(T value, RoutedEvent routedEvent)
            : base(routedEvent)
        {
            m_value = value;
        }

        private T m_value;

        public T Value
        {
            get { return m_value; }
        }
    }
}
