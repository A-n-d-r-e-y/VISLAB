using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisLab.Classes.Implementation.Design.Generics;

namespace VisLab.Classes.Implementation.Design.Interfaces
{
    interface IDogged
    {
        event EventHandler<EventArgs<string, bool>> Attempt;
    }
}
