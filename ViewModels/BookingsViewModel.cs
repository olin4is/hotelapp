using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using кркр.Infrastructure;
using кркр.Models;

namespace кркр.ViewModels
{
    public class BookingsViewModel : INotifyPropertyChanged
    {
        private RelayCommand _showMainPage;
        private RelayCommand _deleteBooking;
        public delegate void MainHandler();
        public event MainHandler Main;
        public ObservableCollection<Bookings> Bookings { get; set; }
        public Bookings selectedBooking { get; set; }
        public RelayCommand ShowMainPageCommand
        {
            get
            {
                return _showMainPage ?? new RelayCommand(obj =>
                {
                    Main.Invoke();
                });
            }
        }
        public void RefreshTable()
        {
            Bookings = null;
            Bookings = DatabaseControl.GetBookings();
            selectedBooking = null;
            OnPropertyChanged("Bookings");
        }
        public BookingsViewModel()
        {
            if (Session.Role == "Клиент")
            {
                Bookings = DatabaseControl.GetClientBooking(Session.Id);
                StatusInfo = Visibility.Hidden;
            } else if (Session.Role == "Админ")
            {
                Bookings = DatabaseControl.GetBookings();
                StatusInfo = Visibility.Visible;
            }
            
        }
        public RelayCommand DeleteBookingCommand
        {
            get
            {
                return _deleteBooking ?? new RelayCommand(obj =>
                {
                    if (selectedBooking != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить бронирование?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            DatabaseControl.DeleteBooking(selectedBooking);
                            RefreshTable();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите бронирование", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                });
            }
        }

        private Visibility _status;
        public Visibility StatusInfo
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
