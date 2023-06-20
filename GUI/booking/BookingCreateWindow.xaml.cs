using HotelBookingManager.domain.constatnt;
using HotelBookingManager.domain.dto;
using HotelBookingManager.domain.exception;
using HotelBookingManager.service.client;
using HotelBookingManager.service.room;
using HotelBookingManager.service.roomType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HotelBookingManager.GUI.booking
{
    /// <summary>
    /// Interaction logic for BookingCreateWindow.xaml
    /// </summary>
    public partial class BookingCreateWindow : Window
    {
        private readonly IClientService clientService;
        private readonly IRoomService roomService;
        private readonly IRoomTypeService roomTypeService;
        private Booking booking;
        private List<Client> clients;
        private List<Room> rooms;
        private List<RoomType> roomTypes;
        private Dictionary<int, string> bookingStatuses;
        private Dictionary<int, string> bookingPaymentStatuses;

        public BookingCreateWindow(bool isNewBooking,
            Booking booking,
            IClientService clientService,
            IRoomService roomService,
            IRoomTypeService roomTypeService)
        {
            InitializeComponent();
            this.booking = booking;
            this.clientService = clientService;
            this.roomService = roomService;
            this.roomTypeService = roomTypeService;
            this.bookingStatuses = new Dictionary<int, string>()
            {
                {0, "Ожидает подтверждения" },
                {1, "Подтверждено" },
                {2, "Отменено" }
            };
            this.bookingPaymentStatuses = new Dictionary<int, string>()
            {
                {0,"Ожидает оплаты" },
                {1, "Оплачено" },
                {2, "Ошибка оплаты" }
            };

            InitializeLists();

            if (isNewBooking)
            {
                saveButtonText.Text = "Добавить";
            }
            else
            {
                Title = "Редактирование бронирования";
                InitializeWindow();
            }
        }

        private void InitializeLists()
        {
            InitializeClientList();
            InitializeRoomTypeList();

            statusInput.ItemsSource = bookingStatuses;
            statusInput.SelectedIndex = 0;

            paymentStatusInput.ItemsSource = bookingPaymentStatuses;
            paymentStatusInput.SelectedIndex = 0;
        }

        private void InitializeClientList()
        {
            clients = clientService.GetAllClients();
            clientInput.ItemsSource = clients;
            clientInput.SelectedIndex = 0;
        }

        private void InitializeRoomTypeList()
        {
            roomTypes = roomTypeService.GetAllRoomTypes();
            roomTypeInput.ItemsSource = roomTypes;
            roomTypeInput.SelectedIndex = 0;
        }

        private void InitializeRoomList()
        {
            rooms = roomService.GetRoomsByTypeId((int)roomTypeInput.SelectedValue);
            roomInput.ItemsSource = rooms.Where(room => room.Availability == 0).ToList();
            roomInput.SelectedIndex = 0;
        }

        private void InitializeWindow()
        {
            clientInput.SelectedValue = booking.ClientId;
            roomTypeInput.SelectedValue = booking.RoomTypeId;

            if (booking.RoomId == null)
            {
                isRoomNotAssigned.IsChecked = true;
            }
            else
            {
                roomInput.SelectedValue = booking.RoomId;
            }

            arrivalDateInput.SelectedDate = booking.ArrivalDate;

            departureDateInput.SelectedDate = booking.DepartureDate;

            statusInput.SelectedValue = booking.Status;

            paymentStatusInput.SelectedValue = booking.PaymentStatus;

            additionalInformationInput.Text = booking.AdditionalInformation;
        }

        private int CalculateDurationOfStay()
        {
            DateTime? startDate = arrivalDateInput.SelectedDate;
            DateTime? endDate = departureDateInput.SelectedDate;

            DateTime start = DateTime.Now.Date;
            DateTime end = DateTime.Now.Date;

            if (startDate.HasValue)
            {
                start = startDate.Value;
            }

            if (endDate.HasValue)
            {
                end = endDate.Value;
            }

            TimeSpan difference = end.Subtract(start);
            int days = (int)difference.TotalDays;

            return days;
        }

        private decimal CalculateTotalCost()
        {
            decimal totalCost = 0;
            RoomType selectedRoomType = (RoomType)roomTypeInput.SelectedItem;
            decimal roomTypeCost = selectedRoomType.Cost;
            bool result = int.TryParse(durationOfStayInput.Content.ToString(), out var days);

            if (result)
            {
                totalCost = roomTypeCost * days;
            }

            return totalCost;
        }

        private void DisplayCosts()
        {
            totalCostInput.Content = CalculateTotalCost().ToString("N2");
            dayCostInput.Content = (roomTypeInput.SelectedItem as RoomType).Cost.ToString("N2");
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateFields();
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.UserMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                MarkInvalidFields(ex.Code);
                return;
            }

            booking.ClientId = (int)clientInput.SelectedValue;

            if (isRoomNotAssigned.IsChecked.Value)
            {
                booking.RoomId = null;
            }
            else
            {
                booking.RoomId = (int?)roomInput.SelectedValue;
            }

            booking.RoomTypeId = (int)roomTypeInput.SelectedValue;
            booking.ArrivalDate = arrivalDateInput.SelectedDate.Value;
            booking.DepartureDate = departureDateInput.SelectedDate.Value;
            booking.DurationOfStay = (int)durationOfStayInput.Content;
            booking.AdditionalInformation = additionalInformationInput.Text;
            booking.Status = (int)statusInput.SelectedValue;
            booking.TotalCost = decimal.Parse(totalCostInput.Content.ToString());
            booking.PaymentStatus = (int)paymentStatusInput.SelectedValue;

            if (booking.CreatedAt == DateTime.MinValue)
            {
                booking.CreatedAt = DateTime.Now;
            }

            DialogResult = true;
            Close();
        }

        private void MarkInvalidFields(ExceptionCode code)
        {
            clientInput.BorderBrush = new SolidColorBrush(Color.FromArgb(0x89, 0x00, 0x00, 0x00));
            roomInput.BorderBrush = new SolidColorBrush(Color.FromArgb(0x89, 0x00, 0x00, 0x00));
            arrivalDateInput.BorderBrush = new SolidColorBrush(Color.FromArgb(0x89, 0x00, 0x00, 0x00));
            departureDateInput.BorderBrush = new SolidColorBrush(Color.FromArgb(0x89, 0x00, 0x00, 0x00));

            if (code == ExceptionCode.INVALID_CLIENT)
            {
                clientInput.BorderBrush = Brushes.Red;
            }
            else if (code == ExceptionCode.INVALID_ROOM)
            {
                roomInput.BorderBrush = Brushes.Red;
            }
            else if (code == ExceptionCode.INVALID_ARRIVAL_DATE)
            {
                arrivalDateInput.BorderBrush = Brushes.Red;
            }
            else if (code == ExceptionCode.INVALID_DEPARTURE_DATE)
            {
                departureDateInput.BorderBrush = Brushes.Red;
            }
        }

        private void ValidateFields()
        {
            if (clientInput.SelectedValue == null)
            {
                throw new ValidationException(ExceptionCode.INVALID_CLIENT,
                    "Несуществующий клиент. Пожалуйста, выберите клиента из списка или добавьте нового клиента.");
            }

            if (!isRoomNotAssigned.IsChecked.Value && roomInput.SelectedValue == null)
            {
                throw new ValidationException(ExceptionCode.INVALID_ROOM,
                    "Несуществующая комната. Пожалуйста, выберите комнату из списка или добавьте новую комнату.");
            }

            if (!arrivalDateInput.SelectedDate.HasValue)
            {
                throw new ValidationException(ExceptionCode.INVALID_ARRIVAL_DATE,
                    "Пожалуйста, введите дату приезда.");
            }

            if (!departureDateInput.SelectedDate.HasValue)
            {
                throw new ValidationException(ExceptionCode.INVALID_DEPARTURE_DATE,
                    "Пожалуйста, введите дату отъезда.");
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void roomTypeInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InitializeRoomList();
            DisplayCosts();
        }

        private void isRoomNotAssigned_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            bool isChecked = checkBox.IsChecked ?? false; // Получаем текущее состояние флажка

            // Делайте что-то при изменении состояния CheckBox
            if (isChecked)
            {
                // Флажок установлен
                roomInput.IsEnabled = false;
                roomInput.SelectedIndex = -1;
            }
            else
            {
                // Флажок снят
                roomInput.IsEnabled = true;
                roomInput.SelectedIndex = 0;
            }
        }

        private void departureDateInput_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = departureDateInput.SelectedDate;
            if (selectedDate.HasValue)
            {
                if (!arrivalDateInput.SelectedDate.HasValue)
                {
                    arrivalDateInput.SelectedDate = DateTime.Now.Date;
                }

                int days = CalculateDurationOfStay();

                if (days < 0)
                {
                    MessageBox.Show("Даты проживания введены некорректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    departureDateInput.SelectedDate = arrivalDateInput.SelectedDate;
                    durationOfStayInput.Content = 0;
                    return;
                }

                durationOfStayInput.Content = days;
                DisplayCosts();
            }
        }

        private void arrivalDateInput_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = arrivalDateInput.SelectedDate;
            if (selectedDate.HasValue)
            {
                if (!departureDateInput.SelectedDate.HasValue)
                {
                    departureDateInput.SelectedDate = DateTime.Now.Date.AddDays(7);
                }

                int days = CalculateDurationOfStay();

                if (days < 0)
                {
                    MessageBox.Show("Даты проживания введены некорректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    arrivalDateInput.SelectedDate = departureDateInput.SelectedDate;
                    durationOfStayInput.Content = 0;
                    return;
                }

                durationOfStayInput.Content = days;
                DisplayCosts();
            }
        }
    }
}
