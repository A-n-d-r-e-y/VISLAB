using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisLab.Classes.Implementation.Design.Generics
{
    public class ValueWrapper<T> where T : struct
    {
        public T Value { get; set; }
        public ValueWrapper(T value) { this.Value = value; }
    }
}
