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
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;

        private readonly IApplicationActor _actor;
        private readonly CommandExecutor _executor;

        public RolesController(TeamworkContext context, IMapper mapper, IApplicationActor actor, CommandExecutor executor)
        {
            _context = context;
            _mapper = mapper;
            _actor = actor;
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
        public IActionResult Get(int id)
        {
            try
            {
                var role = _context.Roles.Find(id);

                if(role != null)
                {
                    return Ok(_mapper.Map<RoleDTO>(role));
                } 
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

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
        public IActionResult Put(int id, [FromBody] RoleDTO dto, [FromServices] UpdateRoleValidation validation)
        {

            dto.Id = id;

            try
            {
                var data = validation.Validate(dto);

                var role = _context.Roles.Find(id);

                if(role != null)
                {
                    _mapper.Map(dto, role);
                    _context.SaveChanges();

                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var role = _context.Roles.Find(id);

                if(role != null)
                {
                    role.DeletedAt = DateTime.Now;
                    _context.SaveChanges();

                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
