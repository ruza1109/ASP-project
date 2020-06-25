using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CommandHendler
{
    public interface IUseCase
    {
        public int Id { get; }
        public string Name { get; }

    }
}
