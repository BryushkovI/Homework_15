using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Model.Classes
{
    public class Entity : Client
    {
        public Entity(JObject client)
            : base(client)
        {

        }
        public Entity(int number)
            : base(number)
        {

        }
        public override int Deposite_percent
        {
            get
            {
                return 5;
            }
        }

        public override int Credit_percent
        {
            get
            {
                return 7;
            }
        }
    }
}
