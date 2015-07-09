using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace VisLab.Classes.Implementation.Design.Generics
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T value)
        {
            m_value = value;
        }

        private T m_value;

        public T Value
        {
            get { return m_value; }
        }
    }

    public class EventArgs<T, U> : EventArgs
    {
        private T value;
        private U param;

        public EventArgs(T value, U param)
        {
            this.value = value;
            this.param = param;
        }

        public T Value { get { return value; } }

        public U Param {
            get { return param; }
            set { param = value; }
        }
    }
}
