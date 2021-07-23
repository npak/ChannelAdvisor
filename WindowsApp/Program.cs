using log4net;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            LoggerManager.Logger.Info("Starting app. Version:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            LoggerManager.Logger.Info(".NET Version:" + Environment.Version);
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
            LoggerManager.Logger.Info("Normal termination of app");
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LoggerManager.Logger.Fatal("Unhandled exception:" + e.ExceptionObject.ToString());
            MessageBox.Show("Unhandled exception was caught, details were written to log file");
            
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LoggerManager.Logger.Fatal("Unhandled exception:" + e.Exception.ToString());
            MessageBox.Show("Unhandled exception was caught, details were written to log file");
        }
    }

    public static class LoggerManager
    {
        public static ILog Logger = log4net.LogManager.GetLogger("Default");        
    }
}
