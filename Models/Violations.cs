using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace кркр.Models
{
    public class Violations
    {
        private int _id;
        private decimal _fine;
        private string _violation;
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
        public decimal Fine
        {
            get { return _fine; }
            set
            {
                _fine = value;
                OnPropertyChanged("Fine");
            }
        }

        public string Violation
        {
            get { return _violation; }
            set
            {
                _violation = value;
                OnPropertyChanged("Violation");
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
