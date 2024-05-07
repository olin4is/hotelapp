using System;
using System.Collections.Generic;
using System.Data;
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
using кркр.Models;
using кркр.ViewModels;

namespace кркр.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UpdateClient.xaml
    /// </summary>
    public partial class UpdateClient : UserControl
    {
        public UpdateClient()
        {
            InitializeComponent();
            
        }

        private void StatusCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //if (StatusCheckBox.IsChecked == true)
            //{
            //    (this.DataContext as UpdateClientViewModel).selectedStatus = "Заселен";
            //}
            
        }

        private void StatusCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //if (StatusCheckBox.IsChecked == false) { 
            //    (this.DataContext as UpdateClientViewModel).selectedStatus = "Не заселен";
            //}
            
        }
    }
}
