using System.Windows;
using System.Windows.Controls;
using кркр.Models;
using кркр.ViewModels;

namespace кркр.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BookingInfoPage.xaml
    /// </summary>
    public partial class BookingInfoPage : UserControl
    {
        public BookingInfoPage()
        {
            InitializeComponent();
            if (Session.Role == "Клиент")
            {
                Clients.Visibility = Visibility.Hidden;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsComboBox.SelectedItem != null)
            {
                (this.DataContext as BookingInfoViewModel).selectedClient = (Users)ClientsComboBox.SelectedItem;
            }
            
        }
    }
}
