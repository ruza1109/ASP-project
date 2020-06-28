using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.Project;
using Application.DTO;
using Application.DTO.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Teamwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly CommandExecutor _executor;

        public ProjectsController(CommandExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<ProjectsController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchProjectDTO dto, [FromServices] IGetProjectQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, dto));
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public IActionResult Post([FromBody] ProjectDTO dto, [FromServices] ICreateProjectCommand command)
        {
            _executor.ExecuteCommand(command, dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
