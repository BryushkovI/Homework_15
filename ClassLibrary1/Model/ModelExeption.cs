using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Model
{
    public class ModelExeption : Exception
    {
        public class RemittanceExeption : ModelExeption
        {
            public string Msg { get; }
            public RemittanceExeption()
            {
                Msg = "Невозможно выполнить перевод.\n" +
                      "Сумма перевода некорректна";
            }
        }
        
    }
}
