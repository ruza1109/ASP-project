using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CommandHаndler
{
    public interface ICommand<TRequest> : IUseCase
    {
        void Execute(TRequest request);
    }

}
