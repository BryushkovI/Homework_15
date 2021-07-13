using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeWork_13_MVVM.Models.Classes
{
    public class Department<T> : INotifyPropertyChanged
        where T : Client
    {
        public ObservableCollection<T> ClientList { get; set; }
        /// <summary>
        /// Название отдела
        /// </summary>
        public string Nameing { get; set; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Department()
        {
            ClientList = new ObservableCollection<T>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Добавление в список
        /// </summary>
        /// <param name="Item"></param>
        public void Add(T Item)
        {
            ClientList.Add(Item);
        }
    }
}
