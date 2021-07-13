using HomeWork_13_MVVM.Commands;
using HomeWork_13_MVVM.Models;
using HomeWork_13_MVVM.Models.Classes;
using HomeWork_13_MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeWork_13_MVVM.ViewModels
{
    internal class DeleteClientVM : VMBase
    {
        public static event Action<BankEvent> NewEvent;
        #region Имя клиента
        public string _selectedClientName;
        public string SelectedClientName
        {
            get => _selectedClientName;
            set
            {
                Set(ref _selectedClientName, value);
            }
        }
        #endregion

        #region Команды
        #region Подтверждение удаления клиента
        public ICommand ConfirmDeleteClientCommand { get; }
        private void OnConfirmDeleteClientCommandExecuted(object p)
        {
            Function function = new Function();
            function.DeliteClient(MainWindowVM._SelectedClient);
            NewEvent?.Invoke(new BankEvent($"Был удален клиент {SelectedClientName}."));
            App.Current.MainWindow.Show();
            foreach (Window window in Application.Current.Windows)
            {
                if (window is DeleteClient)
                {
                    window.Close();
                    break;
                }
            }
        }
        private bool CanConfirmDeleteClientCommandExecute(object p) => true;
        #endregion

        #region Отмена удаления клиента
        public ICommand CancelDeleteClientCommand { get; }
        private void OnCancelDeleteClientCommandExecuted(object p)
        {
            App.Current.MainWindow.Show();
            foreach (Window window in Application.Current.Windows)
            {
                if (window is DeleteClient)
                {
                    window.Close();
                    break;
                }
            }
        }
        private bool CanCancelDeleteClientCommandExecute(object p) => true;
        #endregion
        #endregion
        public DeleteClientVM()
        {
            SelectedClientName = MainWindowVM._SelectedClient.Name;
            ConfirmDeleteClientCommand = new LambdaCommand(OnConfirmDeleteClientCommandExecuted, CanConfirmDeleteClientCommandExecute);
            CancelDeleteClientCommand = new LambdaCommand(OnCancelDeleteClientCommandExecuted, CanCancelDeleteClientCommandExecute);
        }
    }
}
