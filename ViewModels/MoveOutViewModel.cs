using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using кркр.Infrastructure;
using кркр.Models;

namespace кркр.ViewModels
{
    public class MoveOutViewModel : INotifyPropertyChanged
    {
        private RelayCommand _showMainPage;
        public delegate void MainHandler();
        public event MainHandler Main;
        
        private RelayCommand _moveOutCommand;
        private RelayCommand _createDocumentCommand;
        private RelayCommand _findBookingByClientCommand;

        public delegate void AddClientHandler();
        public event AddClientHandler AddClient;
        public delegate void UpdateClientHandler(Clients clients);
        public event UpdateClientHandler UpdateClient;

        public ObservableCollection<Bookings> Bookings { get; set; }
        public ObservableCollection<Violations> Violations { get; set; }

        public string _fio;
        public string FIO { get; set; }
        public DateTime DateOfArrival { get; set; }
        public DateTime DateOfDeparture {  get; set; }
        public string ViolationText {  get; set; }
        public double FineText { get; set; }
        public double Summary { get; set; }
        public string Price { get; set; }
        public int RoomId { get; set; }
        public string RoomType {  get; set; }

        public MoveOutViewModel()
        {
            Bookings = DatabaseControl.GetBookingsSelected();
            Violations = DatabaseControl.GetViolations();
        }
        public void RefreshTable()
        {
            Bookings = null;
            Bookings = DatabaseControl.GetBookingsSelected();
            Violations = null;
            Violations = DatabaseControl.GetViolations();
            double a = (DateOfDeparture - DateOfArrival).TotalDays;
            double b = Convert.ToDouble(Price);
            if (a == 0)
            {
                Summary = b + FineText;
            } else
            {
                Summary = (b * a) + FineText;
            }
            ChangeProperties();
        }

        private void ChangeProperties()
        {
            OnPropertyChanged("Bookings");
            OnPropertyChanged("Violations");
            OnPropertyChanged("FIO");
            OnPropertyChanged("DateOfArrival");
            OnPropertyChanged("DateOfDeparture");
            OnPropertyChanged("ViolationText");
            OnPropertyChanged("FineText");
            OnPropertyChanged("Summary");
            OnPropertyChanged("RoomType");
            OnPropertyChanged("RoomId");
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
        
        private Bookings _selectedBooking = new Bookings();
        public Bookings SelectedBooking
        {
            get => _selectedBooking;
            set
            {
                if (value != null)
                {
                    _selectedBooking = value;
                    BookingInfoCommand();
                }
                    
                OnPropertyChanged("SelectedBooking");
            }
        }

        private string _search;
        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                OnPropertyChanged("Search");
            }
        }

        public void BookingInfoCommand()
        {
            FIO = _selectedBooking.UsersEntity.ClientsEntity.FIO;
            DateOfArrival = _selectedBooking.DateOfArrival;
            DateOfDeparture = _selectedBooking.DateOfDeparture;
            Price = _selectedBooking.RoomsEntity.RoomTypesEntity.Price.ToString();
            RoomId = _selectedBooking.Room_id;
            RoomType = _selectedBooking.RoomsEntity.RoomTypesEntity.RoomType;
            double a = (DateOfDeparture - DateOfArrival).TotalDays;
            double b = Convert.ToDouble(Price);
            Summary = (b * a) + FineText;

            ChangeProperties();
        }
        private List<Violations> violationsList = new List<Violations>();
        public List<Violations> ViolationsList 
        { 
            get => violationsList; 
            set
            {
                violationsList = value;
                ViolationInfoCommand();
                OnPropertyChanged("ViolationsList");
            }
        } 

        public void ViolationInfoCommand()
        {
            double fine = 0;
            foreach (var v in violationsList)
            {
                fine += Convert.ToDouble(v.Fine);
                if (v.Violation == "Нет нарушений")
                {
                    fine = 0;
                    break;
                }
            }
            FineText = fine;

            double a = (DateOfDeparture - DateOfArrival).TotalDays;
            double b = Convert.ToDouble(Price);
            if (a == 0)
            {
                Summary = b + FineText;
            }
            else
            {
                Summary = (b * a) + FineText;
            }

            ChangeProperties();
        }

        public RelayCommand FindBookingByClientCommand
        {
            get
            {
                return _findBookingByClientCommand ?? new RelayCommand(obj =>
                {
                    Bookings = DatabaseControl.GetBookingsByClientNameSelected(Search);
                    OnPropertyChanged("Bookings");

                });
            }
        }

