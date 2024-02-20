using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Input;
using HomeWork_13_MVVM.Commands;
using HomeWork_13_MVVM.View;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ClassLibrary1.Model.Classes;
using ClassLibrary1.Model;
using ClassLibrary1;
using HomeWork_13_MVVM.Data;

namespace HomeWork_13_MVVM.ViewModels
{
    internal class MainWindowVM : VMBase
    {

        #region Список отделов

        public static ObservableCollection<Department<Client>> _departments;

        public ObservableCollection<Department<Client>> Departments
        {
            get => _departments;
            set
            {
                Set(ref _departments, value);
            }
        }

        #endregion

        #region Департамент
        public static Department<Client> _department;
        public Department<Client> Department
        {
            get => _department;
            set
            {
                Set(ref _department, value);
            }
        }
        #endregion

        #region Выбранный клиент 

        public static Client _SelectedClient;

        public Client SelectedClient
        {
            get
            {
                return _SelectedClient;
            }
            set
            {
                Set(ref _SelectedClient, value);
            }
        }

        #endregion

        #region Прогноз кредита/депозита
        #region Количество лет
        public static int _years;
        public int Years
        {
            get => _years;
            set
            {
                try
                {
                    Set(ref _years, value);
                    _SelectedClient.Predict_Years = _years;
                    _SelectedClient.OnPropertyChanged("CurentDeposite");
                    _SelectedClient.OnPropertyChanged("CurentCredit");
                }
                catch (NullReferenceException)
                {
                    if(Years != 0)
                    {
                        MessageBox.Show("Не был выбран клиент.\nПеред прогнозированием выберете нужного клиента.",
                                    "Ошибка",
                                    MessageBoxButton.OK);
                        Years = 0;
                    }
                    
                }
            }
        }
        #endregion
        #region Количество месяцев
        public static int _months;
        public int Months
        {
            get => _months;
            set
            {
                try
                {
                    Set(ref _months, value);
                    _SelectedClient.Predict_Months = _months;
                    _SelectedClient.OnPropertyChanged("CurentDeposite");
                }
                catch (NullReferenceException)
                {
                    if (Months != 0)
                    {
                        MessageBox.Show("Не был выбран клиент.\nПеред прогнозированием выберете нужного клиента.",
                                    "Ошибка",
                                    MessageBoxButton.OK);
                        Months = 0;
                    }
                }
            }
        }
        #endregion
        #endregion

        #region Команды

        #region Добавление нового клиента
        public ICommand AddNewClientCommand { get; }
        private void OnAddNewClientCommandExecuted(object p)
        {
            AddNewClient addNewClient = new AddNewClient();
            addNewClient.Show();
            App.Current.MainWindow.Hide();
        }
        private bool CanAddNewClientCommandExecute(object p) => true;
        #endregion

        #region Выход
        public ICommand ExitCommand { get; }
        private void OnExitCommandExecuted(object p)
        {
            _departments.SerializeCilents(@"Clients.json");
            App.Current.MainWindow.Close();
        }
        private bool CanExitCommandExecute(object p) => true;
        #endregion

        #region Удалить клиента
        public ICommand DeleteClientCommand { get; }
        private void OnDeleteClientCommandExecuted(object p)
        {
            App.Current.MainWindow.Hide();
            DeleteClient deliteClient = new DeleteClient();
            deliteClient.Show();
        }
        private bool CanDeleteClientCommandExecute(object p)
        {
            if (_SelectedClient != null) return true;
            return false;
        }
        #endregion

        #region Перевести деньги
        public ICommand RemittanceCommand { get; }

        private void OnRemittanceCommandExecuted(object p)
        {
            App.Current.MainWindow.Hide();
            Remittance remittance = new Remittance();
            remittance.Show();
        }
        private bool CanRemittanceCommandExecute(object p)
        {
            if (_SelectedClient != null) return true;
            return false;
        }
        #endregion

        #region Открыть вклад
        public ICommand OpenDepositeCommand { get; }
        private void OnOpenDepositeCommandExecuted(object p)
        {
            DepositeOpen depositeOpen = new DepositeOpen();
            depositeOpen.Show();
            App.Current.MainWindow.Hide();
        }
        private bool CanOpenDepositeCommandExecute(object p)
        {
            if(_SelectedClient != null)
            if (_SelectedClient.Deposite == 0) return true;
            return false;
        }
        #endregion

        #region Выдать кредит
        public ICommand  CreditCommand { get; }

        private void OnCreditCommandExecuted(object p)
        {
            CreditOpen creditOpen = new CreditOpen();
            creditOpen.Show();
            App.Current.MainWindow.Hide();

        }
        private bool CanCreditCommandExecute(object p)
        {
            if(_SelectedClient != null)
            {
                if (_SelectedClient.Credit == 0) return true;
            }
            return false;
        }
        #endregion

        #endregion

        #region Список событий

        public static ObservableCollection<BankEvent> _eventsList;
        public ObservableCollection<BankEvent> EventsList
        {
            get => _eventsList;
            set
            {
                Set(ref _eventsList, value);
            }
        }
        #endregion
        public MainWindowVM( )
        {
            
            _departments = new DataProvider().DeserializeClients(@"Clients.json");
            AddNewClientCommand = new LambdaCommand(OnAddNewClientCommandExecuted, CanAddNewClientCommandExecute);
            ExitCommand = new LambdaCommand(OnExitCommandExecuted, CanExitCommandExecute);
            DeleteClientCommand = new LambdaCommand(OnDeleteClientCommandExecuted, CanDeleteClientCommandExecute);
            RemittanceCommand = new LambdaCommand(OnRemittanceCommandExecuted, CanRemittanceCommandExecute);
            CreditCommand = new LambdaCommand(OnCreditCommandExecuted, CanCreditCommandExecute);
            OpenDepositeCommand = new LambdaCommand(OnOpenDepositeCommandExecuted, CanOpenDepositeCommandExecute);
            AddNewClientVM.NewEvent += BankEvent.CreateNewEvent;
            DeleteClientVM.NewEvent += BankEvent.CreateNewEvent;
            RemittanceVM.NewEvent += BankEvent.CreateNewEvent;
            CreditOpenVM.NewEvent += BankEvent.CreateNewEvent;
            DepositeOpenVM.NewEvent += BankEvent.CreateNewEvent;
            _eventsList = new ObservableCollection<BankEvent>();
        }
    }
}
