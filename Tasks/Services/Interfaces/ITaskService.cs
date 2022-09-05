using ApiTasks.Domain;
using System.Collections.Generic;

namespace Tasks.Services.Interfaces
{
    public interface ITaskService
    {
        IEnumerable<TaskList> ListAll();
        IEnumerable<TaskList> List(int cod_task);
        int Insert(TaskList taskList);
        int Update(TaskList taskList);
    }
}
