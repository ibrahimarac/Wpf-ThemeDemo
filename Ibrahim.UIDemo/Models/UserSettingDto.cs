using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibrahim.Scheduler.Models
{
    public class UserSettingDto : INotifyPropertyChanged
    {
        int _id;
        string _theme;

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("Id"); }
        }

        public string Theme
        {
            get { return _theme; }
            set { _theme = value; OnPropertyChanged("Theme"); }
        }

        public IEnumerable<UserSettingDto> Settings { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
