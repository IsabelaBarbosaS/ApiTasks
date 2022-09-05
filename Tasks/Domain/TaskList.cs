using System;

namespace ApiTasks.Domain
{
    public sealed class TaskList
    {
        public long Cod_task { get; set; }
        public string Description_task { get; set; }
        public string Status_task { get; set; }
    }
}
