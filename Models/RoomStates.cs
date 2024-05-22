using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace кркр.Models
{
    public class RoomStates
    {
        private int _id;
        private string _state;
        public List<Rooms> _roomsEntity;

        public int Id
        {
            get => _id; 
            set 
            { 
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        public string State
        {
            get => _state; 
            set 
            { 
                _state = value;
                OnPropertyChanged("State");
            }
        }
        public List<Rooms> RoomsEntity
        {
            get { return _roomsEntity; }
            set
            {
                _roomsEntity = value;
                OnPropertyChanged("RoomsEntity");
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
