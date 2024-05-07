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
    public class UpdateViolationViewModel : INotifyPropertyChanged
    {
        private Violations _violation = new Violations();
        private RelayCommand _showViolationsPage;
        private RelayCommand _updateViolation;
        public delegate void ViolationsHandler();
        public event ViolationsHandler Violations; 
        public int Id { get; set; }
        public string Violation { get; set; }
        public decimal Fine {  get; set; }

        public UpdateViolationViewModel(Violations violation)
        {
            Id = violation.Id;
            Violation = violation.Violation;
            Fine = violation.Fine;
        }
        public RelayCommand ShowViolationsPageCommand
        {
            get
            {
                return _showViolationsPage ?? new RelayCommand(obj =>
                {
                    Violations.Invoke();
                });
            }
        }
        public Violations UpdateViolation
        {
            get => _violation;
            set
            {
                _violation = value;
            }
        }
        public RelayCommand UpdateViolationCommand
        {
            get
            {
                return _updateViolation ??
                    (_updateViolation = new RelayCommand(obj =>
                    {
                        bool whitespace = String.IsNullOrWhiteSpace(Violation);
                        if (Violation != null && Fine > 0 && whitespace == false)
                        {
                            Violations updateViolation = new Violations
                            {
                                Id = Id,
                                Violation = Violation,
                                Fine = Fine

                            };
                            DatabaseControl.UpdateViolation(updateViolation);
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
