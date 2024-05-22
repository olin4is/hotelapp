using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace кркр.Models
{
    public class Users
    {
        private int _id;
        private int _role_id;
        private string _login;
        private string _password;
        private string _fio;
        private string? _phone;
        private string? _passport;
        private DateTime _dateOfBirth;
        public Roles _rolesEntity;
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
        [ForeignKey("RolesEntity")]
        public int Role_id
        {
            get { return _role_id; }
            set
            {
                _role_id = value;
                OnPropertyChanged("RoleId");
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
        public string FIO
        {
            get { return _fio; }
            set
            {
                _fio = value;
                OnPropertyChanged("FIO");
            }
        }

        public string? Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public string? Passport
        {
            get { return _passport; }
            set
            {
                _passport = value;
                OnPropertyChanged("Pasport");
            }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }
        public Roles RolesEntity
        {
            get { return _rolesEntity; }
            set
            {
                _rolesEntity = value;
                OnPropertyChanged("RolesEntity");
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
