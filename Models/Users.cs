using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace кркр.Models
{
    public class Users
    {
        private int _id;
        private string _role;
        private string _login;
        private string _password;
        public Clients _clientsEntity;
        public Admins _adminsEntity;
        public List<Bookings> _bookingsEntity;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Role
        {
            get { return _role; }
            set
            {
                _role = value;
                OnPropertyChanged("Role");
            }
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        public Clients ClientsEntity
        {
            get { return _clientsEntity; }
            set
            {
                _clientsEntity = value;
                OnPropertyChanged("ClientsEntity");
            }
        }
        public Admins AdminsEntity
        {
            get { return _adminsEntity; }
            set
            {
                _adminsEntity = value;
                OnPropertyChanged("AdminsEntity");
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
