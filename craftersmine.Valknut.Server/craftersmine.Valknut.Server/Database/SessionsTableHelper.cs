using craftersmine.Valknut.Server.Database.Models;

using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Database
{
    public static class SessionsTableHelper
    {
        public static UserSession GetUserSession(string uuid)
        {
            var escapedUuid = MySqlHelper.EscapeString(uuid);
            var command = DatabaseConnection.Instance.GetSqlConnection().CreateCommand();
            command.CommandText = $"SELECT * FROM sessions WHERE `uuid`='{escapedUuid}'";

            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    UserSession uSess = new UserSession();
                    uSess.Uuid = reader.GetString("uuid");
                    uSess.Username = reader.GetString("username");
                    uSess.SessionId = reader.GetString("sessionId");
                    uSess.ServerId = reader.GetString("serverId");
                    uSess.ClientToken = reader.GetString("clientToken");
                    return uSess;
                }
            }
            return null;
        }

        public static UserSession GetUserSessionByServerId(string serverId)
        {
            var escapedServerId = MySqlHelper.EscapeString(serverId);
            var command = DatabaseConnection.Instance.GetSqlConnection().CreateCommand();
            command.CommandText = $"SELECT * FROM sessions WHERE `serverId`='{escapedServerId}'";

            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    UserSession uSess = new UserSession();
                    uSess.Uuid = reader.GetString("uuid");
                    uSess.Username = reader.GetString("username");
                    uSess.SessionId = reader.GetString("sessionId");
                    uSess.ServerId = reader.GetString("serverId");
                    uSess.ClientToken = reader.GetString("clientToken");
                    return uSess;
                }
            }
            return null;
        }

        public static void UpdateUserSession(string username, string uuid, string sessionId = "", string serverId = "", string clientToken = "")
        {
            var escapedUsername = MySqlHelper.EscapeString(username);
            var escapedUuid = MySqlHelper.EscapeString(uuid);
            var escapedSessionId = MySqlHelper.EscapeString(sessionId);
            var escapedServerId = MySqlHelper.EscapeString(serverId);
            var escapedClientToken = MySqlHelper.EscapeString(clientToken);

            var commandSelect = DatabaseConnection.Instance.GetSqlConnection().CreateCommand();
            commandSelect.CommandText = $"SELECT * FROM sessions WHERE `uuid`='{escapedUuid}'";

            bool isSessionExists = false;
            using (var reader = commandSelect.ExecuteReader())
                isSessionExists = reader.HasRows;

            if (isSessionExists)
            {
                var userSession = GetUserSession(uuid);
                var commandUpdate = DatabaseConnection.Instance.GetSqlConnection().CreateCommand();
                if (string.IsNullOrWhiteSpace(sessionId))
                    escapedSessionId = userSession.SessionId;
                if (string.IsNullOrWhiteSpace(serverId))
                    escapedServerId = userSession.ServerId;
                if (string.IsNullOrWhiteSpace(clientToken))
                    escapedClientToken = userSession.ClientToken;
                string ctor = $"UPDATE sessions SET `sessionId`='{escapedSessionId}', `serverId`='{escapedServerId}', `clientToken`='{escapedClientToken}' WHERE `uuid`='{escapedUuid}'";
                commandUpdate.CommandText = ctor;
                commandUpdate.ExecuteNonQuery();
            }
            else
            {
                var commandInsert = DatabaseConnection.Instance.GetSqlConnection().CreateCommand();
                commandInsert.CommandText = $"INSERT INTO sessions (`uuid`, `username`, `sessionId`, `serverId`, `clientToken`) VALUES ('{escapedUuid}', '{escapedUsername}', '{escapedSessionId}', '{escapedServerId}', '{escapedClientToken}')";
                commandInsert.ExecuteNonQuery();
            }
        }

        public static string GenerateSessionId(string username, string uuid)
        {
            byte[] rndBytes = new byte[64];
            new Random().NextBytes(rndBytes);
            string rndStr = Encoding.Default.GetString(rndBytes);
            string id = username + ":|:" + uuid + ":|:" + rndStr + ":|:" + DateTimeOffset.Now.ToUnixTimeSeconds();
            string sessId = BCrypt.Net.BCrypt.HashPassword(id);
            return sessId;
        }

        public static string GetUserClientToken(string uuid)
        {
            var escapedUuid = MySqlHelper.EscapeString(uuid);

            var command = DatabaseConnection.Instance.GetSqlConnection().CreateCommand();
            command.CommandText = $"SELECT `clientToken` FROM sessions WHERE `uuid`='{escapedUuid}'";

            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    return reader.GetString("clientToken");
                }
                else return null;
            }
        }
    }
}
