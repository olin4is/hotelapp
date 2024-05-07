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
    /// Логика взаимодействия для AddRoom.xaml
    /// </summary>
    public partial class AddRoom : UserControl
    {
        public AddRoom()
        {
            InitializeComponent();
        }

        private void RoomTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoomTypeComboBox.SelectedItem != null)
            {
                (this.DataContext as AddRoomViewModel).selectedRoomType = (RoomTypes)RoomTypeComboBox.SelectedItem;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
