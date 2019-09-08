using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QQWater
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += App_Startup;
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            QQWater.Windows.InfoWindow infoWindow = new Windows.InfoWindow();
            infoWindow.ViewModel.Info = e.Exception.Message;
            infoWindow.Show();
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
        }
    }
}
