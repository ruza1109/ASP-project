using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(int id)
            :base($"Entity with an id: '{id}' does not exist.")
        {

        }
    }
}
