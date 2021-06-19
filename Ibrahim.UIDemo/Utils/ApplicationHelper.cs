using Ibrahim.UI.Abstractions;
using Ibrahim.Wpf.CustomControls.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ibrahim.UI.Utils
{
    public class ApplicationHelper
    {
        public static void CloseWindowFromViewModel(Guid windowId)
        {
            //ViewModel sınıflarımız IViewModel arayüzünden miras alıyor. Bu arayüzden ViewModel sınıflarımıza WindowId isimli
            //bir Guid geçmiştir. Açık olan windowların DataContext'leri içerisinde yer alan windowId'lere bakarak
            //kapatılacak olanı buluyoruz.
            foreach (Window window in Application.Current.Windows)
            {
                if ((window.DataContext as ViewModelBase)?.WindowId ==windowId )
                    window.Close();
            }
        }
    }
}
