﻿using HotelBookingManager.dao.booking;
using HotelBookingManager.dao.client;
using HotelBookingManager.dao.room;
using HotelBookingManager.dao.roomType;
using HotelBookingManager.domain.dto;
using HotelBookingManager.GUI;
using HotelBookingManager.GUI.booking;
using HotelBookingManager.GUI.room;
using HotelBookingManager.service.booking;
using HotelBookingManager.service.client;
using HotelBookingManager.service.room;
using HotelBookingManager.service.roomType;
using HotelBookingManager.util.converter;
using HotelBookingManager.util.db;
using HotelBookingManager.util.statusConverter;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace HotelBookingManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DBConnection connection;
        private List<Client> clients;
        private List<Booking> bookings;
        private List<Room> rooms;
        private IClientService clientService;
        private IRoomService roomService;
        private IRoomTypeService roomTypeService;
        private IBookingService bookingService;

        public MainWindow()
        {
            connection = DBConnection.Instance();
            clientService = new ClientServiceImpl(new ClientDaoImpl(connection));
            roomService = new RoomServiceImpl(new RoomDaoImpl(connection));
            roomTypeService = new RoomTypeServiceImpl(new RoomTypeDaoImpl(connection));
            bookingService = new BookingServiceImpl(new BookingDaoImpl(connection));

            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            refreshClientList();
            refreshRoomList();
            refreshBookingList();
        }

        private void refreshBookingList()
        {
            bookings = bookingService.GetAllBookings();
            bookingDataGrid.ItemsSource = bookings;
        }

        private void refreshClientList()
        {
            clients = clientService.GetAllClients();
            clientDataGrid.ItemsSource = clients;
        }

        private void refreshRoomList()
        {
            rooms = roomService.GetAllRooms();
            roomDataGrid.ItemsSource = rooms;
        }

        private void clientDataGrid_AutoGeneratedColumns(object sender, EventArgs e)
        {
            clientDataGrid.Columns[0].Visibility = Visibility.Hidden;
            clientDataGrid.Columns[1].Header = "ФИО";
            clientDataGrid.Columns[2].Header = "Адрес";
            clientDataGrid.Columns[3].Header = "E-mail";
            clientDataGrid.Columns[4].Header = "Телефон";
        }

        private void roomDataGrid_AutoGeneratedColumns(object sender, EventArgs e)
        {
            roomDataGrid.Columns[0].Visibility = Visibility.Hidden;
            roomDataGrid.Columns[1].Header = "Номер";
            roomDataGrid.Columns[2].Header = "Тип";
            roomDataGrid.Columns[3].Header = "Статус";
            roomDataGrid.Columns[4].Header = "Описание";
            roomDataGrid.Columns[4].Width = 300;

            ApplyCellStyle();
        }

        private void bookingDataGrid_AutoGeneratedColumns(object sender, EventArgs e)
        {
            bookingDataGrid.Columns[0].Visibility = Visibility.Hidden;
            bookingDataGrid.Columns[1].Header = "Имя клиента";
            bookingDataGrid.Columns[2].Header = "Комната";
            bookingDataGrid.Columns[3].Header = "Тип комнаты";
            bookingDataGrid.Columns[4].Header = "С";
            bookingDataGrid.Columns[5].Header = "По";
            bookingDataGrid.Columns[6].Header = "Дней проживания";
            bookingDataGrid.Columns[7].Header = "Доп. информация";
            bookingDataGrid.Columns[8].Header = "Статус заказа";
            bookingDataGrid.Columns[9].Header = "Цена";
            bookingDataGrid.Columns[10].Header = "Статус оплаты";
            bookingDataGrid.Columns[11].Header = "Дата заказа";
        }

        private void roomDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Availability")
            {
                DataGridTextColumn column = e.Column as DataGridTextColumn;
                column.Binding = new Binding("Availability")
                {
                    Converter = new RoomStatusConverter()
                };
            }

            if (e.PropertyName == "TypeId")
            {
                DataGridTextColumn column = e.Column as DataGridTextColumn;
                column.Binding = new Binding("TypeId")
                {
                    Converter = new RoomTypeIdToRoomTypeNameConverter(roomTypeService)
                };
            }
        }

        private void bookingDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Status")
            {
                DataGridTextColumn column = e.Column as DataGridTextColumn;
                column.Binding = new Binding("Status")
                {
                    Converter = new BookingStatusConverter()
                };
            }

            if (e.PropertyName == "PaymentStatus")
            {
                DataGridTextColumn column = e.Column as DataGridTextColumn;
                column.Binding = new Binding("PaymentStatus")
                {
                    Converter = new BookingPaymentStatusConverter()
                };
            }

            if (e.PropertyName == "ClientId")
            {
                DataGridTextColumn column = e.Column as DataGridTextColumn;
                column.Binding = new Binding("ClientId")
                {
                    Converter = new ClientIdToClientNameConverter(clients)
                };
            }

            if (e.PropertyName == "RoomId")
            {
                DataGridTextColumn column = e.Column as DataGridTextColumn;
                column.Binding = new Binding("RoomId")
                {
                    Converter = new RoomIdToRoomNumberConverter(rooms)
                };
            }

            if (e.PropertyName == "RoomTypeId")
            {
                DataGridTextColumn column = e.Column as DataGridTextColumn;
                column.Binding = new Binding("RoomTypeId")
                {
                    Converter = new RoomTypeIdToRoomTypeNameConverter(roomTypeService)
                };
            }

            if (e.PropertyName == "ArrivalDate" || e.PropertyName == "DepartureDate" || e.PropertyName == "CreatedAt")
            {
                DataGridTextColumn column = e.Column as DataGridTextColumn;
                column.Binding = new Binding("ArrivalDate")
                {
                    Converter = new DateConverter()
                };
            }

            if (e.PropertyName == "DepartureDate")
            {
                DataGridTextColumn column = e.Column as DataGridTextColumn;
                column.Binding = new Binding("DepartureDate")
                {
                    Converter = new DateConverter()
                };
            }

            if (e.PropertyName == "CreatedAt")
            {
                DataGridTextColumn column = e.Column as DataGridTextColumn;
                column.Binding = new Binding("CreatedAt")
                {
                    Converter = new DateConverter()
                };
            }
        }

        private void refreshRoomButton_Click(object sender, RoutedEventArgs e)
        {
            refreshRoomList();
        }

        private void refreshClientButton_Click(object sender, RoutedEventArgs e)
        {
            refreshClientList();
        }

        private void refreshBookingButton_Click(object sender, RoutedEventArgs e)
        {
            refreshBookingList();
        }

        private void addClientButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client();

            ClientCreateWindow clientProfileWindow = new ClientCreateWindow(true, client, clientService);
            var result = clientProfileWindow.ShowDialog();

            if (result == true)
            {
                clientService.AddClient(client);
                refreshClientList();
            }
        }

        private void addRoomButton_Click(object sender, RoutedEventArgs e)
        {
            Room room = new Room();

            RoomCreateWindow roomCreateWindow = new RoomCreateWindow(true, room, roomTypeService, roomService);
            bool? result = roomCreateWindow.ShowDialog();

            if (result == true)
            {
                roomService.AddRoom(room);
                refreshRoomList();
            }
        }

        private void editClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (clientDataGrid.SelectedItem != null)
            {
                Client client = (Client)clientDataGrid.SelectedItem;
                ClientCreateWindow clientProfileWindow = new ClientCreateWindow(false, client, clientService);
                var result = clientProfileWindow.ShowDialog();

                if (result == true)
                {
                    clientService.UpdateClient(client);
                    refreshClientList();
                }
            }
        }

        private void editRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (roomDataGrid.SelectedItem != null)
            {
                Room room = (Room)roomDataGrid.SelectedItem;
                RoomCreateWindow roomCreateWindow = new RoomCreateWindow(false, room, roomTypeService, roomService);
                bool? result = roomCreateWindow.ShowDialog();

                if (result == true)
                {
                    roomService.UpdateRoom(room);
                    refreshRoomList();
                }
            }
        }

        private void deleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (clientDataGrid.SelectedItem != null)
            {
                Client client = (Client)clientDataGrid.SelectedItem;
                MessageBoxResult messageBoxResult = MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    clientService.DeleteClientById(client.Id);
                    refreshClientList();
                }
            }
        }

        private void deleteRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (roomDataGrid.SelectedItem != null)
            {
                Room room = (Room)roomDataGrid.SelectedItem;
                MessageBoxResult messageBoxResult = MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    roomService.DeleteRoomById(room.Id);
                    refreshRoomList();
                }
            }
        }

        private void deleteBookingButton_Click(object sender, RoutedEventArgs e)
        {
            if (bookingDataGrid.SelectedItem != null)
            {
                Booking booking = (Booking)roomDataGrid.SelectedItem;
                MessageBoxResult messageBoxResult = MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    bookingService.DeleteBookingById(booking.Id);
                    refreshBookingList();
                }
            }
        }

        private void ApplyCellStyle()
        {
            Style cellStyle = new Style(typeof(DataGridCell));

            // Установите привязку фона ячейки к свойству Status
            cellStyle.Setters.Add(new Setter(BackgroundProperty, new Binding("Availability")
            {
                Converter = new StatusToBackgroundConverter()
            }));

            cellStyle.Setters.Add(new Setter(VerticalContentAlignmentProperty, VerticalAlignment.Center));
            cellStyle.Setters.Add(new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
            cellStyle.Setters.Add(new Setter(ForegroundProperty, Brushes.Black));


            DataGridColumn column = roomDataGrid.Columns[3];
            column.CellStyle = cellStyle;
        }

        private void addBookingButton_Click(object sender, RoutedEventArgs e)
        {
            Booking booking = new Booking();
            BookingCreateWindow bookingCreateWindow = new BookingCreateWindow(true, booking, clientService, roomService, roomTypeService);
            bool? result = bookingCreateWindow.ShowDialog();

        }

        private void editBookingButton_Click(object sender, RoutedEventArgs e)
        {
            if (bookingDataGrid.SelectedItem != null)
            {
                Booking booking = (Booking)bookingDataGrid.SelectedItem;
                BookingCreateWindow bookingCreateWindow = new BookingCreateWindow(false, booking, clientService, roomService, roomTypeService);
                bool? result = bookingCreateWindow.ShowDialog();

                if (result == true)
                {
                    /*bookingService.Upda(room);
                    refreshRoomList();*/
                }
            }
        }
    }
}
