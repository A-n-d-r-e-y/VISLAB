using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading;
using System.Windows;
using VisLab.Classes.Implementation.Design.Generics;
using VisLab.Classes.Implementation.Design.Utilities;

namespace VisLab.Classes.Implementation.Design
{
    public class AsyncExceptionHandler
    {
        private readonly Dispatcher context;

        public AsyncExceptionHandler(Dispatcher context)
        {
            this.context = context;
        }

        public event EventHandler<EventArgs<Exception, ValueWrapper<bool>>> Exception;

        private bool OnException(object sender, Exception ex)
        {
            var result = new ValueWrapper<bool>(false);

            if (Exception != null) Exception(sender, new EventArgs<Exception, ValueWrapper<bool>>(ex, result));

            return result.Value;
        }

        private bool HandleException(object sender, Exception ex)
        {
            bool result = false;

            if (Thread.CurrentThread.IsBackground)
            {
                context.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
                {
                    result = OnException(sender, ex);
                }));
            }
            else result = OnException(sender, ex);

            return result;
        }

        public void Protect(Action action)
        {
        tryagain:
            try
            {
                action();
            }
            catch (Exception ex)
            {
                RemoteLogger.ReportIssueAsync(ex);
                if (HandleException(this, ex)) goto tryagain;
            }
        }
    }
}
