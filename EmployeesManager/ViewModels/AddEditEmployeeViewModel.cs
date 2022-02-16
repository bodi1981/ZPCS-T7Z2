using EmployeesManager.Commands;
using EmployeesManager.Models.Domains;
using EmployeesManager.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EmployeesManager.ViewModels
{
    class AddEditEmployeeViewModel : BaseViewModel
    {
        private EmployeeWrapper _employee;
        private bool _isEdit;

        public AddEditEmployeeViewModel(EmployeeWrapper employee)
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);

            if (employee == null)
                Employee = new EmployeeWrapper();
            else
            {
                Employee = employee;
                IsEdit = true;
            }
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EmployeeWrapper Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;
                OnPropertyChanged();
            }
        }

        private void Cancel(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void Confirm(object obj)
        {
            if (!Employee.IsValid)
                return;

            if (IsEdit)
            {
                Repository.EditEmployee(Employee);
                CloseWindow(obj as Window);
            }
            else
            {
                Repository.AddEmployee(Employee);
                CloseWindow(obj as Window);
            }
        }

        public bool IsEdit
        {
            get { return _isEdit; }
            set
            {
                _isEdit = value;
                OnPropertyChanged();
            }
        }
    }
}
