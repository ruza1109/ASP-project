using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;

namespace Implementation.Profiles
{

    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role,RoleDTO>();
            CreateMap<RoleDTO,Role>();
        }
    }
}
