    using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrelloApi.Data;
using TrelloApi.Entities;
namespace TrelloApi.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ClientesDb _clienteDb;

        public TaskController(ClientesDb clientesDb)
        {
            this._clienteDb = clientesDb;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clienteDb.Get());
        }

        [HttpGet("{id:length(24)}", Name = "GetTask")]
        public IActionResult GetById(string id)
        {
            var task = _clienteDb.GetById(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }


        [HttpPost]
        public IActionResult Create(Task task)
        {
            _clienteDb.CreateTask(task);

            return Ok(task);

            //return CreatedAtRoute("GetTask", new
            //{
            //    id = task.Id.ToString()
            //},  task);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Task task)
        {
            var _task = _clienteDb.GetById(id);

            if (_task == null)
            {
                return NotFound();
            }

            _clienteDb.Update(id, task);

            return NoContent();
        }
    }
}