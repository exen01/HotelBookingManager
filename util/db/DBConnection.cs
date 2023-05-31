using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace HotelBookingManager.util.db
{
    internal class DBConnection
    {
        private DBConnection()
        {
        }

        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public MySqlConnection Connection { get; private set; }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
            {
                _instance = new DBConnection()
                {
                    Server = "127.0.0.1",
                    DatabaseName = "hotel_booking_manager",
                    UserName = "root",
                    Password = "root"
                };
            }

            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(DatabaseName))
                {
                    return false;
                }

                string connectionString = string.Format("server={0};database={1};uid={2};password={3}", Server, DatabaseName, UserName, Password);
                Connection = new MySqlConnection(connectionString);

                try
                {
                    Connection.Open();
                }
                catch (MySqlException e)
                {
                    MessageBox.Show(e.Message + '\n' + "Не удалось подключиться ни к одному из указанных хостов MySQL.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
            }

            return true;
        }

        public void Close()
        {
            Connection.Close();
        }
    }
}
