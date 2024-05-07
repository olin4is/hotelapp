using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using кркр.Infrastructure;
using кркр.Models;

namespace кркр.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private RelayCommand _showAuthorizationPage;
        private RelayCommand _showClientsPage;
        private RelayCommand _showRoomsPage;
        private RelayCommand _showBookingInfoCommand;
        private RelayCommand _showViolationsPage;
        private RelayCommand _showBookingsPage;
        private RelayCommand _showFurnituresPage;
        private RelayCommand _showFindRooms;
        private RelayCommand _showMoveOutPage;
        private RelayCommand _showMoveInPage;
        public delegate void AuthorizationHandler();
        public event AuthorizationHandler Authorize;
        public delegate void ClientsHandler();
        public event ClientsHandler Clients;
        public delegate void RoomsHandler();
        public event RoomsHandler RoomsAll;
        public delegate void BookingInfoHandler(Rooms room, DateTime dateOfArrival, DateTime dateOfDeparture);
        public event BookingInfoHandler BookingInfo;
        public delegate void ViolationsHandler();
        public event ViolationsHandler Violations;
        public delegate void BookingsHandler();
        public event BookingsHandler Bookings;
        public delegate void FurnituresHandler();
        public event FurnituresHandler Furnitures;
        public delegate void MoveOutHandler();
        public event MoveOutHandler MoveOut;
        public delegate void MoveInhandler();
        public event MoveInhandler MoveIn;
        public ObservableCollection<Rooms> Rooms { get; set; }
        public Rooms selectedRoom { get; set; }
        public DateTime dateOfArrival { get; set; }
        public DateTime dateOfDeparture { get; set; }
        public MainPageViewModel()
        {
            dateOfArrival = DateTime.Today;
            dateOfDeparture = DateTime.Today;
            //Rooms = DatabaseControl.GetRooms();
            Rooms = DatabaseControl.GetFreeRooms(dateOfArrival, dateOfDeparture);
        }
        public void RefreshTable()
        {
            Rooms = null;
            //Rooms = DatabaseControl.GetRooms();
            Rooms = DatabaseControl.GetFreeRooms(dateOfArrival, dateOfDeparture);
            selectedRoom = null;
            OnPropertyChanged("Rooms");
        }
        public RelayCommand ShowAuthorizationPageCommand
        {
            get
            {
                return _showAuthorizationPage ?? new RelayCommand(obj =>
                {
                    Authorize.Invoke();
                    Session.Id = 0;
                    Session.FIO = "";
                    Session.Role = "";
                });
            }
        }
        public RelayCommand ShowMoveOutPageCommand
        {
            get
            {
                return _showMoveOutPage ?? new RelayCommand(obj =>
                {
                    MoveOut.Invoke();
                });
            }
        }
        public RelayCommand ShowMoveInPageCommand
        {
            get
            {
                return _showMoveInPage ?? new RelayCommand(obj =>
                {
                    MoveIn.Invoke();
                });
            }
        }
        public RelayCommand GetRoomsCommand
        {
            get
            {
                return _showFindRooms ?? new RelayCommand(obj =>
                {
                    RefreshTable();
                });
            }
        }
        public RelayCommand ShowClientsPageCommand
        {
            get
            {
                return _showClientsPage ?? new RelayCommand(obj =>
                {
                    Clients.Invoke();
                });
            }
        }
        public RelayCommand ShowRoomsPageCommand
        {
            get
            {
                return _showRoomsPage ?? new RelayCommand(obj =>
                {
                    RoomsAll.Invoke();
                });
            }
        }
        public RelayCommand ShowBookingInfoCommand
        {
            get
            {
                return _showBookingInfoCommand ?? new RelayCommand(obj =>
                {
                    string dateA = dateOfArrival.ToString();
                    string dateD = dateOfDeparture.ToString();
                    if (selectedRoom != null)
                    {
                        if (selectedRoom.Id != 0 && dateA != "01.01.0001 0:00:00" && dateD != "01.01.0001 0:00:00")
                        {
                            BookingInfo.Invoke(selectedRoom, dateOfArrival, dateOfDeparture);
                        } else
                        {
                            MessageBox.Show("Выберите даты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    } else
                    {
                        MessageBox.Show("Выберите номер", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    
                    
                });
            }
        }
        public RelayCommand ShowViolationsPageCommand
        {
            get
            {
                return _showViolationsPage ?? new RelayCommand(obj =>
                {
                    Violations.Invoke();
                });
            }
        }
        public RelayCommand ShowBookingsPageCommand
        {
            get
            {
                return _showBookingsPage ?? new RelayCommand(obj =>
                {
                    Bookings.Invoke();
                });
            }
        }
        public RelayCommand ShowFurnituresPageCommand
        {
            get
            {
                return _showFurnituresPage ?? new RelayCommand(obj =>
                {
                    Furnitures.Invoke();
                });
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
