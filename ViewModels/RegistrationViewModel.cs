using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using кркр.Infrastructure;
using кркр.Models;

namespace кркр.ViewModels
{
    public class RegistrationViewModel
    {
        private Users _newUser = new Users();
        private Clients _newClient = new Clients();
        private RelayCommand _showAuthorizationPage;
        private RelayCommand _regUser;
        public delegate void AuthorizationHandler();
        public event AuthorizationHandler Register;
        public string Password;
        public RegistrationViewModel()
        {
            _newClient.DateOfBirth = DateTime.Today;
        }
        public RelayCommand ShowAuthorizationPageCommand
        {
            get
            {
                return _showAuthorizationPage ?? new RelayCommand(obj =>
                {
                    Register.Invoke();
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
        public RelayCommand RegUserCommand
        {
            get
            {
                return _regUser ??
                    (_regUser = new RelayCommand(obj =>
                    {
                        string regexFIO = @"^([A-Za-zа-яА-Я]+(\\s[A-Za-zа-яА-Я]+)*)|(\\d+(\\s\\d+)*)$";
                        string regexLogin = @"[A-Za-z]+";
                        string regexPassword = @"^\s*$";
                        string regexDate = @"\d{4}-\d{2}-\d{2}";
                        if (_newClient.FIO != null && _newClient.Passport != null && _newClient.Phone != null &&
                        _newUser.Login != null && Password != null)
                        {
                            int indexPhone = _newClient.Phone.IndexOf('_');
                            int indexPassport = _newClient.Passport.IndexOf("_");
                            if (indexPhone == -1 && indexPassport == -1)
                            {
                                var fio = Regex.IsMatch(_newClient.FIO, regexFIO);
                                var login = Regex.IsMatch(_newUser.Login, regexLogin);
                                var date = !Regex.IsMatch(_newClient.DateOfBirth.ToString(), regexDate);
                                var phone = !Regex.IsMatch(_newClient.Phone, regexPassword);
                                var passport = !Regex.IsMatch(_newClient.Passport, regexPassword);
                                if (fio && login && date && phone && passport)
                                {
                                    Users user = new Users
                                    {
                                        Login = _newUser.Login,
                                        Password = Password,
                                        Role = "Клиент"
                                    };
                                    bool exist = DatabaseControl.CheckUser(user);
                                    if (exist)
                                    {
                                        MessageBox.Show("Такой пользователь существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                                    } else
                                    {
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
                                        Register.Invoke();
                                    }
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
    }
}
