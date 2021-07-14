using HomeWork_13_MVVM.Commands;
//using HomeWork_13_MVVM.Models;
//using HomeWork_13_MVVM.Models.Classes;
using HomeWork_13_MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ClassLibrary1.Model;
using ClassLibrary1.Model.Classes;

namespace HomeWork_13_MVVM.ViewModels
{
    internal class AddNewClientVM : VMBase
    {
        public static event Action<BankEvent> NewEvent;
        #region Название
        private string _Nameing;
        public string Nameing
        {
            get
            {
                return _Nameing;
            }

            set
            {
                Set(ref _Nameing, value);
            }
        }
        #endregion

        #region Type
        private string _Type;
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                Set(ref _Type, value);
            }
        }

        #endregion

        #region Capitalization
        private string _Capitalization;
        public string Capitalization
        {
            get
            {
                return _Capitalization;
            }

            set { Set(ref _Capitalization, value); }
        }
        #endregion

        #region Commands

        #region NewClient
        public ICommand CreateNewClientCommand { get; }
        private void OnCreateNewClientCommandExecuted(object p)
        {
            Methods methods = new Methods();
            Client client = methods.AddClient(_Nameing, _Type, _Capitalization);
            if (client.Type == "Юр. лицо") MainWindowVM._departments[0].Add(client);
            else if (client.Type == "Физ. лицо") MainWindowVM._departments[1].Add(client);
            else MainWindowVM._departments[2].Add(client);
            NewEvent?.Invoke(new BankEvent($"Был создан новый клиент {Nameing}."));
            App.Current.MainWindow.Show();
            foreach (Window window in Application.Current.Windows)
            {
                if (window is AddNewClient)
                {
                    window.Close();
                    break;
                }
            }

        }
        private bool CanCreateNewClientCommandExecute(object p)
        {
            if (!string.IsNullOrEmpty(_Nameing) && !string.IsNullOrEmpty(_Type) && !string.IsNullOrEmpty(_Capitalization)) return true;
            return false;
        }
        #endregion

        #region Отмена
        public ICommand CancelNewClientCommand { get; }
        private void OnCancelNewClientCommandExecuted(object p)
        {
            App.Current.MainWindow.Show();
            foreach (Window window in App.Current.Windows)
            {
                if (window is AddNewClient)
                {
                    window.Close();
                    break;
                }
            }
        }
        private bool CanCancelNewCommandExecute(object p) => true;
        #endregion

        #endregion

        public AddNewClientVM()
        {
            CreateNewClientCommand = new LambdaCommand(OnCreateNewClientCommandExecuted, CanCreateNewClientCommandExecute);
            CancelNewClientCommand = new LambdaCommand(OnCancelNewClientCommandExecuted, CanCancelNewCommandExecute);
        }

    }
}
