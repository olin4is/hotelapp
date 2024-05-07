using System;
using System.Collections.Generic;
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
    public class AddViolationViewModel : INotifyPropertyChanged
    {
        private Violations _newViolation = new Violations();
        private RelayCommand _showViolationsPage;
        public delegate void ViolationsHandler();
        public event ViolationsHandler Violations;
        private RelayCommand _addViolation;
        public RelayCommand ShowViolationsPageCommand
        {
            get
            {
                return _showViolationsPage ?? new RelayCommand(obj =>
                {
                    _newViolation.Violation = "";
                    _newViolation.Fine = 0;
                    Violations.Invoke();
                });
            }
        }
        public Violations NewViolation
        {
            get => _newViolation;
            set
            {
                _newViolation = value;
            }
        }
        public RelayCommand AddViolationCommand
        {
            get
            {
                return _addViolation ??
                    (_addViolation = new RelayCommand(obj =>
                    {
                        bool whitespace = String.IsNullOrWhiteSpace(_newViolation.Violation);
                        if (_newViolation.Fine > 0 && _newViolation.Violation != null && whitespace == false)
                        {
                            Violations violation = new Violations
                            {
                                Fine = _newViolation.Fine,
                                Violation = _newViolation.Violation
                            };
                            DatabaseControl.AddViolation(violation);
                            Violations.Invoke();
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
