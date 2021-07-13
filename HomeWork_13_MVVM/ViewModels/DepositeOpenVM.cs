using HomeWork_13_MVVM.Commands;
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
    internal class DepositeOpenVM : VMBase
    {
        public static event Action<BankEvent> NewEvent;
        #region Сумма кредита
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
        #region Открыть депозит
        public ICommand OpenDepositeCommand { get; }
        private void OnOpenDepositeCommandExecuted(object p)
        {
            MainWindowVM._SelectedClient.Deposite = _sum;
            App.Current.MainWindow.Show();
            MainWindowVM._SelectedClient.Date_deposite = DateTime.Now;
            MainWindowVM._SelectedClient.OnPropertyChanged("Date_deposite");
            MainWindowVM._SelectedClient.OnPropertyChanged("Deposite");
            NewEvent?.Invoke(new BankEvent($"Клиент {MainWindowVM._SelectedClient.Name} открыл депозит на сумму {Sum}."));
            foreach (Window window in Application.Current.Windows)
            {
                if (window is DepositeOpen)
                {
                    window.Close();
                    break;
                }
            }
        }
        private bool CanOpenDepositeCommandExecute(object p)
        {
            if (MainWindowVM._SelectedClient != null)
                if (_sum != 0) return true;
            return false;
        }
        #endregion

        #region Отмена
        public ICommand OpenDepositeCancel { get; }
        private void OnOpenDepositeCancelExecuted(object p)
        {
            App.Current.MainWindow.Show();
            foreach (Window window in Application.Current.Windows)
            {
                if (window is CreditOpen)
                {
                    window.Close();
                    break;
                }
            }
        }
        private bool CanOpenDepositeCancelExecute(object p) => true;
        #endregion
        #endregion
        public DepositeOpenVM()
        {
            OpenDepositeCancel = new LambdaCommand(OnOpenDepositeCancelExecuted, CanOpenDepositeCancelExecute);
            OpenDepositeCommand = new LambdaCommand(OnOpenDepositeCommandExecuted, CanOpenDepositeCommandExecute);
        }
    }
}
