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
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ClientesDb _clienteDb;

        public UserController(ClientesDb clientesDb)
        {
            this._clienteDb = clientesDb;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clienteDb.GetAllUser());
        }

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public IActionResult GetById(string id)
        {
            var user = _clienteDb.GetByIdUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPost]
        public IActionResult Create(User user)
        {
            _clienteDb.CreateUser(user);

            return Ok(user);
            /*
            return CreatedAtRoute("GetUser", new
            {
                id = user.Id.ToString()
            }, user);*/
        }
    }
}