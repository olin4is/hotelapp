using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace кркр.Models
{
    public class RoomTypes
    {
        private int _id;
        private decimal _price;
        private string _roomType;
        private List<Rooms> _roomsEntity;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }
        public string RoomType
        {
            get { return _roomType; }
            set
            {
                _roomType = value;
                OnPropertyChanged("RoomType");
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
