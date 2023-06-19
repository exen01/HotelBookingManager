using HotelBookingManager.domain.dto;
using HotelBookingManager.util.db;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HotelBookingManager.dao.roomType
{
    class RoomTypeDaoImpl : IRoomTypeDao
    {
        private readonly DBConnection connection;

        public RoomTypeDaoImpl(DBConnection connection)
        {
            this.connection = connection;
        }

        public List<RoomType> GetAllRoomTypes()
        {
            List<RoomType> roomTypes = new List<RoomType>();

            if (connection.IsConnect())
            {
                string query = "SELECT id, name, cost FROM room_type";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RoomType roomType = new RoomType()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Cost = reader.GetDecimal("cost")
                        };

                        roomTypes.Add(roomType);
                    }
                }
            }

            return roomTypes;
        }

        public RoomType? GetRoomTypeById(int id)
        {
            RoomType? roomType = null;

            if (connection.IsConnect())
            {
                string query = "SELECT id, name, cost FROM room_type WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roomType = new RoomType()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Cost = reader.GetDecimal("cost")
                        };
                    }
                }
            }

            return roomType;
        }
    }
}
