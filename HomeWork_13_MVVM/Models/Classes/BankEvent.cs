using HomeWork_13_MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_13_MVVM.Models.Classes
{
    public class BankEvent
    {
        public DateTime _dateTime;
        public DateTime DateTime
        {
            get => DateTime.Now;
        }

        public string _event;
        public string Event {get;set;}
        public BankEvent(string _Event)
        {
            Event = _Event;
        }

        public static void CreateNewEvent(BankEvent bankEvent)
        {
            MainWindowVM._eventsList.Add(bankEvent);
        }

    }
}
