using Ibrahim.UI.Abstractions;
using Ibrahim.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ibrahim.Scheduler.ViewModels
{
    public class MainVM:ViewModelBase
    {
        private ICommand userCommand;
        private ICommand settingsCommand;
        private ICommand exitCommand;

        public ICommand UserCommand
        {
            get => userCommand ?? new RelayCommand(() =>
            {
                //Kullanıcı işlemleri formunu aç
                Users userWindow = new Users();
                userWindow.ShowDialog();
            },
                () => { return true; });
        }

        public ICommand SettingsCommand
        {
            get => settingsCommand ?? new RelayCommand(() =>
            {
                //Ayarlar butonuna basıldığında ona ait formu aç
                Setting settingForm = new Setting();
                settingForm.ShowDialog();
            },
                () => { return true; });
        }

        public ICommand ExitCommand
        {
            get => settingsCommand ?? new RelayCommand(() =>
            {
                //Çıkış butonuna basıldığında ona ait formu aç
                Application.Current.Shutdown();
            },
                () => { return true; });
        }

    }
}
