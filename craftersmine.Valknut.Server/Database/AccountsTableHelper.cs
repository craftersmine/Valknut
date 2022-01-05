using craftersmine.Valknut.Server.Database.Models;

using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Database
{
    public static class AccountsTableHelper
    {
        public static UserAccount GetUserAccount(string username)
        {
            var command = DatabaseConnection.Instance.GetSqlConnection().CreateCommand();
            string escapedUsername = MySqlHelper.EscapeString(username);
            command.CommandText = $"SELECT * FROM accounts WHERE `username`='{escapedUsername}'";
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    UserAccount userAccount = new UserAccount();
                    userAccount.Id = reader.GetInt32("id");
                    userAccount.Username = reader.GetString("username");
                    userAccount.Password = reader.GetString("password");
                    userAccount.Uuid = reader.GetString("uuid");
                    userAccount.Email = reader.GetString("email");
                    return userAccount;
                }
                else return null;
            }
        }

        public static UserAccount GetUserAccountByEmail(string email)
        {
            var command = DatabaseConnection.Instance.GetSqlConnection().CreateCommand();
            string escapedEmail = MySqlHelper.EscapeString(email);
            command.CommandText = $"SELECT * FROM accounts WHERE `email`='{escapedEmail}'";
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    UserAccount userAccount = new UserAccount();
                    userAccount.Id = reader.GetInt32("id");
                    userAccount.Username = reader.GetString("username");
                    userAccount.Password = reader.GetString("password");
                    userAccount.Uuid = reader.GetString("uuid");
                    userAccount.Email = reader.GetString("email");
                    return userAccount;
                }
                else return null;
            }
        }

        public static UserAccount GetUserAccountByUuid(string uuid)
        {
            var command = DatabaseConnection.Instance.GetSqlConnection().CreateCommand();
            string escapedUuid = MySqlHelper.EscapeString(uuid);
            command.CommandText = $"SELECT * FROM accounts WHERE `uuid`='{escapedUuid}'";
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    UserAccount userAccount = new UserAccount();
                    userAccount.Id = reader.GetInt32("id");
                    userAccount.Username = reader.GetString("username");
                    userAccount.Password = reader.GetString("password");
                    userAccount.Uuid = reader.GetString("uuid");
                    userAccount.Email = reader.GetString("email");
                    return userAccount;
                }
                else return null;
            }
        }

        public static UserAccount UpdateUserUuidByEmail(string email, string uuid)
        {
            var command = DatabaseConnection.Instance.GetSqlConnection().CreateCommand();
            string escapedUuid = MySqlHelper.EscapeString(uuid);
            string escapedEmail = MySqlHelper.EscapeString(email);
            command.CommandText = $"UPDATE accounts SET `uuid`='{escapedUuid}' WHERE `email`='{escapedEmail}'";
            command.ExecuteNonQuery();
            return GetUserAccountByEmail(escapedEmail);
        }

        public static bool CreateUserAccount(string username, string email, string encryptedPassword, string uuid)
        {
            var command = DatabaseConnection.Instance.GetSqlConnection().CreateCommand();
            string escapedUsername = MySqlHelper.EscapeString(username);
            string escapedEmail = MySqlHelper.EscapeString(email);
            string escapedEncryptedPassword = MySqlHelper.EscapeString(encryptedPassword);
            string escapedUuid = MySqlHelper.EscapeString(uuid);

            command.CommandText = $"INSERT INTO accounts (`username`, `password`, `uuid`, `email`) VALUES ('{escapedUsername}', '{escapedEncryptedPassword}', '{escapedUuid}', '{escapedEmail}')";
            var rowsAdded = command.ExecuteNonQuery();

            if (rowsAdded > 0)
                return true;

            return false;
        }
    }
}
