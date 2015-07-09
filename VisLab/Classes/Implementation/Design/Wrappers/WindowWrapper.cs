using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interop;

namespace VisLab.Classes.Implementation.Wrappers
{
    public class WindowWrapper : System.Windows.Forms.IWin32Window
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
