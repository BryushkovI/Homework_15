using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Model.Classes

namespace ClassLibrary1.Model
{
    public class Functions
    {
        /// <summary>
        /// Логика удаления клиента
        /// </summary>
        /// <param name="SelectedClient"></param>
        public void DeliteClient(Client SelectedClient)
        {
            Department<Client> clients = MainWindowVM._department;

            foreach (var e in clients.ClientList)
            {
                if (e.Number == int.Parse(SelectedClient.Number.ToString()))
                {
                    clients.ClientList.Remove(e);
                    break;
                }
            }
        }

        public void Remit(Client _selectedRecipient, int _sum)
        {
            foreach (var e in MainWindowVM._departments)
            {
                foreach (var i in e.ClientList)
                {
                    if (i == _selectedRecipient)
                    {
                        i.Bank_Account += _sum;
                        i.OnPropertyChanged("Bank_Account");
                        MainWindowVM._SelectedClient.Bank_Account -= _sum;
                    }
                }
            }
        }
    }
}
