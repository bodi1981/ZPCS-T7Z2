﻿using EmployeesManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmployeesManager.Views
{
    /// <summary>
    /// Interaction logic for DBConfigView.xaml
    /// </summary>
    public partial class DBConfigView : Window
    {
        public DBConfigView(bool isCheckedDBConfig = false)
        {
            InitializeComponent();
            DataContext = new DBConfigVIewModel(isCheckedDBConfig);
        }
    }
}
