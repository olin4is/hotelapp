using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using кркр.Infrastructure;
using кркр.Models;

namespace кркр.ViewModels
{
    public class BookingInfoViewModel : INotifyPropertyChanged
    {
        public RelayCommand _showMainPage;
        public delegate void MainHandler();
        public event MainHandler Main;
        private ImageSource _image;
        private RelayCommand _addBooking;
        private Bookings _newBooking = new Bookings();
        public string Description { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public double Summary { get; set; }
        public DateTime DateArrival { get; set; }
        public DateTime DateDeparture { get; set; }
        public Users selectedClient { get; set; }
        public Bookings Bookings { get; set; }
        public Rooms Room { get; set; }
        public ObservableCollection<Users> Clients { get; set; }
        public ImageSource Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }
        
        public BookingInfoViewModel(Rooms room, DateTime dateOfArrival, DateTime dateOfDeparture) {
            Clients = DatabaseControl.GetFreeClients();

            Room = room;
            Description = room.Description;
            RoomType = room.RoomTypesEntity.RoomType;
            Price = room.RoomTypesEntity.Price;
            DateArrival = dateOfArrival;
            DateDeparture = dateOfDeparture;
            double a = (DateDeparture - DateArrival).TotalDays;
            double b = Convert.ToDouble(Price);
            if (a == 0)
            {
                Summary = b;
            } else
            {
                Summary = b * a;
            }
            

            BitmapImage _bitmapImage = new BitmapImage();
            using (Stream stream = File.OpenRead(room.Image))
            {
                _bitmapImage.BeginInit();
                _bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                _bitmapImage.StreamSource = stream;
                _bitmapImage.EndInit();
            }

            Image = _bitmapImage;
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
        public int userId;
        public Bookings NewBooking
        {
            get => _newBooking;
            set
            {
                _newBooking = value;
            }
        }
        public RelayCommand AddBookingCommand
        {
            get
            {
                return _addBooking ??
                    (_addBooking = new RelayCommand(obj =>
                    {
                        bool error = false;
                        if (Session.Role == "Клиент")
                        {
                            userId = Session.Id;
                        } else
                        {
                            if (selectedClient != null)
                            {
                                Users client = DatabaseControl.GetClient(selectedClient);
                                userId = client.Id;
                            } else
                            {
                                error = true;
                                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                            }
                            
                        }
                        if (error == false)
                        {
                            Bookings booking = new Bookings
                            {
                                DateOfDeparture = DateDeparture.ToUniversalTime(),
                                DateOfArrival = DateArrival.ToUniversalTime(),
                                Room_id = Room.Id,
                                Violation_id = 5,
                                User_id = userId,
                                Status = "Не заселен"
                            };
                        
                            DatabaseControl.AddBooking(booking);
                        
                            Main.Invoke();
                        }
                        
                    }));
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
