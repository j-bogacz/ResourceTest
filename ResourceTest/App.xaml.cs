using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Views;

namespace ResourceTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
            var mainView = new MainWindow();

            foreach (var resourceUri in GetResourceUrisFromResourcesFolder(typeof(MainWindow).Assembly)
            {
                mainView.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(resourceUri, UriKind.Relative) });
            }

            mainView.Show();
        }

        private IEnumerable<string> GetResourceUrisFromResourcesFolder()
        {
            throw new NotImplementedException();
        }
    }
}
