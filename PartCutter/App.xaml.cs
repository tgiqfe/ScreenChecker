using PartCutter.Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PartCutter
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Item.Trimming = new Trimming();
            Item.BindingParam = new BindingParam()
            {
                TrimmingMode = false
            };
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {

        }
    }
}
