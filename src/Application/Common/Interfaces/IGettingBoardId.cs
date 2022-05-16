using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IGettingBoardId
    {
        Task<Guid> GetBoardId(Type type, Guid id);
    }
}