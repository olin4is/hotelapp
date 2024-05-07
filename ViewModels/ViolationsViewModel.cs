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
using кркр.Views.UserControls;

namespace кркр.ViewModels
{
    public class ViolationsViewModel : INotifyPropertyChanged
    {
        public RelayCommand _showMainPage;
        private RelayCommand _showAddViolationPage;
        private RelayCommand _deleteViolation;
        private RelayCommand _showUpdateViolationPage;
        public delegate void MainHandler();
        public event MainHandler Main;
        public delegate void AddViolationtHandler();
        public event AddViolationtHandler AddViolation;
        public delegate void UpdateViolationHandler(Violations Violation);
        public event UpdateViolationHandler UpdateViolation;
        private ObservableCollection<Violations> _violations;
        public ObservableCollection<Violations> Violations 
        { 
            get => _violations; 
            set
            {
                _violations = value;
                OnPropertyChanged("Violations");
            }
        }
        public Violations selectedViolation { get; set; }
        public ViolationsViewModel()
        {
            Violations = DatabaseControl.GetViolationsForPage();
        }
        public void RefreshTable()
        {
            Violations = null;
            Violations = DatabaseControl.GetViolationsForPage();
            OnPropertyChanged("Violations");
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
        public RelayCommand DeleteViolationCommand
        {
            get
            {
                return _deleteViolation ?? new RelayCommand(obj =>
                {
                    if (selectedViolation != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить нарушение?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            DatabaseControl.DeleteViolation(selectedViolation);
                            RefreshTable();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите нарушение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                });
            }
        }
        public RelayCommand ShowAddViolationPageCommand
        {
            get
            {
                return _showAddViolationPage ?? new RelayCommand(obj =>
                {
                    AddViolation.Invoke();
                });
            }
        }
        public RelayCommand ShowUpdateViolationPageCommand
        {
            get
            {
                return _showUpdateViolationPage ?? new RelayCommand(obj =>
                {
                    if (selectedViolation != null)
                    {
                        UpdateViolation.Invoke(selectedViolation);
                    }
                    else
                    {
                        MessageBox.Show("Выберите нарушение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
