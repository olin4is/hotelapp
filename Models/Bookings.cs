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
    public class Bookings
    {
        private int _id;
        private int _room_id;
        private int _violation_id;
        private int _user_id;
        private string _status;
        private DateTime _dateOfArrival;
        private DateTime _dateOfDeparture;
        
        private Rooms _roomsEntity;
        private Violations _violationsEntity;
        private Users _usersEntity;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        [ForeignKey("RoomsEntity")]
        public int Room_id
        {
            get { return _room_id; }
            set
            {
                _room_id = value;
                OnPropertyChanged("RoomId");
            }
        }
        [ForeignKey("ViolationsEntity")]
        public int Violation_id
        {
            get { return _violation_id; }
            set
            {
                _violation_id = value;
                OnPropertyChanged("ViolationId");
            }
        }
        [ForeignKey("UsersEntity")]
        public int User_id
        {
            get { return _user_id; }
            set
            {
                _user_id = value;
                OnPropertyChanged("UserId");
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }
        public DateTime DateOfArrival
        {
            get { return _dateOfArrival; }
            set
            {
                _dateOfArrival = value;
                OnPropertyChanged("DateOfArrival");
            }
        }
        public DateTime DateOfDeparture
        {
            get { return _dateOfDeparture; }
            set
            {
                _dateOfDeparture = value;
                OnPropertyChanged("DateOfDeparture");
            }
        }
        public Rooms RoomsEntity
        {
            get { return _roomsEntity; }
            set
            {
                _roomsEntity = value;
                OnPropertyChanged("RoomsEntity");
            }
        }
        public Violations ViolationsEntity
        {
            get { return _violationsEntity; }
            set
            {
                _violationsEntity = value;
                OnPropertyChanged("ViolationsEntity");
            }
        }
        public Users UsersEntity
        {
            get { return _usersEntity; }
            set
            {
                _usersEntity = value;
                OnPropertyChanged("UsersEntity");
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
