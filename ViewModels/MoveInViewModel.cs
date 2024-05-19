using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using кркр.Infrastructure;
using Spire.Doc.Documents;
using Spire.Doc;
using Section = Spire.Doc.Section;
using Paragraph = Spire.Doc.Documents.Paragraph;
using кркр.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace кркр.ViewModels
{
    class MoveInViewModel : INotifyPropertyChanged
    {
        public string ClientName { get; set; } = string.Empty;
        public string Passport { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfArrival { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public int RoomId { get; set; }
        public string RoomType { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public ObservableCollection<Bookings>? Bookings { get; set; }
        private RelayCommand _showMainPage;
        private RelayCommand _findBookingByClientCommand;
        private RelayCommand _createDocumentCommand;
        private RelayCommand _moveInCommand;
        private RelayCommand _bookingInfoCommand;
        public delegate void MainHandler();
        public event MainHandler Main;
        private Bookings _selectedBooking;
        public Bookings? SelectedBooking
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
        public void RefreshTable()
        {
            Bookings = null;
            Bookings = DatabaseControl.GetBookingsNotSelected();
            OnPropertyChanged("ClientName");
            OnPropertyChanged("Passport");
            OnPropertyChanged("Phone");
            OnPropertyChanged("DateOfBirth");
            OnPropertyChanged("DateOfArrival");
            OnPropertyChanged("DateOfDeparture");
            OnPropertyChanged("RoomId");
            OnPropertyChanged("RoomType");
            OnPropertyChanged("Info");
            OnPropertyChanged("Bookings");
            OnPropertyChanged("SelectedBooking");
        }
        string textWithParams;
        string headerName;
        string firstPartHeader;
        string secondPartHeader;
        string thirdPartHeader;
        string fourthPartHeader;
        string fifthPartHeader;
        string sixPartHeader;
        string sevenPartHeader;
        string eightPartHeader;
        string firstPartText;
        string secondPartText;
        string threePartText;
        string fourPartText;
        string fivePartText;
        string sixPartText;
        string sevenPartText;
        string recRightText;
        string recLeftText;

        public MoveInViewModel()
        {
            Bookings = DatabaseControl.GetBookingsNotSelected();
            
        }

        public void CreateDocument()
        {
            textWithParams = "ООО ОТЕЛЬ «Отелло», в лице генерального директора Орловой Л.В., " +
            "действующего на основании Устава, именуемый в дальнейшем «Исполнитель, с одной стороны, " +
            $"и {ClientName} «Заказчик», с другой стороны, совместно " +
            "именуемые «Стороны», заключили настоящий договор о нижеследующем:";

            headerName = "ДОГОВОР ОКАЗАНИЯ ГОСТИНИЧНЫХ УСЛУГ";

            firstPartHeader = "1. Предмет договора";
            secondPartHeader = "2. Обязанности сторон";
            thirdPartHeader = "3. Исполнение обязательств";
            fourthPartHeader = "4. Расчеты между сторонами";
            fifthPartHeader = "5. Стоимость услуг и порядок расчетов";
            sixPartHeader = "6. Порядок разрешения споров";
            sevenPartHeader = "7. Дополнительные условия";
            eightPartHeader = "8. Реквизиты сторон";

            firstPartText = "1.1.По настоящему договору Исполнитель обязуется по заданию " +
                "Заказчика в соответствии с условиями настоящего Договора оказать Заказчику гостиничные " +
                "услуги/услуги размещения/ для проживания «Заказчика» и/или лиц, указанных Заказчиком в " +
                "заявке на бронирование номеров исходя из потребностей «Заказчика», изложенных в заявке " +
                "(заказе), и возможностей «Исполнителя», в соответствии со спецификацией, а " +
                "Заказчик обязуется оплатить оказанные гостиничные услуги/услуги размещения, " +
                "в размере и сроки, установленные в настоящем договоре. " +
                "\r\n1.2.Услуги по временному размещению Заказчика оказываются в Отеле «Отелло», " +
                "расположенном по адресу: 443099, г. Екатеринбург ул.Куйбышева,88 " +
                "\r\n1.3 Расселение Заказчика в предоставленные гостиничные номера осуществляется " +
                "согласно «Правилам предоставления гостиничных услуг в Российской Федерации», " +
                "утвержденными Постановлением Правительства Российской Федерации от 18 ноября " +
                "2020 г. N 1853 г., иные дополнительные услуги оказываются в соответствии с " +
                "действующим законодательством Российской Федерации.";

            secondPartText = "2.1.Исполнитель обязуется обеспечить круглосуточное размещение " +
                "и обслуживание Заказчика, направленных последним для проживания в гостинице в " +
                "соответствии с заявкой (заказом) на бронирование. " +
                "\r\n2.2.Заявка на бронирование производится Заказчиком по телефону, либо посредством приложения, указанных в разделе 7 настоящего договора. Она должна включать в себя Ф.И.О.,  данные паспорта или иного документа, удостоверяющего личность Заказчика, дату и время въезда, дату и время выезда, категорию номера " +
                "\r\n2.3.Исполнитель подтверждает бронь в течение " +
                "24 часов получения заявки (заказа) - гарантированное бронирование. Выставление счета на " +
                "оплату является фактом, подтверждающим принятие заявки Заказчика к исполнению. " +
                "\r\n2.4.Заказчик обязан при заключении настоящего договора предоставить гостинице данные " +
                "паспорта или иного документа, удостоверяющего личность (личности) Заказчика, мобильный " +
                "телефон и иные персональные данные. При этом Заказчик даёт согласие гостинице " +
                "на обработку таких персональных данных. Персональные данные могут быть переданы " +
                "работникам гостиницы, ответственным за предоставление гостиничных услуг исключительно " +
                "в целях исполнения данного договора; могут быть переданы сотрудникам УФМС в рамках " +
                "действующего законодательства. В соответствии с Законом РФ «О персональных данных», " +
                "при обработке персональных данных Клиента Заказчика, гостиница обязуется принимать все " +
                "необходимые организационные и технические меры для защиты таких персональных данных от " +
                "неправомерного или случайного доступа к ним, уничтожения, изменения, блокирования, " +
                "копирования, распространения персональных данных, а также иных неправомерных действий. " +
                "\r\n2.5.Заказчик своевременно оплачивает услуги Исполнителя по «Прейскуранту цен на " +
                "проживание», действующему на момент заключения настоящего договора" +
                "\r\n2.6.Исполнитель предоставляет Заказчику Регистрационную карту Гостя, " +
                "где указывается : Ф.И.О., паспортные данные, дату въезда и выезда, номер брони, номер " +
                "комнаты, цена за проживание, число гостей, согласие на обработку персональных данных. " +
                "\r\n2.7.Исполнитель обязан предоставить Заказчику без дополнительной оплаты следующие виды " +
                "услуг: - вызов скорой помощи; - пользование медицинской аптечкой; - доставка в номер " +
                "корреспонденции по ее получении; - побудка к определенному времени; - предоставление кипятка, " +
                "иголок, ниток, одного комплекта посуды и столовых приборов. " +
                "\r\n2.8 Исполнитель отвечает за сохранность вещей Заказчика. В случае обнаружения " +
                "забытых вещей Исполнитель обязан немедленно уведомить об этом владельца вещей.";

            threePartText = "3.1.Порядок оказания услуг по настоящему договору определяется «Исполнителем» " +
                "самостоятельно в соответствии с действующими нормативно-правовыми актами, регулирующими порядок " +
                "оказания гостиничных услуг. " +
                "\r\n3.2.Обеспечить Заказчику предоставление льгот, если такие льготы предусмотрены " +
                "законами и иными нормативными правовыми актами. " +
                "\r\n3.3.Довести до сведения Заказчика перечень " +
                "услуг, которые входят в цену номера (места в номере). ";

            fourPartText = "4.1.Плата за проживание в гостинице взимается в соответствии с единым " +
                "расчетным часом с 12-00 часов текущих суток. Время заезда в гостиницу после 14-00 т" +
                "екущего дня. При размещении до расчетного часа (с 00-00 до 12-00) плата взимается за " +
                "половину суток. При проживании не более суток (24 часов) плата взимается за сутки " +
                "независимо от расчетного часа. При задержке выезда после расчетного часа (с 12-00 до 00-00) " +
                "плата взимается за половину суток. " +
                "\r\n4.2.Дополнительные услуги, оказываемые «Исполнителем», " +
                "не включенные «Заказчиком» в заявку (заказ), оплачиваются Заказчиком." +
                "\r\n4.3.Порядок аннуляции: Отказ Заказчика от забронированных номеров должен быть " +
                "заявлен Исполнителю в письменной форме в следующие сроки: " +
                "\r\n4.3.1.не позднее, чем за 1 (одни) сутки до даты заезда при отмене бронирования 1 (одного) номера; " +
                "\r\n4.3.2 не позднее, чем за 3 (трое) суток до даты заезда при отмене бронирования 2-5 " +
                "(от двух до пяти) номеров; " +
                "\r\n4.3.3. не позднее, чем за 10 (десять) суток до даты заезда " +
                "при отмене бронирования от 6 (от шести) номеров. При соблюдении данных " +
                "сроков отмена бронирования осуществляется без штрафных санкций. При нарушении сроков " +
                "уведомления стоимость отмены бронирования составляет стоимость одних суток проживания. " +
                "\r\n4.4. Дополнительные услуги, оказываемые «Исполнителем», не включенные «Заказчиком» в заявку (заказ)," +
                " оплачивается Заказчиком. ";

            fivePartText = "5.1. Стоимость услуг, установленная Исполнителем для Заказчика, определяется " +
                "Приложением 1. Цены, указанные в Приложении №1 к настоящему Договору, являются окончательными и " +
                "могут быть изменены только по соглашению сторон. " +
                "\r\n5.2. Исполнитель использует упрощенную систему налогообложения и не является плательщиком НДС. " +
                "\r\n5.3. Тарифы, указанные в Приложении №1, не облагаются НДС на основании Главы 26.2 НК РФ. " +
                "\r\n5.4. Тарифы, указанные в Приложении №1, включают завтрак по системе «шведский стол». " +
                "\r\n5.5. Оплата Заказчиком оказываемых Исполнителем услуг осуществляется Заказчиком по ценам, " +
                "установленным в Приложении №1 настоящего Договора, любой формой оплаты, предусмотренной Законодательством " +
                "РФ, не позднее даты выезда. \r\n5.6. Забронированные номера должны быть оплачены в размере 50% от общей " +
                "стоимости проживания Заказчиком не позднее 10 дней до даты заезда. " +
                "\r\n5.7. Забронированные номера должны быть оплачены в размере 100% от общей стоимости проживания " +
                "Заказчиком не позднее 3 дней до даты заезда. ";

            sixPartText = "6.1.Споры и разногласия, возникшие в связи с неисполнением обязательств по настоящему " +
                "договору, разрешаются сторонами путем переговоров. " +
                "\r\n6.2. В случае невозможности разрешения споров по соглашению сторон спор рассматривается судом " +
                "в установленном законодательством порядке. ";

            sevenPartText = "7.1. Настоящий договор вступает в силу с момента подписания и до полного исполнения " +
                "Сторонами своих обязательств. " +
                "\r\n7.2. Стороны имеют право по взаимному соглашению сторон досрочно расторгнуть или изменить " +
                "настоящий договор. " +
                "\r\n7.3. Все изменения и дополнения к настоящему договору осуществляются путем " +
                "заключения дополнительного соглашения, подписанного уполномоченными на то представителями сторон, " +
                "являющегося неотъемлемой частью договора. " +
                "\r\n7.4. Соглашение о расторжении настоящего договора заключается в письменной форме и " +
                "подписывается уполномоченными представителями каждой из сторон." +
                "\r\n7.5. Настоящий договор составлен на русском языке в двух экземплярах, имеющих " +
                "равную юридическую силу, по одному экземпляру каждой из договаривающихся сторон. ";

            recLeftText = "«ИСПОЛНИТЕЛЬ»:" +
                "\r\nООО ОТЕЛЬ «Отелло»" +
                "\r\nИНН 6317067069 " +
                "\r\nКПП 631701001 " +
                "\r\nЮр. адрес: 443099, г. Екатеринбург, ул. Куйбышева,88 " +
                "\r\nФакт. адрес: 443099, г. Екатеринбург, ул. Куйбышева,88 " +
                "\r\nР/сч 40702810254400030049 Поволжский банк ОАО «Сбербанка России» г. Екатеринбурга " +
                "\r\nБИК 043601607 " +
                "\r\nК/сч. 3010181020000000607 " +
                "\r\nОГРН 10663170365234 " +
                "\r\nЕГРЮЛ 2106317003010 Межрайонная инспекция Федеральной налоговой службы №18 по Свердловской Области" +
                "\r\nТел. + 7 (846) 333-70-31 " +
                "\r\nФакс. +7 (846) 332-36-15 " +
                "\r\nMail: info@hotelgraforlov.ru" +
                "\r\nГенеральный директор _______Орлова Л.В.";

            recRightText = "«ЗАКАЗЧИК»" +
                "\r\nПаспортные данные (серия и номер):" +
                $"\r\n{Passport}" +
                $"\r\nТел.: {Phone} \r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n" +
                $"\r\n___________{ClientName}";
        }
        public void BookingInfoCommand()
        {
            ClientName = SelectedBooking.UsersEntity.ClientsEntity.FIO;
            Passport = SelectedBooking.UsersEntity.ClientsEntity.Passport;
            Phone = SelectedBooking.UsersEntity.ClientsEntity.Phone;
            DateOfBirth = SelectedBooking.UsersEntity.ClientsEntity.DateOfBirth;
            DateOfArrival = SelectedBooking.DateOfArrival;
            DateOfDeparture = SelectedBooking.DateOfDeparture;
            RoomId = SelectedBooking.Room_id;
            RoomType = SelectedBooking.RoomsEntity.RoomTypesEntity.RoomType;
            //RefreshTable();
            OnPropertyChanged("ClientName");
            OnPropertyChanged("Passport");
            OnPropertyChanged("Phone");
            OnPropertyChanged("DateOfBirth");
            OnPropertyChanged("DateOfArrival");
            OnPropertyChanged("DateOfDeparture");
            OnPropertyChanged("RoomId");
            OnPropertyChanged("RoomType");
            OnPropertyChanged("Info");
            OnPropertyChanged("Bookings");
            OnPropertyChanged("SelectedBooking");
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
        public RelayCommand FindBookingByClientCommand
        {
            get
            {
                return _findBookingByClientCommand ?? new RelayCommand(obj =>
                {
                    Bookings = DatabaseControl.GetBookingsByClientName(Search);
                    OnPropertyChanged("Bookings");
                    
                });            
            }
        }
        public RelayCommand CreateDocumentCommand
        {
            get
            {
                return _createDocumentCommand ?? new RelayCommand(obj =>
                {
                    Document doc = new Document();
                    CreateDocument();
                    Section section = doc.AddSection();
                    section.PageSetup.Margins.Top = 60;
                    section.PageSetup.Margins.Bottom = 60;
                    section.PageSetup.Margins.Left = 60;
                    section.PageSetup.Margins.Right = 60;

                    Paragraph pHeader = section.AddParagraph();
                    pHeader.AppendText(headerName);
                    pHeader.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

                    Spire.Doc.Table pTable = section.AddTable(true);
                    pTable.ResetCells(1, 2);
                    pTable.TableFormat.Borders.BorderType = BorderStyle.None;
                    Spire.Doc.TableRow FirstColumn = pTable.Rows[0];
                    Paragraph pTableLeftColumn = FirstColumn.Cells[0].AddParagraph();
                    pTableLeftColumn.AppendText("г. Екатеринбург");
                    Spire.Doc.TableRow SecondColumn = pTable.Rows[0];
                    Paragraph pTableRightColumn = FirstColumn.Cells[1].AddParagraph();
                    pTableRightColumn.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                    pTableRightColumn.AppendText(DateTime.Now.ToShortDateString());

                    section.AddParagraph();

                    Paragraph pTextWithParams = section.AddParagraph();
                    pTextWithParams.AppendText(textWithParams);

                    Paragraph pFirstPartHeader = section.AddParagraph();
                    pFirstPartHeader.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pFirstPartHeader.AppendText(firstPartHeader);

                    Paragraph pFirstPartText = section.AddParagraph();
                    pFirstPartText.AppendText(firstPartText);

                    Paragraph pSecondPartHeader = section.AddParagraph();
                    pSecondPartHeader.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pSecondPartHeader.AppendText(secondPartHeader);

                    Paragraph pSecondPartText = section.AddParagraph();
                    pSecondPartText.AppendText(secondPartText);

                    Paragraph pThreePartHeader = section.AddParagraph();
                    pThreePartHeader.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pThreePartHeader.AppendText(thirdPartHeader);

                    Paragraph pThreePartText = section.AddParagraph();
                    pThreePartText.AppendText(threePartText);

                    Paragraph pFourPartHeader = section.AddParagraph();
                    pFourPartHeader.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pFourPartHeader.AppendText(fourthPartHeader);

                    Paragraph pFourPartText = section.AddParagraph();
                    pFourPartText.AppendText(fourPartText);

                    Paragraph pFivePartHeader = section.AddParagraph();
                    pFivePartHeader.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pFivePartHeader.AppendText(fifthPartHeader);

                    Paragraph pFivePartText = section.AddParagraph();
                    pFivePartText.AppendText(fivePartText);

                    Paragraph pSixPartHeader = section.AddParagraph();
                    pSixPartHeader.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pSixPartHeader.AppendText(sixPartHeader);

                    Paragraph pSixPartText = section.AddParagraph();
                    pSixPartText.AppendText(sixPartText);

                    Paragraph pSevenPartHeader = section.AddParagraph();
                    pSevenPartHeader.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pSevenPartHeader.AppendText(sevenPartHeader);

                    Paragraph pSevenPartText = section.AddParagraph();
                    pSevenPartText.AppendText(sevenPartText);

                    Spire.Doc.Table pTableRec = section.AddTable(true);
                    pTableRec.ResetCells(1, 2);
                    Spire.Doc.TableRow FirstRow = pTableRec.Rows[0];
                    Paragraph pTableLeftColumnRec = FirstRow.Cells[0].AddParagraph();
                    pTableLeftColumnRec.AppendText(recLeftText);
                    Paragraph pTableRightColumnRec = FirstRow.Cells[1].AddParagraph();
                    pTableRightColumnRec.AppendText(recRightText);

                    doc.SaveToFile("Договор оказания гостиничных услуг.docx", FileFormat.Docx);

                    System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = "Договор оказания гостиничных услуг.docx", UseShellExecute = true });
                });            
            }
        }
        public RelayCommand MoveInCommand
        {
            get
            {
                return _moveInCommand ?? new RelayCommand(obj =>
                {
                    var booking = new Bookings()
                    {
                        Id = SelectedBooking.Id,
                        Room_id = SelectedBooking.Room_id,
                        Violation_id = SelectedBooking.Violation_id,
                        User_id = SelectedBooking.User_id,
                        DateOfArrival = SelectedBooking.DateOfArrival.ToUniversalTime(),
                        DateOfDeparture = SelectedBooking.DateOfDeparture.ToUniversalTime(),
                        Status = "Заселен"
                    };

                    DatabaseControl.UpdateBooking(booking);
                    Main.Invoke();
                });            
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
