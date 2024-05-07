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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using кркр.Infrastructure;
using кркр.Models;

namespace кркр.ViewModels
{
    public class AddRoomViewModel : INotifyPropertyChanged
    {
        private Rooms _newRoom = new Rooms();
        private RelayCommand _showRoomsPage;
        public delegate void RoomsHandler();
        public event RoomsHandler Rooms;
        private RelayCommand _addRoom;
        private RelayCommand _image;
        private OpenFileDialog _img;
        public ImageSource Image;
        public ObservableCollection<RoomTypes> RoomTypes {  get; set; }
        public RoomTypes selectedRoomType { get; set; }
        public RelayCommand ShowRoomsPageCommand
        {
            get
            {
                return _showRoomsPage ?? new RelayCommand(obj =>
                {
                    _newRoom.Image = "";
                    _newRoom.Description = "";
                    selectedRoomType = null;
                    Rooms.Invoke();
                });
            }
        }
        public AddRoomViewModel()
        {
            RoomTypes = DatabaseControl.GetRoomTypes();
        }
        public Rooms NewRoom
        {
            get => _newRoom;
            set
            {
                _newRoom = value;
            }
        }
        public RelayCommand AddRoomCommand
        {
            get
            {
                return _addRoom ??
                    (_addRoom = new RelayCommand(obj =>
                    {
                        bool whitespace = String.IsNullOrWhiteSpace(_newRoom.Description);

                        if (_newRoom.Description != null &&
                        selectedRoomType != null && _newRoom.Image != null && whitespace == false)
                        {
                            Rooms room = new Rooms
                            {
                                Description = _newRoom.Description,
                                Status = "Свободен",
                                Image = _newRoom.Image,
                                RoomType_id = selectedRoomType.Id
                            };
                            DatabaseControl.AddRoom(room);
                            Rooms.Invoke();
                        } else
                        {
                            MessageBox.Show("Неправильно введены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                        }
                        
                    }));
            }
        }
        public RelayCommand ImageCommand
        {
            get
            {
                return _image ?? new RelayCommand(obj =>
                {

                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Images (*.jpg, *.png)|*.jpg; *.png; *.JPG; *.PNG";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        _img = openFileDialog;
                        Random random = new Random();
                        int r = random.Next(0, 100000);
                        _newRoom.Image = System.IO.Path.Combine("C:\\Users\\alin4\\Desktop\\учеба\\4 курс\\курсовой проект\\кркр\\images\\", r + _img.SafeFileName);
                        File.Copy(_img.FileName, _newRoom.Image, true);


                        BitmapImage _bitmapImage = new BitmapImage();
                        using (Stream stream = File.OpenRead(_newRoom.Image))
                        {
                            _bitmapImage.BeginInit();
                            _bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            _bitmapImage.StreamSource = stream;
                            _bitmapImage.EndInit();
                        }
                        Image = _bitmapImage;
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
