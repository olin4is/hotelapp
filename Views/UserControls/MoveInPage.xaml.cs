using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace кркр.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для MoveInPage.xaml
    /// </summary>
    public partial class MoveInPage : UserControl
    {
        public MoveInPage()
        {
            InitializeComponent();
            Info.Visibility = Visibility.Hidden;
        }

        private void BookingsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookingsList.SelectedItem != null)
            {
            // (this.DataContext as MoveInViewModel).SelectedBooking = (Bookings)BookingsList.SelectedItem;
               Info.Visibility = Visibility.Visible;
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
