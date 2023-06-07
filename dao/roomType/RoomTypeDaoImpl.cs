using HotelBookingManager.domain.dto;
using HotelBookingManager.util.db;
using MySql.Data.MySqlClient;

namespace HotelBookingManager.dao.roomType
{
    class RoomTypeDaoImpl : IRoomTypeDao
    {
        private readonly DBConnection connection;

        public RoomTypeDaoImpl(DBConnection connection)
        {
            this.connection = connection;
        }

        public RoomType? GetRoomTypeById(int id)
        {
            RoomType? roomType = null;

            if (connection.IsConnect())
            {
                string query = "SELECT id, name FROM room_type WHERE id = @id";
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
                        };
                    }
                }
            }

            return roomType;
        }
    }
}
