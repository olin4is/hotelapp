using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using кркр.Models;
using кркр.ViewModels;

namespace кркр.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BookingsPage.xaml
    /// </summary>
    public partial class BookingsPage : UserControl
    {
        public BookingsPage()
        {
            InitializeComponent();
            //if (Session.Role == "Клиент")
            //{
            //    Status1.Visibility = Visibility.Hidden;
            //}
        }

        private void BookingsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookingsList.SelectedItem != null)
            {
                (this.DataContext as BookingsViewModel).selectedBooking = (Bookings)BookingsList.SelectedItem;
            }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
