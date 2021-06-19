using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Ibrahim.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public void ChangeTheme(string themeName)
        {
            //Temayı güncellemek için yazılan kod
            Application.Current.Resources.Source = new Uri($"/Ibrahim.Themes;component/{themeName}.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
