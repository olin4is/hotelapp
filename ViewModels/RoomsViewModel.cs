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
    public class RoomsViewModel : INotifyPropertyChanged
    {
        private RelayCommand _showMainPage;
        private RelayCommand _showAddRoomPage;
        private RelayCommand _deleteRoom;
        private RelayCommand _showUpdateRoomPage;
        public delegate void MainHandler();
        public event MainHandler Main;
        public delegate void AddRoomHandler();
        public event AddRoomHandler AddRoom;
        public delegate void UpdateRoomHandler(Rooms room);
        public event UpdateRoomHandler UpdateRoom;
        public Rooms selectedRoom { get; set; }
        public ObservableCollection<Rooms> Rooms { get; set; }
        public RoomsViewModel()
        {
            Rooms = DatabaseControl.GetRooms();
        }
        public void RefreshTable()
        {
            Rooms = null;
            Rooms = DatabaseControl.GetRooms();
            OnPropertyChanged("Rooms"); 
        }
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
        public RelayCommand ShowAddRoomPageCommand
        {
            get
            {
                return _showAddRoomPage ?? new RelayCommand(obj =>
                {
                    AddRoom.Invoke();
                });
            }
        }
        public RelayCommand ShowUpdateRoomPageCommand
        {
            get
            {
                return _showUpdateRoomPage ?? new RelayCommand(obj =>
                {
                    if (selectedRoom != null)
                    {
                        UpdateRoom.Invoke(selectedRoom);
                    }
                    else
                    {
                        MessageBox.Show("Выберите номер", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                });
            }
        }
        public RelayCommand DeleteRoomCommand
        {
            get
            {
                return _deleteRoom ?? new RelayCommand(obj =>
                {
                    if (selectedRoom != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить номер?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            DatabaseControl.DeleteRoom(selectedRoom);
                            RefreshTable();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите номер", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

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
