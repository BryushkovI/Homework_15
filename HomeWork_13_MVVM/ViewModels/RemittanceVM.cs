using HomeWork_13_MVVM.Commands;
using HomeWork_13_MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ClassLibrary1.Model;
using ClassLibrary1.Model.Classes;

namespace HomeWork_13_MVVM.ViewModels
{
    internal class RemittanceVM : VMBase
    {
        public static event Action<BankEvent,ObservableCollection<BankEvent>> NewEvent;
        #region Список получателей
        public ObservableCollection<Client> _recipients;
        public ObservableCollection<Client> Recipients
        {
            get => _recipients;
            set
            {
                Set(ref _recipients, value);
            }
        }
        #endregion

        #region Выбранный получатель
        public Client _selectedRecipient;
        public Client Selectedrecipient
        {
            get => _selectedRecipient;
            set
            {
                Set(ref _selectedRecipient, value);
            }
        }
        #endregion

        #region Сумма перевода
        public int _sum;
        public int Sum
        {
            get => _sum;
            set
            {
                Set(ref _sum, value);
            }
        }
        #endregion

        #region Команды
        #region Перевести
        public ICommand RemitCommand { get; }
        private void OnRemitCommandExecuted(object p)
        {
            Function function = new Function();
            function.Remit(_selectedRecipient, _sum,MainWindowVM._departments,MainWindowVM._SelectedClient);
            MainWindowVM._SelectedClient.OnPropertyChanged("Bank_Account");
            NewEvent?.Invoke(new BankEvent($"{MainWindowVM._SelectedClient.Name} перевел(a) {Sum} на счет {Selectedrecipient.Name}."),MainWindowVM._eventsList);
            App.Current.MainWindow.Show();
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Remittance)
                {
                    window.Close();
                    break;
                }
            }

        }
        private bool CanRemitCommandExecute(object p)
        {
            if (MainWindowVM._SelectedClient != null)
            if (_sum <= MainWindowVM._SelectedClient.Bank_Account && _selectedRecipient!=null) return true;
            return false;
        }
        #endregion

        #region Отменить
        public ICommand CancelRemitCommand { get; }
        private void OnCancelRemitExecuted(object p)
        {
            App.Current.MainWindow.Show();
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Remittance)
                {
                    window.Close();
                    break;
                }
            }
        }
        private bool CanCancelRemitCommandExecute(object p) => true;
        #endregion

        #endregion

        public RemittanceVM()
        {
            ClassLibrary1.Model.Methods methods = new ClassLibrary1.Model.Methods();
            _recipients = methods.AllRecipients(MainWindowVM._SelectedClient,MainWindowVM._departments);
            RemitCommand = new LambdaCommand(OnRemitCommandExecuted, CanRemitCommandExecute);
            CancelRemitCommand = new LambdaCommand(OnCancelRemitExecuted, CanCancelRemitCommandExecute);
        }
    }
}
