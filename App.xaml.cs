using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using кркр.ViewModels;

namespace кркр
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public AppViewModel mainWindowVM = new AppViewModel();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var main = new MainWindow()
            {
                DataContext = mainWindowVM,
            };
            main.Closed += OnMainWindowClosed;
            main.Show();
        }

        void OnMainWindowClosed(object sender, EventArgs e)
        {
            Shutdown();
        }
    }
}
