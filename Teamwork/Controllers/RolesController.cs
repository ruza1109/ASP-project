﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands;
using Application.Commands.Role;
using Application.DTO;
using Application.DTO.Search;
using AutoMapper;
using DataAccess;
using Implementation.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.CommandHаndler;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Teamwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly CommandExecutor _executor;

        public RolesController(CommandExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/roles
        [HttpGet]
        public IActionResult Get([FromQuery] SearchRoleDTO search, [FromServices] IGetRoleQuery query)
        {
            return Ok(_executor.ExecuteQuery(query,search));
        }

        // GET api/roles/1
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetOneRoleQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<RolesController>
        [HttpPost]
        public IActionResult Post([FromBody] RoleDTO dto, [FromServices] ICreateRoleCommand command)
        {
            _executor.ExecuteCommand(command, dto);

            return NoContent();
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RoleDTO dto, [FromServices] IUpdateRoleCommand command)
        {
            dto.Id = id;

            _executor.ExecuteCommand(command, dto);

            return NoContent();

        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRoleCommand command)
        {
            _executor.ExecuteCommand(command, id);

            return NoContent();
        }
    }
}
