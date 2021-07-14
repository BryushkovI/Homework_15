using HomeWork_13_MVVM.Commands;
//using HomeWork_13_MVVM.Models.Classes;
using HomeWork_13_MVVM.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ClassLibrary1.Model.Classes;

namespace HomeWork_13_MVVM.ViewModels
{
    internal class CreditOpenVM : VMBase
    {
        public static event Action<BankEvent,ObservableCollection<BankEvent>> NewEvent;
        #region Команды
        #region Отмена
        public ICommand OpenCreditCancelCommand { get; }
        private void OnOpenCreditCancelExecuted(object p)
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
        private bool CanOpenCreditCancelExecute(object p) => true;
        #endregion

        #region Выдать кредит
        public ICommand OpenCreditCommand { get; }
        private void OnOpenCreditCommandExecuted(object p)
        {
            MainWindowVM._SelectedClient.Credit = _sum;
            MainWindowVM._SelectedClient.Date_credit = DateTime.Now;
            MainWindowVM._SelectedClient.OnPropertyChanged("Date_credit");
            MainWindowVM._SelectedClient.OnPropertyChanged("Credit");
            NewEvent?.Invoke(new BankEvent($"Клиент {MainWindowVM._SelectedClient.Name} взял кредит на сумму {Sum}."),MainWindowVM._eventsList);
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
        private bool CanOpenCreditCommandExecute(object p)
        {
            if(MainWindowVM._SelectedClient != null)
            if (_sum != 0) return true;
            return false;
        }
        #endregion
        #endregion

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
        public CreditOpenVM()
        {
            _sum = MainWindowVM._SelectedClient.Credit;
            OpenCreditCancelCommand = new LambdaCommand(OnOpenCreditCancelExecuted, CanOpenCreditCancelExecute);
            OpenCreditCommand = new LambdaCommand(OnOpenCreditCommandExecuted, CanOpenCreditCommandExecute);
        }
    }
}
