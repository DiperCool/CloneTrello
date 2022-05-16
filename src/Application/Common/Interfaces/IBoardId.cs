using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IBoardId
    {
        Type Key { get; }
        Task<Guid> GetBoardId(Guid guid);
    }
}