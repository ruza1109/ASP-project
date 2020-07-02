using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands;
using Application.DTO.Search;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Teamwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly CommandExecutor _executor;

        public LogsController(CommandExecutor executor)
        {
            _executor = executor;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] SearchLogDTO dto, [FromServices] IGetLogsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, dto));
        }

       
    }
}
