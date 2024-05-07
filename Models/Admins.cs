using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace кркр.Models
{
    public class Admins
    {
        private int _id;
        private int _user_id;
        private string _fio;
        private string _phone;
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
        public int User_id
        {
            get { return _user_id; }
            set
            {
                _user_id = value;
                OnPropertyChanged("UserId");
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

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged("Phone");
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

