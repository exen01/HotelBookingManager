using HotelBookingManager.domain.dto;
using HotelBookingManager.util.db;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HotelBookingManager.dao.client
{
    internal class ClientDaoImpl : IClientDao
    {
        private readonly DBConnection connection;

        public ClientDaoImpl(DBConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Добавляет нового клиента в базу данных.
        /// </summary>
        /// <param name="client">Объект клиента для добавления.</param>
        public void AddClient(Client client)
        {
            if (connection.IsConnect())
            {
                string query = "INSERT INTO client(name, address, email, phone_number) " +
                    "VALUES (@name, @address, @email, @phone_number)";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@name", client.Name);
                command.Parameters.AddWithValue("@address", client.Address);
                command.Parameters.AddWithValue("@email", client.Email);
                command.Parameters.AddWithValue("@phone_number", client.PhoneNumber);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Удаляет клиента из базы данных по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор клиента.</param>
        public void DeleteClientById(int id)
        {
            if (connection.IsConnect())
            {
                string query = "DELETE FROM client WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Получает список всех клиентов из базы данных.
        /// </summary>
        /// <returns>Список всех клиентов.</returns>
        public List<Client> GetAllClients()
        {
            List<Client> clients = new List<Client>();

            if (connection.IsConnect())
            {
                string query = "SELECT id, name, address, email, phone_number FROM client";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Client client = new Client()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Address = reader.GetString("address"),
                            Email = reader.GetString("email"),
                            PhoneNumber = reader.GetString("phone_number")
                        };

                        clients.Add(client);
                    }
                }
            }

            return clients;
        }

        /// <summary>
        /// Возвращает клиента по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор клиента.</param>
        /// <returns>Объект клиента, соответствующий указанному идентификатору, или null, если клиент не найден.</returns>
        public Client? GetClientById(int id)
        {
            Client? client = null;
            if (connection.IsConnect())
            {
                string query = "SELECT id, name, address, email, phone_number FROM client " +
                    "WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Client()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Address = reader.GetString("address"),
                            Email = reader.GetString("email"),
                            PhoneNumber = reader.GetString("phone_number")
                        };
                    }
                }
            }

            return client;
        }

        /// <summary>
        /// Обновляет информацию о клиенте в базе данных.
        /// </summary>
        /// <param name="client">Объект клиента с обновленными данными.</param>
        public void UpdateClient(Client client)
        {
            if (connection.IsConnect())
            {
                string query = "UPDATE client " +
                    "SET name = @name, address = @address, email = @email, phone_number = @phone_number" +
                    "WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                command.Parameters.AddWithValue("@name", client.Name);
                command.Parameters.AddWithValue("@address", client.Address);
                command.Parameters.AddWithValue("@email", client.Email);
                command.Parameters.AddWithValue("@phone_number", client.PhoneNumber);
                command.Parameters.AddWithValue("@id", client.Id);

                command.ExecuteNonQuery();
            }
        }
    }
}
