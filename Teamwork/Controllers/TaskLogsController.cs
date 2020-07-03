using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.TaskLog;
using Application.DTO;
using Application.DTO.Search;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Teamwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskLogsController : ControllerBase
    {
        private readonly CommandExecutor _executor;

        public TaskLogsController(CommandExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<TaskLogsController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchTaskLogDTO dto, [FromServices] IGetTaskLogQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, dto));
        }

        // GET api/<TaskLogsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetOneTaskLogQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<TaskLogsController>
        [HttpPost]
        public IActionResult Post([FromBody] TaskLogDTO dto, [FromServices] ICreateTaskLogCommand command)
        {
            _executor.ExecuteCommand(command, dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<TaskLogsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TaskLogDTO dto, [FromServices] IUpdateTaskLogCommand command)
        {
            dto.Id = id;

            _executor.ExecuteCommand(command, dto);

            return NoContent();
        }

        // DELETE api/<TaskLogsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteTaskLogCommand command)
        {
            _executor.ExecuteCommand(command, id);

            return NoContent();
        }
    }
}
