using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrelloApi.Data;
using TrelloApi.Entities;

namespace TrelloApi.Controllers
{
    [Route("api/tasktokuser")]
    [ApiController]
    public class TaskUserController : ControllerBase
    {
        private readonly ClientesDb _clienteDb;

        public TaskUserController(ClientesDb clientesDb)
        {
            this._clienteDb = clientesDb;
        }

        [HttpGet("{id:length(24)}", Name = "GetTaskToUser")]
        public IActionResult GetById(string id)
        {
            var taskToUser = _clienteDb.GetByIdTaskToUser(id);

            if (taskToUser == null)
            {
                return NotFound();
            }

            return Ok(taskToUser);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clienteDb.ConsultDetail());
        }
               
        [HttpPost]
        public IActionResult Create(TaskToUser taskToUser)
        {
            _clienteDb.CreateTasktoUser(taskToUser);

            return CreatedAtRoute("GetTaskToUser", new
            {
                id = taskToUser.Id.ToString()
            }, taskToUser);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteById(string id)
        {
            var taskToUser = _clienteDb.GetByIdTaskToUser(id);

            if (taskToUser == null)
            {
                return NotFound();
            }

            _clienteDb.DeleteById(taskToUser.Id);

            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, TaskToUser taskToUser)
        {
            var _taskToUser = _clienteDb.GetByIdTaskToUser(id);

            if (_taskToUser == null)
            {
                return NotFound();
            }

            _clienteDb.Update(id, taskToUser);

            return NoContent();
        }
    }
}