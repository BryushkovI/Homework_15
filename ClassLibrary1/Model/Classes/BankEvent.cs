using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Model.Classes
{
    public class BankEvent
    {
        public DateTime _dateTime;
        public DateTime DateTime
        {
            get => DateTime.Now;
        }

        public string _event;
        public string Event { get; set; }
        public BankEvent(string _Event)
        {
            Event = _Event;
        }

        public static void CreateNewEvent(BankEvent bankEvent, ObservableCollection<BankEvent> bankEvents)
        {
            bankEvents.Add(bankEvent);
        }
    }
}
