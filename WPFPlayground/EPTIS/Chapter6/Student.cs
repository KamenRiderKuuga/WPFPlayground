using System.ComponentModel;

namespace EPTIS.Chapter6
{
    public class Student : INotifyPropertyChanged
    {
        private string _name;

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.PropertyChanged.Invoke(this,new PropertyChangedEventArgs("Name"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
