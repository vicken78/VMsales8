using Microsoft.Data.Sqlite;
using System.Configuration;
using System.Data;
using System.IO;

namespace VMsales8.Logic
{
    public interface IDatabaseProvider
    {
        IDbConnection ObtainConnection();
    }

    public class SQLiteDatabase : IDatabaseProvider
    {
        public bool IsConnected => !string.IsNullOrEmpty(Filepath) && File.Exists(Filepath);
        public string Filepath { get; private set; }

        public SQLiteDatabase(string filepath)
        {
            if (filepath != null)
                Connect(filepath);
        }

        protected virtual void OnConnected() { }

        public virtual bool Connect(string filepath)
        {
            // Get from app settings if not provided directly
            filepath = ConfigurationManager.AppSettings["DatabaseFilePath"] ?? filepath;

            if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath))
            {
                throw new FileNotFoundException($"Database file '{filepath}' not found.");
            }

            if (IsConnected)
                return false;

            Filepath = filepath;
            OnConnected();

            return true;
        }

        public virtual bool Disconnect()
        {
            if (!IsConnected)
                return false;

            Filepath = null;
            return true;
        }

        public IDbConnection ObtainConnection()
        {
            if (!IsConnected)
                throw new FileNotFoundException($"The database file at '{Filepath}' does not exist.");

            var connectionString = $"Data Source=\"{Filepath}\"; Foreign Keys=True;";
            return new SqliteConnection(connectionString);
        }
    }
}
