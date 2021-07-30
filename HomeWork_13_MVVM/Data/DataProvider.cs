using ClassLibrary1.Model;
using ClassLibrary1.Model.Classes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_13_MVVM.Data
{
    public class DataProvider : IClientProvider
    {
        public ObservableCollection<Department<Client>> DeserializeClients(string Path)
        {
            return new FileSystemMethods().DeserializeClients(Path);
        }
    }
}
