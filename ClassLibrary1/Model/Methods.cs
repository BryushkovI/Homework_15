﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Model.Classes;


namespace ClassLibrary1.Model
{
    public class Methods
    {
        /// <summary>
        /// Создание уникального номера клиента
        /// </summary>
        /// <param name="Type">Тип клиента</param>
        /// <returns></returns>
        public int GetNum(string Type)
        {
            int spec;
            if (Type == "Юр. лицо") spec = 1;
            else if (Type == "Физ. лицо") spec = 2;
            else spec = 3;
            Random random = new Random();
            return Convert.ToInt32(spec * 10000000 + random.Next(0, 9999999));
        }
        /// <summary>
        /// Создание клиента
        /// </summary>
        /// <param name="Nameing">Имя клиента</param>
        /// <param name="Type">Тип клиента</param>
        /// <param name="Capitalizaion">Тип капитализации</param>
        /// <returns></returns>
        public static Client AddClient(string Nameing, string Type, string Capitalizaion)
        {
            Client client;
            Type = Type.ToString().Substring(38);
            switch (Type)
            {

                case "Юр. лицо":
                    client = new Entity(GetNum(Type));
                    break;
                case "Физ. лицо":
                    client = new Individual_regular(GetNum(Type));
                    break;
                default:
                    client = new Individual_VIP(GetNum(Type));
                    break;
            }

            client.Name = Nameing;
            client.Bank_Account = 0;
            client.Deposite = 0;
            Capitalizaion = Capitalizaion.Substring(38);
            if (Capitalizaion == "С капитализацией")
            {
                client.Deposite_Type = "WithCapital";
            }
            else
            {
                client.Deposite_Type = "WithoutCapital";
            }
            client.Credit = 0;
            client.Type = Type;
            return client;
        }

        /// <summary>
        /// Все gлучатели
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static ObservableCollection<Client> AllRecipients(Client client,ObservableCollection<Department<Client>> departments)
        {
            ObservableCollection<Client> allClients = new ObservableCollection<Client>();

            foreach (var e in departments)
            {
                foreach (var i in e.ClientList)
                {
                    if (i != client) allClients.Add(i);
                }
            }

            return allClients;
        }
    }
}
