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
        private RelayCommand _showClientsPage;
        public delegate void ClientsHandler();
        public event ClientsHandler Client;
        private RelayCommand _addClient;
        public string Password { get; set; }
        public AddClientViewModel()
        {
            SetDefault();
        }
        private void SetDefault()
        {
            _newUser.FIO = "";
            _newUser.Phone = "";
            _newUser.Passport = "";
            _newUser.DateOfBirth = DateTime.Today;
            _newUser.Login = "";
            _newUser.Password = "";
        }
        public RelayCommand ShowClientsPageCommand
        {
            get
            {
                return _showClientsPage ?? new RelayCommand(obj =>
                {
                    SetDefault();
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
        public RelayCommand AddClientCommand
        {
            get
            {
                return _addClient ??
                    (_addClient = new RelayCommand(obj =>
                    {
                        if (_newUser.FIO != null && _newUser.Passport != null && _newUser.Phone != null)
                        {
                            int indexPhone = _newUser.Phone.IndexOf('_');
                            int indexPassport = _newUser.Passport.IndexOf("_");
                            if (indexPhone == -1 && indexPassport == -1)
                            {
                                bool whitespacePassport = String.IsNullOrWhiteSpace(_newUser.Passport);
                                bool whitespaceFIO = String.IsNullOrWhiteSpace(_newUser.FIO);
                                string regexFIO = @"([А-ЯЁ][а-яё]+[\-\s]?){3,}";
                                string regexLogin = @"[A-Za-z]+";
                                string regexPassword = @"^\s*$";
                                bool FIO = Regex.IsMatch(_newUser.FIO, @"([А-ЯЁ][а-яё]+[\-\s]?){3,}");
                                bool Login = Regex.IsMatch(_newUser.Login, @"[A-Za-z]+");
                                bool PasswordWhitespace = Password.Contains(' ');
                                

                                if (FIO && Login && !PasswordWhitespace)
                                {
                                    Users user = new Users
                                    {
                                        Login = _newUser.Login,
                                        Password = Password,
                                        FIO = _newUser.FIO,
                                        Passport = _newUser.Passport,
                                        DateOfBirth = _newUser.DateOfBirth.ToUniversalTime(),
                                        Phone = _newUser.Phone,
                                        Role_id = 2
                                    };
                                    DatabaseControl.AddUser(user);
                                    SetDefault();
                                    Client.Invoke();
                                } else
                                {
                                    MessageBox.Show("Неправильно введены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                                }
                            } else
                            {
                                MessageBox.Show("Неправильно введены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
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
