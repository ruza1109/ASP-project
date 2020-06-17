using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teamwork.App.Extensions;
using Teamwork.App.Validations;
using Teamwork.DTO;
using Teamwork.DTO.Search;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Teamwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;

        public RolesController(TeamworkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/roles
        [HttpGet]
        public IActionResult Get([FromQuery] SearchRoleDTO dto)
        {
            try
            {
                var rolesQuery = _context.Roles.AsQueryable();

                if(dto.Name != null)
                {
                    rolesQuery = rolesQuery.Where(r => r.Name.ToLower().Contains(dto.Name.ToLower()));
                }

                var roles = _mapper.Map<List<RoleDTO>>(rolesQuery.ToList());

                return Ok(roles);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

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
        public IActionResult Post([FromBody] RoleDTO dto, [FromServices] CreateRoleValidation validation)
        {
            var data = validation.Validate(dto);

            if(!data.IsValid)
            {
                return data.AsUnprocessableEntity();
            }

            var role = _mapper.Map<Role>(dto);

            try
            {
                _context.Add(role);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RoleDTO dto)
        {

            dto.Id = id;
            try
            {
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
