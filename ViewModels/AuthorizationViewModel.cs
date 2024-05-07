using System.Windows;
using кркр.Infrastructure;
using кркр.Models;

namespace кркр.ViewModels
{
    public class AuthorizationViewModel
    {
        private Users _user = new Users();
        private RelayCommand _logUser;
        private RelayCommand _showMainPage;
        private RelayCommand _showRegisterPage;
        public delegate void RegisterHandler();
        public event RegisterHandler Authorize;
        public event RegisterHandler Main;
        public string Password;
        public RelayCommand ShowRegisterPageCommand
        {
            get
            {
                return _showRegisterPage ?? new RelayCommand(obj =>
                {
                    Authorize.Invoke();
                });
            }
        }

        public Users User
        {
            get => _user;
            set
            {
                _user = value;
            }
        }

        public RelayCommand ShowMainPageCommand
        {
            get
            {
                return _showMainPage ??
                (_showMainPage = new RelayCommand(obj =>
                {
                    Users user = new Users
                    {
                        Login = _user.Login,
                        Password = Password
                    };
                    bool exist = DatabaseControl.CheckUser(user);
                    if (exist)
                    {
                        Main.Invoke();
                        Users users = DatabaseControl.GetUser(user);

                        Session.Id = users.Id;
                        Session.Role = users.Role;

                        if (users.Role == "Админ")
                        {
                            Admins admin = DatabaseControl.GetAdmin(users.Id);
                            Session.FIO = admin.FIO;
                        } else
                        {
                            Clients client = DatabaseControl.GetClientById(users.Id);
                            Session.FIO = client.FIO;
                        }   
                    } else
                    {
                        MessageBox.Show("Неверный логин или пароль", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }));
            }
        }
    }
}
