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
        private RelayCommand _showAuthorizationPage;
        private RelayCommand _regUser;
        public delegate void AuthorizationHandler();
        public event AuthorizationHandler Register;
        public string Password;
        public RegistrationViewModel()
        {
            _newUser.DateOfBirth = DateTime.Today;
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
                        if (_newUser.FIO != null && _newUser.Passport != null && _newUser.Phone != null &&
                        _newUser.Login != null && Password != null)
                        {
                            int indexPhone = _newUser.Phone.IndexOf('_');
                            int indexPassport = _newUser.Passport.IndexOf("_");
                            if (indexPhone == -1 && indexPassport == -1)
                            {
                                var fio = Regex.IsMatch(_newUser.FIO, regexFIO);
                                var login = Regex.IsMatch(_newUser.Login, regexLogin);
                                var date = !Regex.IsMatch(_newUser.DateOfBirth.ToString(), regexDate);
                                var phone = !Regex.IsMatch(_newUser.Phone, regexPassword);
                                var passport = !Regex.IsMatch(_newUser.Passport, regexPassword);
                                if (fio && login && date && phone && passport)
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
                                    bool exist = DatabaseControl.CheckUser(user);
                                    if (exist)
                                    {
                                        MessageBox.Show("Такой пользователь существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                                    } else
                                    {
                                        DatabaseControl.AddUser(user);
                                        
                                        _newUser.FIO = "";
                                        _newUser.Phone = "";
                                        _newUser.Passport = "";
                                        _newUser.DateOfBirth = DateTime.Today;
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
