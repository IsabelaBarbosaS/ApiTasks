using ApiTasks.Domain;
using ApiTasks.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Tasks.Repository.Connection;
using Tasks.Services.Interfaces;

namespace Tasks.Services
{
    public class TaskService : ITaskService
    {

        ITaskListRepository repository;

        public TaskService(ITaskListRepository taskListRepository)
        {
            this.repository = taskListRepository;
        }

        public IEnumerable<TaskList> ListAll()
        {
            try
            {
                return repository.ListAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TaskList> List(int cod_task)
        {

            try
            {
                return repository.List(cod_task);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(TaskList taskList)
        {
            try
            {
                var cod = List((int)taskList.Cod_task);
                if (cod != null)
                {
                    if (cod.Any(x => x.Cod_task == taskList.Cod_task))
                        throw new Exception("Codigo de tarefa já existe, digite outro!");
                }

                if (taskList.Description_task == "")
                    throw new Exception($"A descrição deve estar preenchida");

                taskList.Status_task = String.IsNullOrEmpty(taskList.Status_task) ? "P" : taskList.Status_task;

                return repository.Insert(taskList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Update(TaskList taskList)
        {

            try
            {
                 if (taskList.Description_task == "")
                    throw new Exception($"A descrição deve estar preenchida");

                 if(taskList.Status_task.ToUpper() == "P")
                    throw new Exception($"O Status só pode ser passado para Concluido");


                return repository.Update(taskList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message) ;
            }

        }




    }
}
