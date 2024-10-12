using Microsoft.Data.Sqlite;
using System;
using System.Data;

namespace VMSales8.Logic
{

    public interface IDatabaseProvider
    {
        IDbConnection ObtainConnection();
    }

    public class SQLiteDatabase : IDatabaseProvider
    {
        public bool IsConnected => Filepath != null;
        public string Filepath { get; private set; }
        public SQLiteDatabase(string filepath)
        {
            if (filepath != null)
                Connect(filepath);
        }
        protected virtual void OnConnected() { }
        public virtual bool Connect(string filepath)
        {
            if (IsConnected)
                return false;

            Filepath = filepath ?? throw new ArgumentNullException(nameof(filepath));

            OnConnected();

            return true;
        }
        public virtual bool Disconnect()
        {
            if (IsConnected)
                return false;
            return true;
        }
        public IDbConnection ObtainConnection()
        {
            if (!IsConnected)
                throw new InvalidOperationException("Cant obtain a connection when disconnected!");
            return new SqliteConnection($"Data Source=\"{Filepath}\";FailIfMissing = True; Foreign Keys = True; Version=3;");
        }
    }
}