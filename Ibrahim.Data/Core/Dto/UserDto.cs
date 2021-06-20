using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibrahim.Data.Core.Dto
{
    //UserDto Users,UserSettings ve Theme kalsörlerini birleştirmek için kullanılıyor
    public class UserDto : INotifyPropertyChanged
    {
        int _userid;
        string _name;
        string _surname;
        string _email;
        string _username;
        string _password;
        int _themeId;
        string _themeName;

        public int Userid
        {
            get
            {
                return _userid;
            }

            set
            {
                _userid = value;
                OnPropertyChanged("UserId");
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Surname
        {
            get
            {
                return _surname;
            }

            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public int ThemeId
        {
            get
            {
                return _themeId;
            }

            set
            {
                _themeId = value;
                OnPropertyChanged("ThemeId");
            }
        }

        public string ThemeName
        {
            get
            {
                return _themeName;
            }

            set
            {
                _themeName = value;
                OnPropertyChanged("ThemeName");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
