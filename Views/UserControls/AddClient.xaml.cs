﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using кркр.ViewModels;

namespace кркр.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : UserControl
    {
        public AddClient()
        {
            InitializeComponent();
            
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            (this.DataContext as AddClientViewModel).Password = PasswordBox.Password;
        }
    }
}
