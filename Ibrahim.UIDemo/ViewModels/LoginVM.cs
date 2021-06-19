using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ibrahim.Data.Context;
using Ibrahim.UI.Utils;
using Ibrahim.UI.Views;

namespace Ibrahim.Scheduler.ViewModels
{
    public class LoginVM:INotifyPropertyChanged
    {
        private string username;
        private string password;
        private string error;

        ICommand loginCommand;
        ICommand exitCommand;

        public ICommand LoginCommand
        {
            get
            {
                return loginCommand ?? new RelayCommand(() =>
                {
                    UserContext context = new UserContext();
                    var users = context.LoginUsers.ToList();
                    var userInDb = context.LoginUsers.SingleOrDefault(u => u.Username == Username && u.Password == Password);
                    if(userInDb==null)
                    {
                        //Kullanıcı bulunamadı
                        Error="Kullanıcı adı veya parola hatalı.";
                    }
                    else
                    {
                        //Oturum açan kullanıcıyı Cache içerisinde saklayalım. Uygulama sonlanana kadar bu bilgiye ihtiyacımız olacak.
                        Cache.LoggedUserId = userInDb.UserId;

                        //Bu kullanıcı için geçerli temayı etkin hale getirelim.
                        Cache.Theme = context.UserSettings.Include("Theme").SingleOrDefault(us => us.UserId == Cache.LoggedUserId)
                        .Theme.Name;

                        //Ana ekrana ulaşabilir. Yetkili bir kullanıcı
                        Main form = new Main();
                        form.ShowDialog();
                    }
                },
                () =>
                {
                    return !string.IsNullOrEmpty(username)&&!string.IsNullOrEmpty(password);
                });
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                return exitCommand ?? new RelayCommand(() =>
                {
                    Application.Current.Shutdown();
                },
                () =>
                {
                    return true;
                });
            }
        }


        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Error
        {
            get { return error; }
            set { error = value;OnPropertyChanged("Error"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string property="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
