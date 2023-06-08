using HotelBookingManager.domain.dto;
using HotelBookingManager.service.roomType;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelBookingManager.GUI.room
{
    /// <summary>
    /// Interaction logic for RoomCreateWindow.xaml
    /// </summary>
    public partial class RoomCreateWindow : Window
    {
        private Room room;
        private IRoomTypeService roomTypeService;
        private List<RoomType> roomTypes;
        private Dictionary<int, string> roomStatuses;

        public RoomCreateWindow(bool isNewRoom, Room room, IRoomTypeService roomTypeService)
        {
            InitializeComponent();

            this.room = room;
            this.roomTypeService = roomTypeService;
            this.roomStatuses = new Dictionary<int, string>()
            {
                {0, "Свободен"},
                {1, "Забронирован" },
                {2, "Проживание" }
            };


            InitializeLists();

            if (isNewRoom)
            {
                saveButtonText.Text = "Добавить";
            }
            else
            {
                Title = string.Format("Редактирование комнаты {0}", room.Number);
                InitializeWindow();
            }
        }

        private void InitializeLists()
        {
            roomTypes = roomTypeService.GetAllRoomTypes();
            roomTypeInput.ItemsSource = roomTypes;
            roomTypeInput.SelectedIndex = 0;

            roomAvailabilityInput.ItemsSource = roomStatuses;
            roomAvailabilityInput.SelectedIndex = 0;
        }

        private void InitializeWindow()
        {
            roomNumberInput.Text = room.Number.ToString();
            roomTypeInput.SelectedValue = room.TypeId;
            roomCostInput.Text = room.Cost.ToString(CultureInfo.InvariantCulture);
            roomAvailabilityInput.SelectedValue = room.Availability;
            roomDescriptionInput.Text = room.Description;
        }

        private void roomNumberInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void roomCostInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;

            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains("."))
                {
                    approvedDecimalPoint = true;
                }
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
            {
                e.Handled = true;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            room.Number = int.Parse(roomNumberInput.Text);
            room.TypeId = (int)roomTypeInput.SelectedValue;
            room.Cost = decimal.Parse(roomCostInput.Text, NumberFormatInfo.InvariantInfo);
            room.Availability = (int)roomAvailabilityInput.SelectedValue;
            room.Description = roomDescriptionInput.Text;

            DialogResult = true;
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
