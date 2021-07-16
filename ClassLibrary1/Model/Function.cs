using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Model.Classes;

namespace ClassLibrary1.Model
{

    public static class Functions
    {
        /// <summary>
        /// Логика удаления клиента
        /// </summary>
        /// <param name="SelectedClient">Применяется к клиенту, которого необходимо удалить</param>
        /// <param name="department">Департамент из которого нужно удалить клиента</param>
        public static void DeliteClient(this Client SelectedClient, Department<Client> department)
        {
            foreach (var e in department.ClientList)
            {
                if (e.Number == int.Parse(SelectedClient.Number.ToString()))
                {
                    department.ClientList.Remove(e);
                    break;
                }
            }
        }
        /// <summary>
        /// Логика перевода денег
        /// </summary>
        /// <param name="SelectedClient">Применяется к отправителю</param>
        /// <param name="_sum">Сумма перевода</param>
        /// <param name="departments">Список отделов</param>
        /// <param name="_selectedRecipient">Получатель</param>
        public static void Remit(this Client SelectedClient, int _sum, ObservableCollection<Department<Client>> departments, Client _selectedRecipient)
        {
            foreach (var e in departments)
            {
                foreach (var i in e.ClientList)
                {
                    if (i == _selectedRecipient)
                    {
                        i.Bank_Account += _sum;
                        i.OnPropertyChanged("Bank_Account");
                        SelectedClient.Bank_Account -= _sum;
                    }
                }
            }
        }
    }
}
