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
using System.Windows.Media.Imaging;
using кркр.Infrastructure;
using кркр.Models;

namespace кркр.ViewModels
{
    public class UpdateRoomViewModel : INotifyPropertyChanged
    {
        private Rooms _room = new Rooms();
        private RelayCommand _showRoomsPage;
        private RelayCommand _image;
        private OpenFileDialog _img;
        public delegate void RoomsHandler();
        public event RoomsHandler Rooms;
        private RelayCommand _updateRoom;
        public int Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public RoomTypes RoomType { get; set; }
        public string ComboBoxRoomType {  get; set; }
        public ObservableCollection<RoomTypes> RoomTypes { get; set; }
        public RoomTypes selectedRoomType { get; set; }
        public UpdateRoomViewModel(Rooms room)
        {
            Id = room.Id;
            Description = room.Description;
            Image = room.Image;

            RoomTypes = DatabaseControl.GetRoomTypes();

            RoomType = DatabaseControl.GetRoomTypeById(room.RoomType_id);

            ComboBoxRoomType = RoomType.RoomType;
        }
        public RelayCommand ShowRoomsPageCommand
        {
            get
            {
                return _showRoomsPage ?? new RelayCommand(obj =>
                {
                    Rooms.Invoke();
                });
            }
        }
        public Rooms UpdateRoom
        {
            get => _room;
            set
            {
                _room = value;
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
                        
                        _room.Image = Path.Combine("C:\\Users\\alin4\\Desktop\\учеба\\4 курс\\курсовой проект\\кркр\\images", _img.SafeFileName);
                        File.Copy(_img.FileName, _room.Image, true);
                    }
                });
            }
        }
        public RelayCommand UpdateRoomCommand
        {
            get
            {
                return _updateRoom ??
                    (_updateRoom = new RelayCommand(obj =>
                    {
                        if (_room.Image != null)
                        {
                            Image = _room.Image;
                        }
                        bool whitespace = String.IsNullOrWhiteSpace(Description);
                       
                        if (Description != null && selectedRoomType != null && whitespace == false)
                        {
                            Rooms room = new Rooms
                            {
                                Id = Id,
                                Description = Description,
                                Image = Image,
                                RoomType_id = selectedRoomType.Id,
                                RoomState_id = 1
                            };
                            DatabaseControl.UpdateRoom(room);
                            Rooms.Invoke();
                        } else
                        {
                            MessageBox.Show("Неправильно введены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
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
