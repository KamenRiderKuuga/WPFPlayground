using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabablzControlDemo.Models
{
    public class DialogMessage : INotifyPropertyChanged
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Message"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
