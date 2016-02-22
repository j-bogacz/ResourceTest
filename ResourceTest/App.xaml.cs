using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
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
            var resourceNames = GetResourceNames();

            var mainView = new MainWindow();

            var enStrings = new ResourceDictionary { Source = new Uri("pack://application:,,,/Views;component/strings/en/strings.xaml") };
            mainView.Resources.MergedDictionaries.Add(enStrings);

            var currentUICulture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            var currentUICultureUriSuffix = "strings/" + currentUICulture.Name + "/strings.xaml";
            if (!resourceNames.Contains(currentUICultureUriSuffix))
            {
                currentUICultureUriSuffix = "strings/" + currentUICulture.Parent.Name + "/strings.xaml";
            }

            if (resourceNames.Contains(currentUICultureUriSuffix))
            {
                var currentCultureStrings = new ResourceDictionary { Source = new Uri("pack://application:,,,/Views;component/" + currentUICultureUriSuffix) };
                mainView.Resources.MergedDictionaries.Add(currentCultureStrings);
            }

            mainView.Show();
        }

        public static string[] GetResourceNames()
        {
            var assembly = typeof(MainWindow).Assembly;
            string resName = assembly.GetName().Name + ".g.resources";
            using (var stream = assembly.GetManifestResourceStream(resName))
            {
                using (var reader = new System.Resources.ResourceReader(stream))
                {
                    return reader.Cast<DictionaryEntry>().Select(entry =>
                             (string)entry.Key).ToArray();
                }
            }
        }
    }
}
