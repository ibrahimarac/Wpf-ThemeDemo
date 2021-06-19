using Ibrahim.Data.Context;
using Ibrahim.Data.Core.Domain;
using Ibrahim.Scheduler.ViewModels;
using Ibrahim.UI.Abstractions;
using Ibrahim.UI.Utils;
using Ibrahim.UI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace Ibrahim.UI.ViewModels
{
    public class SettingVM : ViewModelBase,INotifyPropertyChanged
    {
        int _userId;
        Theme _selectedTheme;
        string _error;
        IEnumerable<Theme> _themes;


        public SettingVM()
        {
            _userId = Cache.LoggedUserId;
        }

        private ICommand loadThemesCommand;
        private ICommand saveThemeCommand;
        ICommand exitCommand;

       
        //Aşağıdaki Command, çıkış butonuna basıldığında tetiklenecektir.
        public ICommand ExitCommand
        {
            get
            {
                return exitCommand ?? new RelayCommand(() =>
                {
                    //Aktif pencereyi kapat
                    ApplicationHelper.CloseWindowFromViewModel(WindowId);
                },
                () =>
                {
                    return true;
                });
            }
        }

        //Aşağıdaki Command, Setting isimli View'in Loaded olayında çağrılıyor.
        //Burada tüm temalar yükleniyor ve kullanıcının seçili teması açılır kutuda seçili hale getiriliyor.
        public ICommand LoadThemesCommand
        {
            get => loadThemesCommand ?? new RelayCommand(() =>
            {
                UserContext context = new UserContext();
                //Tüm temeları açılır kutuya yükle.
                Themes = context.Themes.ToList();
                //Bu kullanıcı için seçili temayı açılır kutuda seçili hale getir.
                SelectedTheme = context.UserSettings.Include("Theme").Single(u => u.UserId == _userId).Theme;
            },
            () => { return true; });
        }

        //Aşağıdaki Command kullanıcının seçili temasını veritabanında güncelliyor.
        public ICommand SaveThemeCommand
        {
            get => saveThemeCommand ?? new RelayCommand(() =>
            {
                UserContext context = new UserContext();
                var userSettings = context.UserSettings.Include("Theme").Single(us => us.UserId == _userId);
                userSettings.ThemeId = SelectedTheme.Id;

                try
                {
                    context.SaveChanges();
                    //Cache içerisinde yer alan seçili temayı da güncelliyoruz.
                    Cache.Theme = SelectedTheme.Name;
                    Error = "Tema başarıyla güncellendi.";
                }
                catch (Exception ex)
                {
                    Error = $"Tema güncellenirken bir hata oluştu.\n{ex.Message}";
                }
            },
            () => { return true; });
        }


        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }

        public Theme SelectedTheme
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
