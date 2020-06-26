using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CommandHаndler
{
    public interface IUseCase
    {
        public int Id { get; }
        public string Name { get; }
    }

}
