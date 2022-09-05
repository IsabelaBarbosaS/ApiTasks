using System;
using ApiTasks.Domain;
using ApiTasks.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tasks.Services.Interfaces;

namespace ApiTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListController : ControllerBase
    {
        private readonly ILogger<TaskListController> _logger;
        private readonly ITaskService _taskService;

        public TaskListController(ILogger<TaskListController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAllData()
        {
            try
            {
                var data = _taskService.ListAll();

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar obter dados");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("get/{cod_task}")]
        public IActionResult GetData(int cod_task)
        {
            try
            {
                var data = _taskService.List(cod_task);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar obter Tarefas");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("insert")]
        public IActionResult SetData(TaskList taskList)
        {
            try
            {
                var result = _taskService.Insert(taskList);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar inserir Tarefa");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateData(TaskList taskList)
        {
            try
            {
                var result = _taskService.Update(taskList);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar inserir Tarefa");
                return StatusCode(500, ex.Message);
            }
        }
    }
}