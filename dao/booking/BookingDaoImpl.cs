using HotelBookingManager.domain.dto;
using HotelBookingManager.util.db;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HotelBookingManager.dao.booking
{
    internal class BookingDaoImpl : IBookingDao
    {
        private readonly DBConnection connection;

        public BookingDaoImpl(DBConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Добавляет новую запись о бронировании в базу данных.
        /// </summary>
        /// <param name="booking">Объект бронирования для добавления.</param>
        public void AddBooking(Booking booking)
        {
            if (connection.IsConnect())
            {
                string query = "INSERT INTO booking(client_id, room_id, arrival_date, departure_date, " +
                    "duration_of_stay, additional_information, status, total_cost, payment_status, created_at) " +
                    "VALUES (@client_id, @room_id, @arrival_date, @departure_date, @duration_of_stay, " +
                    "@additional_information, @status, @total_cost, @payment_status, @created_at)";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@client_id", booking.ClientId);
                command.Parameters.AddWithValue("@room_id", booking.RoomId);
                command.Parameters.AddWithValue("@arrival_date", booking.ArrivalDate);
                command.Parameters.AddWithValue("@departure_date", booking.DepartureDate);
                command.Parameters.AddWithValue("@duration_of_stay", booking.DurationOfStay);
                command.Parameters.AddWithValue("@additional_information", booking.AdditionalInformation);
                command.Parameters.AddWithValue("@status", booking.Status);
                command.Parameters.AddWithValue("@total_cost", booking.TotalCost);
                command.Parameters.AddWithValue("@payment_status", booking.PaymentStatus);
                command.Parameters.AddWithValue("@created_at", booking.CreatedAt);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        /// <summary>
        /// Удаляет запись о бронировании из базы данных по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор бронирования.</param>
        public void DeleteBookingById(int id)
        {
            if (connection.IsConnect())
            {
                string query = "DELETE FROM booking WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        /// <summary>
        /// Получает список всех записей о бронировании из базы данных.
        /// </summary>
        /// <returns>Список бронирований.</returns>
        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();

            if (connection.IsConnect())
            {
                string query = "SELECT id, client_id, room_id, arrival_date, departure_date, " +
                    "duration_of_stay, additional_information, status, total_cost, payment_status, created_at " +
                    "FROM booking";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking()
                        {
                            Id = reader.GetInt32("id"),
                            ClientId = reader.GetInt32("client_id"),
                            RoomId = reader.GetInt32("room_id"),
                            ArrivalDate = reader.GetDateTime("arrival_date"),
                            DepartureDate = reader.GetDateTime("departure_date"),
                            DurationOfStay = reader.GetInt32("duration_of_stay"),
                            AdditionalInformation = reader.GetString("additional_information"),
                            Status = reader.GetString("status"),
                            TotalCost = reader.GetDecimal("total_cost"),
                            PaymentStatus = reader.GetString("payment_status"),
                            CreatedAt = reader.GetDateTime("created_at")
                        };

                        bookings.Add(booking);
                    }
                }
            }

            connection.Close();
            return bookings;
        }

        /// <summary>
        /// Возвращает запись о бронировании по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор бронирования.</param>
        /// <returns>Объект бронирования, соответствующий указанному идентификатору, или null,
        /// если бронирование не найдено.</returns>
        public Booking? GetBookingById(int id)
        {
            Booking? booking = null;

            if (connection.IsConnect())
            {
                string query = "SELECT id, client_id, room_id, arrival_date, departure_date, " +
                    "duration_of_stay, additional_information, status, total_cost, payment_status, created_at " +
                    "FROM booking " +
                    "WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        booking = new Booking()
                        {
                            Id = reader.GetInt32("id"),
                            ClientId = reader.GetInt32("client_id"),
                            RoomId = reader.GetInt32("room_id"),
                            ArrivalDate = reader.GetDateTime("arrival_date"),
                            DepartureDate = reader.GetDateTime("departure_date"),
                            DurationOfStay = reader.GetInt32("duration_of_stay"),
                            AdditionalInformation = reader.GetString("additional_information"),
                            Status = reader.GetString("status"),
                            TotalCost = reader.GetDecimal("total_cost"),
                            PaymentStatus = reader.GetString("payment_status"),
                            CreatedAt = reader.GetDateTime("created_at")
                        };
                    }
                }
            }

            connection.Close();
            return booking;
        }

        /// <summary>
        /// Возвращает записи о бронировании по указанному идентификатору клиента.
        /// </summary>
        /// <param name="clientId">Идентификатор клиента.</param>
        /// <returns>Список бронирований клиента.</returns>
        public List<Booking> GetBookingsByClientId(int clientId)
        {
            List<Booking> bookings = new List<Booking>();

            if (connection.IsConnect())
            {
                string query = "SELECT id, client_id, room_id, arrival_date, departure_date, " +
                    "duration_of_stay, additional_information, status, total_cost, payment_status, created_at " +
                    "FROM booking " +
                    "WHERE client_id = @client_id";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@client_id", clientId);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking()
                        {
                            Id = reader.GetInt32("id"),
                            ClientId = reader.GetInt32("client_id"),
                            RoomId = reader.GetInt32("room_id"),
                            ArrivalDate = reader.GetDateTime("arrival_date"),
                            DepartureDate = reader.GetDateTime("departure_date"),
                            DurationOfStay = reader.GetInt32("duration_of_stay"),
                            AdditionalInformation = reader.GetString("additional_information"),
                            Status = reader.GetString("status"),
                            TotalCost = reader.GetDecimal("total_cost"),
                            PaymentStatus = reader.GetString("payment_status"),
                            CreatedAt = reader.GetDateTime("created_at")
                        };

                        bookings.Add(booking);
                    }
                }
            }

            return bookings;
        }

        /// <summary>
        /// Возвращает записи о бронировании по указанному идентификатору номера гостиницы.
        /// </summary>
        /// <param name="clientId">Идентификатор номера гостиницы.</param>
        /// <returns>Список бронирований номера.</returns>
        public List<Booking> GetBookingsByRoomId(int roomId)
        {
            List<Booking> bookings = new List<Booking>();

            if (connection.IsConnect())
            {
                string query = "SELECT id, client_id, room_id, arrival_date, departure_date, " +
                    "duration_of_stay, additional_information, status, total_cost, payment_status, created_at " +
                    "FROM booking " +
                    "WHERE room_id = @room_id";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@room_id", roomId);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking()
                        {
                            Id = reader.GetInt32("id"),
                            ClientId = reader.GetInt32("client_id"),
                            RoomId = reader.GetInt32("room_id"),
                            ArrivalDate = reader.GetDateTime("arrival_date"),
                            DepartureDate = reader.GetDateTime("departure_date"),
                            DurationOfStay = reader.GetInt32("duration_of_stay"),
                            AdditionalInformation = reader.GetString("additional_information"),
                            Status = reader.GetString("status"),
                            TotalCost = reader.GetDecimal("total_cost"),
                            PaymentStatus = reader.GetString("payment_status"),
                            CreatedAt = reader.GetDateTime("created_at")
                        };

                        bookings.Add(booking);
                    }
                }
            }

            return bookings;
        }

        /// <summary>
        /// Возвращает записи о бронировании по указанному статусу.
        /// </summary>
        /// <param name="status">Статус бронирования.</param>
        /// <returns>Список бронирований.</returns>
        public List<Booking> GetBookingsByStatus(string status)
        {
            List<Booking> bookings = new List<Booking>();

            if (connection.IsConnect())
            {
                string query = "SELECT id, client_id, room_id, arrival_date, departure_date, " +
                    "duration_of_stay, additional_information, status, total_cost, payment_status, created_at " +
                    "FROM booking " +
                    "WHERE status LIKE @status";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@status", "%" + status + "%");

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking()
                        {
                            Id = reader.GetInt32("id"),
                            ClientId = reader.GetInt32("client_id"),
                            RoomId = reader.GetInt32("room_id"),
                            ArrivalDate = reader.GetDateTime("arrival_date"),
                            DepartureDate = reader.GetDateTime("departure_date"),
                            DurationOfStay = reader.GetInt32("duration_of_stay"),
                            AdditionalInformation = reader.GetString("additional_information"),
                            Status = reader.GetString("status"),
                            TotalCost = reader.GetDecimal("total_cost"),
                            PaymentStatus = reader.GetString("payment_status"),
                            CreatedAt = reader.GetDateTime("created_at")
                        };

                        bookings.Add(booking);
                    }
                }
            }

            return bookings;
        }

        /// <summary>
        /// Обновляет информацию о бронировании в базе данных.
        /// </summary>
        /// <param name="booking">Объект бронирования с обновленными данными.</param>
        public void UpdateBooking(Booking booking)
        {
            if (connection.IsConnect())
            {
                string query = "UPDATE booking " +
                    "SET client_id = @client_id, room_id = @room_id, arrival_date = @arrival_date " +
                    "departure_date = @departure_date, duration_of_stay = @duration_of_stay, additional_information = @additional_information " +
                    "status = @status, total_cost = @total_cost, payment_status = @payment_status, created_at = @created_at " +
                    "WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@client_id", booking.ClientId);
                command.Parameters.AddWithValue("@room_id", booking.RoomId);
                command.Parameters.AddWithValue("@arrival_date", booking.ArrivalDate);
                command.Parameters.AddWithValue("@departure_date", booking.DepartureDate);
                command.Parameters.AddWithValue("@duration_of_stay", booking.DurationOfStay);
                command.Parameters.AddWithValue("@additional_information", booking.AdditionalInformation);
                command.Parameters.AddWithValue("@status", booking.Status);
                command.Parameters.AddWithValue("@total_cost", booking.TotalCost);
                command.Parameters.AddWithValue("@payment_status", booking.PaymentStatus);
                command.Parameters.AddWithValue("@created_at", booking.CreatedAt);

                command.ExecuteNonQuery();
            }
        }
    }
}
