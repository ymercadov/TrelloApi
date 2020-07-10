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
    [Route("api/statu")]
    [ApiController]
    public class StatuController : ControllerBase
    {
        private readonly ClientesDb _clienteDb;

        public StatuController(ClientesDb clientesDb)
        {
            this._clienteDb = clientesDb;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var statu = _clienteDb.GetAllStatu();

            if (statu.Count == 0)
            {
                string[] status = new string[] { "Open", "In-Progress", "Completed", "Archived" };

                foreach (string item in status)
                {
                    Create(item);
                }

                return Ok(_clienteDb.GetAllStatu());
            }
            else
            {
                return Ok(statu);
            }
        }

        private void Create(string statu)
        {
            Statu _statu = new Statu(statu);
            _clienteDb.CreateStatu(_statu);

        }

        [HttpPost]
        public IActionResult Create(Statu statu)
        {
            _clienteDb.CreateStatu(statu);

            return CreatedAtRoute("GetTask", new
            {
                id = statu.Id.ToString()
            }, statu);
        }


    }
}