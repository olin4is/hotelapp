using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using кркр.Infrastructure;
using кркр.Models;
using System.Windows;

namespace кркр.ViewModels
{
    public class UpdateClientViewModel : INotifyPropertyChanged
    {
        private Clients _client = new Clients();
        private bool _status;
        private RelayCommand _showClientsPage;
        private RelayCommand _getClientStatus;
        public delegate void ClientsHandler();
        public event ClientsHandler Client;
        private RelayCommand _updateClient;
        public int User_Id { get; set; }
        public string FIO { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone {  get; set; }
        public bool Check { get; set; }
        public string Passport { get; set; }

        public UpdateClientViewModel(Clients client) { 
            User_Id = client.User_id;
            FIO = client.FIO;
            DateOfBirth = client.DateOfBirth;
            Phone = client.Phone;
            Passport = client.Passport;
           
        }
        public RelayCommand ShowClientsPageCommand
        {
            get
            {
                return _showClientsPage ?? new RelayCommand(obj =>
                {
                    Client.Invoke();
                });
            }
        }
        public Clients UpdateClient
        {
            get => _client;
            set
            {
                _client = value;
            }
        }
        public RelayCommand UpdateClientCommand
        {
            get
            {
                return _updateClient ??
                    (_updateClient = new RelayCommand(obj =>
                    {
                        bool whitespaceFIO = String.IsNullOrWhiteSpace(FIO);
                        bool whitespacePassport = String.IsNullOrWhiteSpace(Passport);
                        bool whitespacePhoe = String.IsNullOrWhiteSpace(Phone);
                        if (FIO != null && Passport != null && Phone != null &&
                        whitespaceFIO == false && whitespacePassport == false && whitespacePhoe == false)
                        {
                            string regexFIO = @"^([A-Za-zа-яА-Я]+(\\s[A-Za-zа-яА-Я]+)*)|(\\d+(\\s\\d+)*)$";
                            
                            if (Regex.IsMatch(FIO, regexFIO))
                            {
                                Clients client = DatabaseControl.GetClientById(User_Id);
                                Clients updateClient = new Clients
                                {
                                    Id = client.Id,
                                    FIO = FIO,
                                    Passport = Passport,
                                    DateOfBirth = DateOfBirth.ToUniversalTime(),
                                    Phone = Phone,
                                    User_id = User_Id
                            
                                };
                                DatabaseControl.UpdateClient(updateClient);
                                Client.Invoke();
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
