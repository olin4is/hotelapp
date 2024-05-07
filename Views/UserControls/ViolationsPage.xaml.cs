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
using System.Windows.Navigation;
using System.Windows.Shapes;
using кркр.Models;
using кркр.ViewModels;

namespace кркр.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ViolationsPage.xaml
    /// </summary>
    public partial class ViolationsPage : UserControl
    {
        public ViolationsPage()
        {
            InitializeComponent();
        }

        private void ViolationsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViolationsList.SelectedItem != null)
            {
                (this.DataContext as ViolationsViewModel).selectedViolation = (Violations)ViolationsList.SelectedItem;
            }
        }
    }
}
