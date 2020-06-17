using AutoMapper;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teamwork.DTO;

namespace Teamwork.App.Profiles
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
