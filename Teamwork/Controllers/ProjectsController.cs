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

        [HttpGet]
        public IActionResult Get([FromQuery] SearchProjectDTO dto, [FromServices] IGetProjectQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, dto));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetOneProjectQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProjectDTO dto, [FromServices] ICreateProjectCommand command)
        {
            _executor.ExecuteCommand(command, dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProjectDTO dto, [FromServices] IUpdateProjectCommand command)
        {
            dto.Id = id;

            _executor.ExecuteCommand(command, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteProjectCommand command)
        {
            _executor.ExecuteCommand(command, id);

            return NoContent();
        }
    }
}
