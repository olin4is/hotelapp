using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace кркр.Models
{
    public class Rooms
    {
        private int _id;
        private string _description;
        private string _image;
        private int _roomType_id;
        private int _roomState_id;
        private RoomTypes _roomTypesEntity;
        private RoomStates _roomStatesEntity;
        private List<Bookings> _bookingsEntity;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        [ForeignKey("RoomTypesEntity")]
        public int RoomType_id
        {
            get { return _roomType_id; }
            set
            {
                _roomType_id = value;
                OnPropertyChanged("RoomTypeId");
            }
        }   
        [ForeignKey("RoomStatesEntity")]
        public int RoomState_id
        {
            get { return _roomState_id; }
            set
            {
                _roomState_id = value;
                OnPropertyChanged("RoomStateId");
            }
        }   

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }
        public RoomTypes RoomTypesEntity
        {
            get { return _roomTypesEntity; }
            set
            {
                _roomTypesEntity = value;
                OnPropertyChanged("RoomTypesEntity");
            }
        }
        public RoomStates RoomStatesEntity
        {
            get { return _roomStatesEntity; }
            set
            {
                _roomStatesEntity = value;
                OnPropertyChanged("RoomStatesEntity");
            }
        }
        public List<Bookings> BookingsEntity
        {
            get { return _bookingsEntity; }
            set
            {
                _bookingsEntity = value;
                OnPropertyChanged("BookingsEntity");
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
