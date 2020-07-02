using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.Task;
using Application.DTO;
using Application.DTO.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Teamwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly CommandExecutor _executor;

        public TasksController(CommandExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<TasksController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchTaskDTO dto, [FromServices] IGetTaskQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, dto));
        }

        // GET api/<TasksController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetOneTaskQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<TasksController>
        [HttpPost]
        public IActionResult Post([FromBody] TaskDTO dto, [FromServices] ICreateTaskCommand command)
        {
            _executor.ExecuteCommand(command, dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<TasksController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TaskDTO dto, [FromServices] IUpdateTaskCommand command)
        {
            dto.Id = id;

            _executor.ExecuteCommand(command, dto);

            return NoContent();
        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteTaskCommand command)
        {
            _executor.ExecuteCommand(command,id);

            return NoContent();
        }
    }
}
