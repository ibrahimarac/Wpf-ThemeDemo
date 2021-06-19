using Ibrahim.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ibrahim.UI.Utils
{
    public static class Cache
    {
        static string _theme="Silver";

        static int _loggedUserId;



        public static string Theme
        {
            get
            {
                return _theme;
            }
            set
            {
                _theme = value;
                //Uygulamanında genelinde kullanılan temayı da değiştirelim.
                (Application.Current as App).ChangeTheme(value);
            }
        }

        public static int LoggedUserId
        {
            get
            {
                return _loggedUserId;
            }

            set
            {
                _loggedUserId = value;
            }
        }
    }
}
