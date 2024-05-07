using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using кркр.Models;
using кркр.ViewModels;

namespace кркр.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для MoveOutPage.xaml
    /// </summary>
    public partial class MoveOutPage : UserControl
    {
        public MoveOutPage()
        {
            InitializeComponent();
            Info.Visibility = Visibility.Hidden;
            ViolationButton.Visibility = Visibility.Hidden;
        }

        private void BookingsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookingsList.SelectedItem != null)
            {
                //(this.DataContext as MoveOutViewModel).selectedBooking = (Bookings)BookingsList.SelectedItem;
                Info.Visibility = Visibility.Visible;
            }

        }

        private void ViolationsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = ViolationsList.SelectedItems.Cast<Violations>().ToList();

            if ((this.DataContext as MoveOutViewModel) != null)
                (this.DataContext as MoveOutViewModel).ViolationsList = list;
            
            if (list.Any(x => x.Violation == "Нет нарушений") || list.Count() == 0)
            {
                ViolationButton.Visibility = Visibility.Hidden;
            } else
            {
                ViolationButton.Visibility = Visibility.Visible;
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
