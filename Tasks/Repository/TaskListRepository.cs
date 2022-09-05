using System;
using System.Collections.Generic;
using ApiTasks.Domain;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Tasks.Repository.Connection;
using Tasks.Repository.Interfaces;

namespace ApiTasks.Repository
{
    public sealed class TaskListRepository : ITaskListRepository
    {
        private IConnection connection;

        public TaskListRepository(IConnection connection)
        {
            this.connection = connection;
        }

        public List<TaskList> ListAll()
        {
            try
            {
                connection.Open();

                List<TaskList> tasks = new List<TaskList>();

                string query = "select Cod_task,Description_task, Status_task from list";

                using (NpgsqlDataReader reader = connection.ExecuteReader(query))
                {

                    while (reader.Read())
                    {
                        tasks.Add(new TaskList
                        {
                            Cod_task = Convert.ToInt64(reader["Cod_task"]),
                            Description_task = reader["Description_task"].ToString(),
                            Status_task = reader["Status_task"].ToString(),
                        });
                    }

                }

                return tasks.Count > 0 ? tasks : null;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<TaskList> List(int cod_task)
        {
            try
            {
                connection.Open();

                List<TaskList> tasks = new List<TaskList>();

                List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();

                parameters.Add(new NpgsqlParameter("@codtask", cod_task));

                string query = "select Cod_task,Description_task, Status_task from list where cod_task = @codtask";

                using (NpgsqlDataReader reader = connection.ExecuteReader(query, parameters))
                {

                    while (reader.Read())
                    {
                        tasks.Add(new TaskList
                        {
                            Cod_task = Convert.ToInt64(reader["Cod_task"]),
                            Description_task = reader["Description_task"].ToString(),
                            Status_task = reader["Status_task"].ToString(),
                        });
                    }

                }

                return tasks.Count > 0 ? tasks : null;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public int Insert(TaskList taskList)
        {
            try
            {
                connection.Open();

                connection.OpenTransaction();

                List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();

                parameters.Add(new NpgsqlParameter("@cod_task", taskList.Cod_task));
                parameters.Add(new NpgsqlParameter("@status", taskList.Status_task));
                parameters.Add(new NpgsqlParameter("@description", taskList.Description_task));

                var query = "insert into list (cod_task, status_task,description_task)values (@cod_task, @status , @description)";

                
               var retorno = connection.ExecuteNonQuery(query, parameters);

                connection.CommitTransaction();

                return retorno;
            }
            catch (Exception ex)
            {
                if (connection.TransactionIsOpen())
                {
                    connection.RollBackTransaction();
                }

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public int Update(TaskList taskList)
        {
            try
            {

                connection.Open();

                connection.OpenTransaction();

                List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();

                parameters.Add(new NpgsqlParameter("@status", taskList.Status_task));
                parameters.Add(new NpgsqlParameter("@description", taskList.Description_task));
                parameters.Add(new NpgsqlParameter("@cod_task", taskList.Cod_task));


                var query = "update public.list set  status_task = @status , description_task = @description where cod_task = @cod_task";

                var retorno = connection.ExecuteNonQuery(query, parameters);

                connection.CommitTransaction();

                return retorno;
            }
            catch (Exception ex)
            {
                if (connection.TransactionIsOpen())
                {
                    connection.RollBackTransaction();
                }

                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

