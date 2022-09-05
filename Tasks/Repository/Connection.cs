using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Repository.Interfaces;

namespace Tasks.Repository.Connection
{
    public class Connection:IConnection
    {
        private NpgsqlConnection connection;
        private NpgsqlTransaction transaction;


        public Connection(IConfiguration configuration)
        {
            connection = new NpgsqlConnection(configuration.GetConnectionString("TaskListServer"));
        }


        public void Open()
        {
            if (connection != null && connection.State == ConnectionState.Closed)
            {
                this.connection.Open();
            }
        }

        public void Close()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                this.connection.Close();
            }
        }

        public void OpenTransaction()
        {
            try
            {
                if (this.connection.State == ConnectionState.Open && transaction == null)
                {
                    this.transaction = this.connection.BeginTransaction();
                }

            }
            catch (Exception)
            {
                throw;
            }

        }


        public void CommitTransaction()
        {
            try
            {
                if (this.connection.State == ConnectionState.Open && transaction != null)
                {
                    this.transaction.Commit();
                    this.transaction = null;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void RollBackTransaction()
        {
            try
            {
                if (this.connection.State == ConnectionState.Open && transaction != null)
                {
                    this.transaction.Rollback();
                    this.transaction = null;
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool IsOpen()
        {
            return this.connection.State == ConnectionState.Open;
        }

        public bool TransactionIsOpen()
        {
            try
            {
                return transaction != null;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public NpgsqlDataReader ExecuteReader(string query, List<NpgsqlParameter> parameters)
        {
            try
            {
                using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection))
                {

                    npgsqlCommand.Parameters.AddRange(parameters.ToArray());

                    return npgsqlCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public NpgsqlDataReader ExecuteReader(string query)
        {
            try
            {
                using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection))
                {
                    return npgsqlCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int ExecuteNonQuery(string query, List<NpgsqlParameter> parameters)
        {
            try
            {
                using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection))
                {
                    npgsqlCommand.Parameters.AddRange(parameters.ToArray());

                    return npgsqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

     
        public int ExecuteNonQuery(string query)
        {
            try
            {
                using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection))
                {
                    return npgsqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
