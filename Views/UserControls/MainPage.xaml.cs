using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using кркр.Models;
using кркр.ViewModels;

namespace кркр.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            FIO.Text = Session.FIO;
            if (Session.Role == "Клиент")
            {
                Clients.Visibility = Visibility.Hidden;
                Rooms.Visibility = Visibility.Hidden;
                Departure.Visibility = Visibility.Hidden;
                Violations.Visibility = Visibility.Hidden;
                Arrival.Visibility = Visibility.Hidden;
            }
            
        }

        private void RoomsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoomsList.SelectedItem != null)
            {
                (this.DataContext as MainPageViewModel).selectedRoom = (Rooms)RoomsList.SelectedItem;
            }
            
        }

        private void DateArrival_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateArrival.SelectedDate != null)
            {
                (this.DataContext as MainPageViewModel).dateOfArrival = (DateTime)DateArrival.SelectedDate;
            }
        }

        private void DateDeparture_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateDeparture.SelectedDate != null)
            {
                (this.DataContext as MainPageViewModel).dateOfDeparture = (DateTime)DateDeparture.SelectedDate;
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
