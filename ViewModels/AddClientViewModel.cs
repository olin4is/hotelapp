using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using кркр.Infrastructure;
using кркр.Models;

namespace кркр.ViewModels
{
    public class AddClientViewModel : INotifyPropertyChanged
    {
        private Users _newUser = new Users();
        private Clients _newClient = new Clients();
        private RelayCommand _showClientsPage;
        public delegate void ClientsHandler();
        public event ClientsHandler Client;
        private RelayCommand _addClient;
        public string Password { get; set; }
        public AddClientViewModel()
        {
            _newClient.FIO = "";
            _newClient.Phone = "";
            _newClient.Passport = "";
            _newClient.DateOfBirth = DateTime.Today;
            _newUser.Login = "";
            _newUser.Password = "";
        }
        public RelayCommand ShowClientsPageCommand
        {
            get
            {
                return _showClientsPage ?? new RelayCommand(obj =>
                {
                    _newClient.FIO = "";
                    _newClient.Phone = "";
                    _newClient.Passport = "";
                    _newClient.DateOfBirth = DateTime.Today;
                    _newUser.Login = "";
                    _newUser.Password = "";
                    Client.Invoke();
                });
            }
        }
        public Users NewUser
        {
            get => _newUser;
            set
            {
                _newUser = value;
            }
        }
        public Clients NewClient
        {
            get => _newClient;
            set
            {
                _newClient = value;
            }
        }
        public RelayCommand AddClientCommand
        {
            get
            {
                return _addClient ??
                    (_addClient = new RelayCommand(obj =>
                    {
                        if (_newClient.FIO != null && _newClient.Passport != null && _newClient.Phone != null)
                        {
                            int indexPhone = _newClient.Phone.IndexOf('_');
                            int indexPassport = _newClient.Passport.IndexOf("_");
                            if (indexPhone == -1 && indexPassport == -1)
                            {
                                bool whitespacePassport = String.IsNullOrWhiteSpace(_newClient.Passport);
                                bool whitespaceFIO = String.IsNullOrWhiteSpace(_newClient.FIO);
                                string regexFIO = @"([А-ЯЁ][а-яё]+[\-\s]?){3,}";
                                string regexLogin = @"[A-Za-z]+";
                                string regexPassword = @"^\s*$";
                                bool FIO = Regex.IsMatch(_newClient.FIO, @"([А-ЯЁ][а-яё]+[\-\s]?){3,}");
                                bool Login = Regex.IsMatch(_newUser.Login, @"[A-Za-z]+");
                                bool PasswordWhitespace = Password.Contains(' ');
                                

                                if (FIO && Login && !PasswordWhitespace)
                                {
                                    Users user = new Users
                                    {
                                        Login = _newUser.Login,
                                        Password = Password,
                                        Role = "Клиент"
                                    };
                                    DatabaseControl.AddUser(user);
                                    Clients client = new Clients
                                    {
                                        FIO = _newClient.FIO,
                                        Passport = _newClient.Passport,
                                        DateOfBirth = _newClient.DateOfBirth.ToUniversalTime(),
                                        Phone = _newClient.Phone,
                                        User_id = user.Id
                                    };
                                    DatabaseControl.AddClient(client);
                                    _newClient.FIO = "";
                                    _newClient.Phone = "";
                                    _newClient.Passport = "";
                                    _newClient.DateOfBirth = DateTime.Today;
                                    _newUser.Login = "";
                                    _newUser.Password = "";
                                    Client.Invoke();
                                } else
                                {
                                    MessageBox.Show("Неправильно введены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                                }
                            }
                                
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
