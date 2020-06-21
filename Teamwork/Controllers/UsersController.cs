﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teamwork.App.Extensions;
using Teamwork.App.Validations;
using Teamwork.DTO;
using Teamwork.DTO.Search;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Teamwork.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;

        public UsersController(TeamworkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchUserDTO dto)
        {
            try
            {
                var usersQuery = _context.Users.Include(u => u.Role).AsQueryable();

                if(dto.FullName != null)
                {
                    usersQuery = usersQuery.Where(u => u.FullName.ToLower().Contains(dto.FullName.ToLower()));
                }
                
                if(dto.Username != null)
                {
                    usersQuery = usersQuery.Where(u => u.Username.ToLower().Contains(dto.Username.ToLower()));
                }

                if(dto.Role != null)
                {
                    usersQuery = usersQuery.Where(u => u.Role.Name.ToLower().Contains(dto.Role.Name.ToLower()));
                }

                var users = _mapper.Map<List<UserDTO>>(usersQuery.ToList());

                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _context.Users.Where(u => u.Id == id).Include(u => u.Role).FirstOrDefault();

            if(user == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<UserDTO>(user);

            return Ok(response);
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] UserDTO dto,
            [FromServices] CreateUserValidation validation)
        {
            var data = validation.Validate(dto);

            if (!data.IsValid)
            {
                return data.AsUnprocessableEntity();
            }

            var user = _mapper.Map<User>(dto);

            //  Set Principal Entity to null to prevent creating it
            user.Role = null;
            user.RoleId = dto.Role.Id;

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = _context.Users.Find(id);

                if (user == null)
                {
                    return NotFound();
                }

                user.DeletedAt = DateTime.Now;
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}