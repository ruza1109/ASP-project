using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public abstract class BaseCommand
    {
        private readonly TeamworkContext _context;
        private readonly IMapper _mapper;

        protected BaseCommand(TeamworkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public TeamworkContext Context => _context;

        public IMapper Mapper => _mapper;
    }
}
