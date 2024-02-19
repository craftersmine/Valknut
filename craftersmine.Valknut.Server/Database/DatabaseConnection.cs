using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Database
{
    public sealed class DatabaseConnection
    {
        private MySqlConnection connection;

        public static DatabaseConnection Instance { get; private set; }

        public DatabaseConnection()
        {
            MySqlConnectionStringBuilder connectionStringBuilder = new MySqlConnectionStringBuilder();
            connectionStringBuilder.Server = Program.Config.DbConnectionConfig.Host;
            connectionStringBuilder.Port = (uint)Program.Config.DbConnectionConfig.Port;
            connectionStringBuilder.UserID = Program.Config.DbConnectionConfig.Username;
            connectionStringBuilder.Password = Program.Config.DbConnectionConfig.Password;
            connectionStringBuilder.Database = Program.Config.DbConnectionConfig.Database;

            connection = new MySqlConnection(connectionStringBuilder.GetConnectionString(true));

            Instance = this;
        }

        public void OpenConnection()
        {
            connection.Open();
        }

        public void EnsureConnectionOpen()
        {
            if (connection.State is ConnectionState.Closed or ConnectionState.Broken)
                OpenConnection();
        }

        public MySqlConnection GetSqlConnection()
        {
            EnsureConnectionOpen();
            return connection;
        }
    }
}
