using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using кркр.Models;

namespace кркр.ViewModels
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private object _currentContentVM;
        private AuthorizationViewModel _authorization = new AuthorizationViewModel();
        private RegistrationViewModel _registration = new RegistrationViewModel();
        private MainPageViewModel _main = new MainPageViewModel();
        private AddClientViewModel _addClient = new AddClientViewModel();
        private AddRoomViewModel _addRoom = new AddRoomViewModel();
        private AddViolationViewModel _addViolation = new AddViolationViewModel();
        
        public object CurrentContentVM
        {
            get { return _currentContentVM; }
            set
            { 
                _currentContentVM = value; 
                OnPropertyChanged("CurrentContentVM"); 
            }
        }
        public AppViewModel()
        {
            CurrentContentVM = _authorization;
            _authorization.Authorize += ShowRegisterPage;
            _registration.Register += ShowAuthorizationPage;
            _main.Authorize += ShowAuthorizationPage;
            _authorization.Main += ShowMainPage;
        }

        public void ShowRegisterPage()
        {
            CurrentContentVM = _registration;
        }

        public void ShowAuthorizationPage()
        {
            CurrentContentVM = _authorization;
        }
        public void ShowMainPage()
        {
            ClientsViewModel _clients = new ClientsViewModel();
            CurrentContentVM = _main;
            
            _main.Clients += ShowClientsPage;
            _main.Clients += _clients.RefreshTable;
            _main.RoomsAll += ShowRoomsPage;
            _main.BookingInfo += ShowBookingInfoPage;
            _main.Violations += ShowViolationsPage;
            _main.Bookings += ShowBookingsPage;
            _main.MoveOut += ShowMoveOutPage;
            _main.MoveIn += ShowMoveInPage;
        }
        public void ShowClientsPage()
        {
            ClientsViewModel _clients = new ClientsViewModel();
            CurrentContentVM = _clients;
            _clients.AddClient += ShowAddClientPage;
            _clients.UpdateClient += ShowUpdateClientPage;
            _clients.Main += ShowMainPage;
        }
        public void ShowRoomsPage()
        {
            RoomsViewModel _rooms = new RoomsViewModel();
            CurrentContentVM = _rooms;
            _rooms.AddRoom += ShowAddRoomPage;
            _rooms.UpdateRoom += ShowUpdateRoomPage;
            _addRoom.Rooms += ShowRoomsPage;
            _addRoom.Rooms += _rooms.RefreshTable;
            _rooms.Main += ShowMainPage;
            _rooms.Main += _main.RefreshTable;
        }
        public void ShowAddClientPage()
        {
            ClientsViewModel _clients = new ClientsViewModel();
            _addClient.Client += _clients.RefreshTable;
            _addClient.Client += ShowClientsPage;
            CurrentContentVM = _addClient;
        }
        public void ShowAddRoomPage()
        {
            CurrentContentVM = _addRoom;
        }
        public void ShowAddViolationPage()
        {
            CurrentContentVM = _addViolation;
        }
        public void ShowUpdateClientPage(Clients client)
        {
            UpdateClientViewModel _updateClient = new UpdateClientViewModel(client);
            ClientsViewModel _clients = new ClientsViewModel();
            CurrentContentVM = _updateClient;
            _updateClient.Client += ShowClientsPage;
            _updateClient.Client += _clients.RefreshTable;
        }
        public void ShowUpdateRoomPage(Rooms room) { 
            UpdateRoomViewModel _updateRoom = new UpdateRoomViewModel(room);
            RoomsViewModel _rooms = new RoomsViewModel();
            CurrentContentVM = _updateRoom;
            _updateRoom.Rooms += ShowRoomsPage;
            _updateRoom.Rooms += _rooms.RefreshTable;
        }
        public void ShowUpdateViolationPage(Violations violation) {
            UpdateViolationViewModel _updateViolation = new UpdateViolationViewModel(violation);
            ViolationsViewModel _violations = new ViolationsViewModel();
            CurrentContentVM = _updateViolation;
            _updateViolation.Violations += ShowViolationsPage;
            _updateViolation.Violations += _violations.RefreshTable;
        }
        public void ShowBookingInfoPage(Rooms room, DateTime dateOfArrival, DateTime dateOfDeparture)
        {
            BookingInfoViewModel _bookingInfo = new BookingInfoViewModel(room, dateOfArrival, dateOfDeparture);
            CurrentContentVM = _bookingInfo;
            _bookingInfo.Main += ShowMainPage;
            _bookingInfo.Main += _main.RefreshTable;
        }
        public void ShowViolationsPage()
        {
            ViolationsViewModel _violations = new ViolationsViewModel();
            CurrentContentVM = _violations;
            _violations.AddViolation += ShowAddViolationPage;
            _violations.UpdateViolation += ShowUpdateViolationPage;
            _addViolation.Violations += ShowViolationsPage;
            _addViolation.Violations += _violations.RefreshTable;
            _violations.Main += ShowMainPage;
        }
        public void ShowBookingsPage()
        {
            BookingsViewModel _bookings = new BookingsViewModel();
            CurrentContentVM = _bookings;
            _bookings.Main += ShowMainPage;
            _main.Bookings += _bookings.RefreshTable;
        }
        public void ShowMoveOutPage()
        {
            MoveOutViewModel _moveOut = new MoveOutViewModel();
            CurrentContentVM = _moveOut;
            _moveOut.Main += ShowMainPage;
            _moveOut.Main += _main.RefreshTable;
        }
        public void ShowMoveInPage()
        {
            MoveInViewModel _moveIn = new MoveInViewModel();
            CurrentContentVM = _moveIn;
            _moveIn.Main += ShowMainPage;
            _moveIn.Main += _main.RefreshTable;
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
