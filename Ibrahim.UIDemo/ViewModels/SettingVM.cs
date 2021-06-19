using Ibrahim.Data.Context;
using Ibrahim.Data.Core.Domain;
using Ibrahim.Scheduler.ViewModels;
using Ibrahim.UI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Ibrahim.UI.ViewModels
{
    public class SettingVM:INotifyPropertyChanged
    {
        int _userId=1;
        int _selectedTheme;
        IEnumerable<Theme> _themes;

       

        //public SettingVM(int userId)
        //{
        //    _userId = userId;

        //}

        private ICommand loadThemesCommand;
        private ICommand saveThemeCommand;

        public ICommand LoadThemesCommand
        {
            get => loadThemesCommand ?? new RelayCommand(() =>
            {
                UserContext context = new UserContext();
                //INotifyPropertyChanged arayüzü Entity sınıflarında kullanılamadığından Entity'mize karşılık gelen
                //Dto sınıfı yazdık. Dolayısıyla burada veritabanından gelen Entity nesnesini Dto nesnesine çeviriyoruz.
                Themes = context.Themes.ToList();
                SelectedTheme = context.UserSettings.Single(u => u.UserId == _userId).ThemeId;
            },
            () => { return true; });
        }

        public ICommand SaveThemeCommand
        {
            get => saveThemeCommand ?? new RelayCommand(() =>
            {
                UserContext context = new UserContext();
                var userSettings=context.UserSettings.Single(us => us.UserId == _userId);
                userSettings.ThemeId = SelectedTheme;
            },
            () => { return true; });
        }



        public int SelectedTheme
        {
            get
            {
                return _selectedTheme;
            }
            set
            {
                _selectedTheme = value;
                OnPropertyChanged("SelectedTheme");
            }
        }


        public IEnumerable<Theme> Themes
        {
            get
            {
                return _themes;
            }

            set
            {
                _themes = value;
                OnPropertyChanged("Themes");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
