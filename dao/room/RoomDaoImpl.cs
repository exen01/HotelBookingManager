﻿using HotelBookingManager.domain.dto;
using HotelBookingManager.util.db;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HotelBookingManager.dao.room
{
    internal class RoomDaoImpl : IRoomDao
    {
        private readonly DBConnection connection;

        public RoomDaoImpl(DBConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Добавляет новый номер гостиницы в базу данных.
        /// </summary>
        /// <param name="room">Объект номера гостиницы для добавления.</param>
        public void AddRoom(Room room)
        {
            if (connection.IsConnect())
            {
                string query = "INSERT INTO room(type, cost, availability, description) " +
                    "VALUES (@type, @cost, @availability, @description)";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@type", room.Type);
                command.Parameters.AddWithValue("@cost", room.Cost);
                command.Parameters.AddWithValue("@availability", room.Availability);
                command.Parameters.AddWithValue("@description", room.Description);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        /// <summary>
        /// Удаляет номер гостиницы из базы данных по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор номера гостиницы.</param>
        public void DeleteRoomById(int id)
        {
            if (connection.IsConnect())
            {
                string query = "DELETE FROM room WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        /// <summary>
        /// Получает список всех гостиничных номеров из базы данных.
        /// </summary>
        /// <returns>Список номеров гостиницы.</returns>
        public List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();

            if (connection.IsConnect())
            {
                string query = "SELECT id, type, cost, availability, description FROM room";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Room room = new Room()
                        {
                            Id = reader.GetInt32("id"),
                            Type = reader.GetString("type"),
                            Cost = reader.GetDecimal("cost"),
                            Availability = reader.GetInt32("availability"),
                            Description = reader.GetString("description")
                        };

                        rooms.Add(room);
                    }
                }
            }

            connection.Close();
            return rooms;
        }

        /// <summary>
        /// Возвращает гостиничный номер по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор номера гостиницы.</param>
        /// <returns>Объект номера гостиницы, соответствующий указанному идентификатору, 
        /// или null, если номер не найден.</returns>
        public Room? GetRoomById(int id)
        {
            Room? room = null;

            if (connection.IsConnect())
            {
                string query = "SELECT id, type, cost, availability, description FROM room " +
                    "WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        room = new Room()
                        {
                            Id = reader.GetInt32("id"),
                            Type = reader.GetString("type"),
                            Cost = reader.GetDecimal("cost"),
                            Availability = reader.GetInt32("availability"),
                            Description = reader.GetString("description")
                        };
                    }
                }
            }

            connection.Close();
            return room;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Room> GetRoomsByType(string type)
        {
            List<Room> rooms = new List<Room>();

            if (connection.IsConnect())
            {
                string query = "SELECT id, type, cost, availability, description FROM room " +
                    "WHERE type LIKE @type";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@type", "%" + type + "%");

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Room room = new Room()
                        {
                            Id = reader.GetInt32("id"),
                            Type = reader.GetString("type"),
                            Cost = reader.GetDecimal("cost"),
                            Availability = reader.GetInt32("availability"),
                            Description = reader.GetString("description")
                        };

                        rooms.Add(room);
                    }
                }
            }

            connection.Close();
            return rooms;
        }

        /// <summary>
        /// Обновляет информацию о гостиничном номере в базу данных.
        /// </summary>
        /// <param name="room">Объект номера гостиницы с обновленными данными.</param>
        public void UpdateRoom(Room room)
        {
            if (connection.IsConnect())
            {
                string query = "UPDATE room " +
                    "SET type = @type, cost = @cost, availability = @availability, description = @description " +
                    "WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@type", room.Type);
                command.Parameters.AddWithValue("@cost", room.Cost);
                command.Parameters.AddWithValue("@availability", room.Availability);
                command.Parameters.AddWithValue("@description", room.Description);
                command.Parameters.AddWithValue("@id", room.Id);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}
