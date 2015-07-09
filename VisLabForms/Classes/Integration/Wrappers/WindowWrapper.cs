using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisLab.Classes
{
    public class WindowWrapper : IWin32Window
    {
        private IntPtr _hwnd = IntPtr.Zero;

        public IntPtr Handle
        {
            get { return _hwnd; }
        }

        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }

        public void Clear()
        {
            _hwnd = IntPtr.Zero;
        }
    }
}
