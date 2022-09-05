using ApiTasks.Domain;
using System.Collections.Generic;

namespace ApiTasks.Repository
{
    public interface ITaskListRepository
    {
        public List<TaskList> ListAll();

        public List<TaskList> List(int cod_task);

        public int Insert(TaskList taskList);

        public int Update(TaskList taskList);
    }
}
