using ClassLibrary1.Model;
using ClassLibrary1.Model.Classes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_13_MVVM.Data
{
    public class DataProvider
    {
        public ObservableCollection<Department<Client>> GetDepartments()
        {
            FileSystemMethods fsm = new FileSystemMethods();
            return fsm.DeserializeClients(@"Clients.json");
        }

    }
}
