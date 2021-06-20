using Ibrahim.Data.Context;
using Ibrahim.Data.Core.Domain;
using Ibrahim.Data.Core.Dto;
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
    //Aşağıdaki ViewModel sınıfında User, UserSettings ve Login bilgileri birlikte güncellenmektedir.
    //Bu bilgilerini tümünü saklamak amacıyla UserDto sınıfı tanımlanmıştır.
    public class UserVM : ViewModelBase, INotifyPropertyChanged
    {
        string _error;
        //Grid'e bağlanan veriler UserDto olarak bağlanmaktadır.
        IEnumerable<UserDto> _users;
        //Users.xaml içerisinde yer alan tema seçim açılır kutusuna bağlanacak veri
        IEnumerable<Theme> _themes;
        //Belli bir anda üzerinde işlem yapılan kullanıcının bilgilerini saklayan field
        UserDto _currentUser;

        string _updateButtonText = "Güncelle";

        //Çıkış butonuna tıklandığında tetiklenen command
        ICommand exitCommand;
        //Form yüklenirken tetiklenen command
        ICommand loadCommand;
        //Temaları yüklemek için kullanılan command
        ICommand loadThemesCommand;
        //Güncelle butonuna basıldığında tetiklenen command
        ICommand updateUserCommand;
        //Yeni Kullanıcı butonuna tıklandığında tetiklenen command
        ICommand createUserCommand;
        //Sil butonuna basıldığında tetiklenen command
        ICommand deleteCommand;

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

        //Aşağıdaki Command, DataDrig'e verileri yüklemek için kullanılır.
        public ICommand LoadCommand
        {
            get
            {
                return loadCommand ?? new RelayCommand(() =>
                {
                    //Grid üzerine verileri yükle
                    LoadGridData();
                },
                () =>
                {
                    return true;
                });
            }
        }

        //Aşağıdaki Command, açılır kutuya temaları yüklemek için kullanılır.
        public ICommand LoadThemesCommand
        {
            get
            {
                return loadThemesCommand ?? new RelayCommand(() =>
                {
                    //Temalar açılır kutusuna kayıtlı temaları yükle
                    UserContext context = new UserContext();
                    Themes = context.Themes;
                },
                () =>
                {
                    return true;
                });
            }
        }

        //Aşağıdaki Command, seçili kullanıcıyı güncellemek ve yeni bir kullanıcı eklmek için kullanılır. 
        public ICommand UpdateUserCommand
        {
            get
            {
                return updateUserCommand ?? new RelayCommand(() =>
                {
                    //Update butonu hem yeni kullanıcı eklerken hem de mevcut kullanıcıyı güncellerken tetiklenmektedir.
                    UserContext context = new UserContext();
                    //Eğer buton güncelleme butonu olarak çalışıyorsa
                    if (UpdateButtonText == "Güncelle")
                    {
                        //Veritabanında kayıtlı kullanıcıyı arayüzden gelen verilerle güncelliyoruz.
                        var user = context.Users.SingleOrDefault(u => u.Id == CurrentUser.Userid);
                        user.Name = CurrentUser.Name;
                        user.Surname = CurrentUser.Surname;
                        user.Email = CurrentUser.Email;
                        //Veritabanında kayıtlı olan tema güncelleniyor
                        var settings = context.UserSettings.SingleOrDefault(s => s.UserId == CurrentUser.Userid);
                        settings.ThemeId = CurrentUser.ThemeId;
                        //Veritabanında kayıtlı olan kullanıcı adı ve parolası güncelleniyor
                        var login = context.LoginUsers.SingleOrDefault(l => l.UserId == CurrentUser.Userid);
                        context.SaveChanges();
                        Error = "Kayıt başarıyla güncellendi.";
                    }
                    else if (UpdateButtonText == "Kaydet") //Eğer buton yeni kayıt ekleme butonu olarak çalışıyorsa
                    {
                        //Kullanıcıyı oluştur
                        var newUser = new User
                        {
                            Name = CurrentUser.Name,
                            Surname = CurrentUser.Surname,
                            Email = CurrentUser.Email
                        };
                        context.Users.Add(newUser);
                        //Kullanıcıya ait ayarları ekle
                        var newSetting = new UserSettings
                        {
                            ThemeId = CurrentUser.ThemeId,
                            UserId = newUser.Id
                        };
                        context.UserSettings.Add(newSetting);
                        //Kullanıcıya ait Login bilgilerini ekleyelim
                        var newLogin = new LoginUser
                        {
                            Username=CurrentUser.Username,
                            Password=CurrentUser.Password,
                            UserId=newUser.Id
                        };
                        context.LoginUsers.Add(newLogin);
                        context.SaveChanges();
                        //Kayıt eklendi. Artık kayıt butonunu eski haline getirelim
                        UpdateButtonText = "Güncelle";
                        Error = "Kayıt başarıyla eklendi.";
                    }

                    //Güncel kullanıcı listesini tekrar grid'e doldur
                    LoadGridData();
                },
                () =>
                {
                    return true;
                });
            }
        }

        //Aşağıdaki Command, seçili kullanıcıyı güncellemek için kullanılır.
        public ICommand CreateUserCommand
        {
            get
            {
                return createUserCommand ?? new RelayCommand(() =>
                {
                    //Yeni kullanıcı ekle butonuna basıldığında arayüzü temizle
                    //Böylece yeni eklenen kayda ait bilgiler arayüzden girilebilecektir.
                    CurrentUser = new UserDto();
                    UpdateButtonText = "Kaydet";
                },
                () =>
                {
                    return true;
                });
            }
        }

        //Aşağıdaki Command, sil butonuna basıldığında tetiklenecektir.
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? new RelayCommand(() =>
                {
                    //Grid üzerinde herhangi bir kayıt seçilmemişse
                    if(CurrentUser.Userid==0)
                    {
                        Error = "Listeden silmek istediğiniz kullanıcıyı seçmelisiniz.";
                    }
                    else
                    {
                        UserContext context = new UserContext();
                        context.UserSettings.Remove(context.UserSettings.SingleOrDefault(us=>us.UserId==CurrentUser.Userid));
                        context.LoginUsers.Remove(context.LoginUsers.SingleOrDefault(l => l.UserId == CurrentUser.Userid));
                        context.Users.Remove(context.Users.SingleOrDefault(u => u.Id == CurrentUser.Userid));
                        context.SaveChanges();
                        Error = "Kayıt başarıyla silindi";
                        //Güncel kullanıcı listesini bağla
                        LoadGridData();
                    }
                },
                () =>
                {
                    return true;
                });
            }
        }


        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }

        public IEnumerable<UserDto> Users
        {
            get
            {
                return _users;
            }

            set
            {
                _users = value;
                OnPropertyChanged("Users");
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

        public UserDto CurrentUser
        {
            get
            {
                return _currentUser;
            }

            set
            {
                _currentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }

        public string UpdateButtonText
        {
            get
            {
                return _updateButtonText;
            }

            set
            {
                _updateButtonText = value;
                OnPropertyChanged("UpdateButtonText");
            }
        }


        //Kullanıcıları Grid'e yükleyen metod
        void LoadGridData()
        {
            UserContext context = new UserContext();
            //Şu an oturum açan kullanıcı dışındaki tüm kullanıcıları yükle
            Users = (from u in context.Users
                     join us in context.UserSettings.Include("Theme") on u.Id equals us.UserId
                     join l in context.LoginUsers on u.Id equals l.UserId
                     where u.Id!=Cache.LoggedUserId
                     select new UserDto
                     {
                         Email = u.Email,
                         Name = u.Name,
                         Password = l.Password,
                         Surname = u.Surname,
                         ThemeId = us.ThemeId,
                         Userid = u.Id,
                         Username = l.Username,
                         ThemeName = us.Theme.Name
                     }).ToList();

            CurrentUser = Users.FirstOrDefault();

            Themes = context.Themes.ToList();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
