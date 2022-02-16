using EmployeesManager.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmployeesManager.ViewModels
{
    class LogonViewModel : BaseViewModel
    {
        private string _username;
        private string _password;

        public LogonViewModel()
        {
            CheckPasswordCommand = new RelayCommand(CheckPassword);
            PasswordChangedCommand = new RelayCommand(ExecChangePassword);
        }

        public ICommand CheckPasswordCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private void CheckPassword(object obj)
        {
            if (Username == "Admin" && Password == "a")
            {
                CloseWindow(obj as Window);
            }
            else
                MessageBox.Show("Błędna nazwa użytkownia i/lub hasło", "Błąd", MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        private void ExecChangePassword(object obj)
        {
            var passwordBox = obj as PasswordBox;
            Password = passwordBox.Password;
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