        public RelayCommand MoveOutCommand
        {
            get
            {
                return _moveOutCommand ?? new RelayCommand(obj =>
                {
                    if (_selectedBooking != null 
                        && _selectedBooking.Id != 0 
                        && violationsList.Count > 0)
                    {
                        var violationId = violationsList.Any(x => x.Violation == "Нет нарушений")
                                          ? 5
                                          : violationsList.First().Id;

                        Bookings booking = new Bookings 
                        {
                            Id = _selectedBooking.Id,
                            Room_id = _selectedBooking.Room_id,
                            Violation_id = violationId,
                            DateOfArrival = _selectedBooking.DateOfArrival.ToUniversalTime(),
                            DateOfDeparture = _selectedBooking.DateOfDeparture.ToUniversalTime(),
                            User_id = _selectedBooking.User_id,
                            Status = "Выселен"
                        };

                        DatabaseControl.UpdateBooking(booking);
                        Main.Invoke();
                    } else
                    {
                        MessageBox.Show("Выберите нарушение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    
                });
            }
        }
        public Document CreateDocument()
        {
            string header = "\r\nо ";
            foreach (var v in violationsList)
            {
                switch (v.Id)
                {
                    case 1:
                        header = header + "причинении вреда номеру от курения, ";
                        break;
                    case 2:
                        header = header + "потере имущества, ";
                        break;
                    case 3:
                        header = header + "порче имущества, ";
                        break;
                    case 4:
                        header = header + "нарушении порядка, ";
                        break;
                    default:
                        break;
                }
            }
            header = header.Remove(header.Length - 2);

            string hotelName = "Отель «Отелло»";
            string headerName = "АКТ" + header +
                $"\r\nот {DateTime.Today.ToString("dd.MM.yyyy")} г.";
            string firstPartText = $"Настоящий акт составлен о том, что в номере №{_selectedBooking.Room_id} ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________";
            string secondPartText = $"Ущерб нанесён гражданином {_selectedBooking.UsersEntity.ClientsEntity.FIO}";
            string thirdPartText = $"Всего на сумму: {FineText} руб.";
            string fourPartText1 = $"С актом ознакомлен  _______________ {_selectedBooking.UsersEntity.ClientsEntity.FIO}";
            string fourPartText2 = "                                                     (подпись)                (Ф.И.О. причинившего ущерб)";
            string fivePartText1 = $"С гр. {_selectedBooking.UsersEntity.ClientsEntity.FIO} получено: _____________________________";
            string fivePartText2 = "                   (Фамилия, имя, отчество)                                           (сумма прописью)";
            string sixPartText1 = $"Принял: Администратор {Session.FIO} _______________";
            string sixPartText2 = "                       (Должность, фамилия, имя, отчество)                          (подпись)";

            Document doc = new Document();
            Section section = doc.AddSection();
            section.PageSetup.Margins.Top = 60;
            section.PageSetup.Margins.Bottom = 60;
            section.PageSetup.Margins.Left = 60;
            section.PageSetup.Margins.Right = 60;
            
            Paragraph pHeaderHotel = section.AddParagraph();
            TextRange range = pHeaderHotel.AppendText(hotelName);
            range.CharacterFormat.FontSize = 14;

            Paragraph pHeader = section.AddParagraph();
            range = pHeader.AppendText(headerName);
            pHeader.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            range.CharacterFormat.FontSize = 14;

            section.AddParagraph();

            Paragraph pFirstPartText = section.AddParagraph();
            range = pFirstPartText.AppendText(firstPartText);
            range.CharacterFormat.FontSize = 14;

            section.AddParagraph();

            Paragraph pSecondPartText = section.AddParagraph();
            range = pSecondPartText.AppendText(secondPartText);
            range.CharacterFormat.FontSize = 14;

            Paragraph pThirdPartText = section.AddParagraph();
            range = pThirdPartText.AppendText(thirdPartText);
            range.CharacterFormat.FontSize = 14;

            section.AddParagraph();

            Paragraph pFourPartText1 = section.AddParagraph();
            range = pFourPartText1.AppendText(fourPartText1);
            range.CharacterFormat.FontSize = 14;

            Paragraph pFourPartText2 = section.AddParagraph();
            range = pFourPartText2.AppendText(fourPartText2);
            range.CharacterFormat.FontSize = 11;

            section.AddParagraph();

            Paragraph pFivePartText1 = section.AddParagraph();
            range = pFivePartText1.AppendText(fivePartText1);
            range.CharacterFormat.FontSize = 14;

            Paragraph pFivePartText2 = section.AddParagraph();
            range = pFivePartText2.AppendText(fivePartText2);
            range.CharacterFormat.FontSize = 11;

            section.AddParagraph();

            Paragraph pSixPartText1 = section.AddParagraph();
            range = pSixPartText1.AppendText(sixPartText1);
            range.CharacterFormat.FontSize = 14;

            Paragraph pSixPartText2 = section.AddParagraph();
            range = pSixPartText2.AppendText(sixPartText2);
            range.CharacterFormat.FontSize = 11;

            return doc;
        }
        public RelayCommand CreateDocumentCommand
        {
            get
            {
                return _createDocumentCommand ?? new RelayCommand(obj =>
                {
                    Document doc = CreateDocument();
                    doc.SaveToFile("Акт о нарушении.docx", FileFormat.Docx);

                    Process.Start(new ProcessStartInfo { FileName = "Акт о нарушении.docx", UseShellExecute = true });
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
