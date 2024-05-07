using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using кркр.Infrastructure;
using кркр.Models;

namespace кркр.ViewModels
{
    public class ClientsViewModel : INotifyPropertyChanged
    {
        private RelayCommand _showMainPage;
        public delegate void MainHandler();
        public event MainHandler Main;
        
        private RelayCommand _showAddClientPage;
        private RelayCommand _deleteClient;
        private RelayCommand _showUpdateClientPage;
        private RelayCommand _showClientsStatus;
        public delegate void AddClientHandler();
        public event AddClientHandler AddClient;
        public delegate void UpdateClientHandler(Clients clients);
        public event UpdateClientHandler UpdateClient;
        public ObservableCollection<Clients> Clients {  get; set; }
        public Clients selectedClient { get; set; }
        public string SelectedStatus {  get; set; }
        public ClientsViewModel()
        {
            Clients = DatabaseControl.GetAllClients();
            SelectedStatus = "Все";
        }
        public void RefreshTable()
        {
            Clients = null;
            Clients = DatabaseControl.GetAllClients();
            OnPropertyChanged("Clients");
        }
        public RelayCommand ShowMainPageCommand
        {
            get
            {
                return _showMainPage ?? new RelayCommand(obj =>
                {
                    Main.Invoke();
                });
            }
        }
        
        public RelayCommand DeleteClientCommand
        {
            get
            {
                return _deleteClient ?? new RelayCommand(obj =>
                {
                    if (selectedClient != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить клиента?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            Clients client = DatabaseControl.GetClient(selectedClient);
                            DatabaseControl.DeleteClient(client);
                            RefreshTable();
                        }
                    } else
                    {
                        MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                });
            }
        }
        public RelayCommand ShowAddClientPageCommand
        {
            get
            {
                return _showAddClientPage ?? new RelayCommand(obj =>
                {
                    AddClient.Invoke();
                });
            }
        }
        public RelayCommand ShowUpdateClientPageCommand
        {
            get
            {
                return _showUpdateClientPage ?? new RelayCommand(obj =>
                {
                    if (selectedClient != null)
                    {
                        UpdateClient.Invoke(selectedClient);
                    } else
                    {
                        MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                });
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
