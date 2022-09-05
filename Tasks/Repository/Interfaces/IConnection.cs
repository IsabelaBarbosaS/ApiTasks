using Npgsql;
using System.Collections.Generic;

namespace Tasks.Repository.Interfaces
{
    public interface IConnection
    {

        void Open();

        void Close();

        void OpenTransaction();

        void CommitTransaction();

        void RollBackTransaction();

        bool IsOpen();
        bool TransactionIsOpen();

        NpgsqlDataReader ExecuteReader(string query, List<NpgsqlParameter> parameters);

        NpgsqlDataReader ExecuteReader(string query);

        int ExecuteNonQuery(string query, List<NpgsqlParameter> parameters);

        int ExecuteNonQuery(string query);

    }
}
