using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tipfeler.Gui {
    static class App {

        private static readonly Mutex SetupMutex = new Mutex(false, @"Global\Tipfeler");

        [STAThread]
        internal static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Medo.Application.UnhandledCatch.Attach();
            Medo.Application.UnhandledCatch.ThreadException += new EventHandler<ThreadExceptionEventArgs>(UnhandledCatch_ThreadException);

            Log.Write.Verbose("Application", "Plugins initializing...");
            Plugin.Initialize();
            Log.Write.Information("Application", "Plugins initialized.");

            Log.Write.Information("Application", "Started");
            Application.Run(new MainForm());
            Log.Write.Information("Application", "Terminated");
            Application.Exit();

            SetupMutex.Close();
        }

        private static void UnhandledCatch_ThreadException(object sender, ThreadExceptionEventArgs e) {
            var ex = e.Exception as Exception;
            if (ex != null) {
                Log.Write.Error("CRITICAL", ex);
            }

            Medo.Diagnostics.ErrorReport.SaveToTemp(e.Exception);
#if !DEBUG
            Medo.Diagnostics.ErrorReport.ShowDialog(null, e.Exception, new Uri("https://api.tipfeler.com/feedback/"), string.Format("{0} at {1}\\{2}", Environment.UserName, Environment.UserDomainName, Environment.MachineName));
#else
            throw e.Exception;
#endif
        }

    }
}
