using EmployeesManager.Commands;
using EmployeesManager.Models;
using EmployeesManager.Models.Domains;
using EmployeesManager.Models.Wrappers;
using EmployeesManager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EmployeesManager.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private EmployeeWrapper _selectedEmployee;
        private ObservableCollection<EmployeeWrapper> _employees;
        private EmployeeGroup _selectedEmployeeGroup;
        private ObservableCollection<EmployeeGroup> _employeeGroups;

        public MainViewModel()
        {
            AddEmployeeCommand = new RelayCommand(AddEmployee);
            EditEmployeeCommand = new RelayCommand(EditEmployee, CanEditDissambleEmployee);
            DissambleEmployeeCommand = new RelayCommand(DissambleEmployee, CanEditDissambleEmployee);
            EmployeeGroupChangedCommand = new RelayCommand(EmployeeGroupChanged);
            DBConfigCommand = new RelayCommand(SetDBConfig);

            Logon();
            CheckDBConnection();
        }

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand DissambleEmployeeCommand { get; set; }
        public ICommand EmployeeGroupChangedCommand { get; set; }
        public ICommand DBConfigCommand { get; set; }

        public EmployeeWrapper SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EmployeeWrapper> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }

        public EmployeeGroup SelectedEmployeeGroup
        {
            get { return _selectedEmployeeGroup; }
            set
            {
                _selectedEmployeeGroup = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EmployeeGroup> EmployeeGroups
        {
            get { return _employeeGroups; }
            set
            {
                _employeeGroups = value;
                OnPropertyChanged();
            }
        }

        private static void Logon()
        {
            var window = new LogonView();
            window.ShowDialog();
        }

        private void DissambleEmployee(object obj)
        {
            var dialogResult = MessageBox.Show($"Czy na pewno chcesz zwolnić pracownika: {SelectedEmployee.FirstName} {SelectedEmployee.LastName}?", "Zwolnienie pracownika", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {
                Repository.DissambleEmployee(SelectedEmployee);
                RefreshDataGrid();
            }
        }

        private void EditEmployee(object obj)
        {
            var addEditEmployeeView = new AddEditEmployeeView(SelectedEmployee);
            addEditEmployeeView.ShowDialog();
        }

        private bool CanEditDissambleEmployee(object obj)
        {
            return SelectedEmployee != null;
        }

        private void AddEmployee(object obj)
        {
            var addEditEmployeeView = new AddEditEmployeeView();
            addEditEmployeeView.Closed += AddEditEmployeeView_Closed;
            addEditEmployeeView.ShowDialog();
            addEditEmployeeView.Closed -= AddEditEmployeeView_Closed;
        }

        private void AddEditEmployeeView_Closed(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void SetDBConfig(object obj)
        {
            var dbConfigView = new DBConfigView();
            dbConfigView.ShowDialog();
        }


        private void EmployeeGroupChanged(object obj)
        {
            var selectedEmployeeGroup = SelectedEmployeeGroup.Id;
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var employees = Repository.GetEmployees(SelectedEmployeeGroup.Id);
            Employees = new ObservableCollection<EmployeeWrapper>(employees);
        }

        private void InitGroups()
        {
            var employeeGroups = new List<EmployeeGroup>
            {
                new EmployeeGroup { Id = 0, Name = "Wszyscy" },
                new EmployeeGroup { Id = 1, Name = "Pracujący"},
                new EmployeeGroup { Id = 2, Name = "Zwolnieni"}
            };

            EmployeeGroups = new ObservableCollection<EmployeeGroup>(employeeGroups);
            SelectedEmployeeGroup = EmployeeGroups[0];
        }

        private  void CheckDBConnection()
        {
            using (var dbContext = new EmpMgrDbContext())
            {
                var canConnect = dbContext.Database.Exists();

                if (!canConnect)
                {
                    var dialog = MessageBox.Show("Nie udało się połączyć z bazą, czy chcesz zmienić ustawienia?", "Błąd połączenia z bazą", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (dialog == MessageBoxResult.Yes)
                    {
                        var dbConfigView = new DBConfigView(true);
                        dbConfigView.ShowDialog();
                    }
                    else
                        Application.Current.Shutdown();
                }
                else
                {
                    InitGroups();
                    RefreshDataGrid();
                }
            }
        }
    }
}
