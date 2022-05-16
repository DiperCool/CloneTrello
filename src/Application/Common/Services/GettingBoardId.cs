using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Services
{
    public class GettingBoardId : IGettingBoardId
    {
        
        IEnumerable<IBoardId> _boardsId;

        public GettingBoardId(IEnumerable<IBoardId> boardsId)
        {
            _boardsId = boardsId;
        }

        public async Task<Guid> GetBoardId(Type type,Guid id)
        {
            foreach(IBoardId boardId in _boardsId)
            {
                if(boardId.Key==type)
                {
                    return await boardId.GetBoardId(id);
                }
            }
            return Guid.Empty;
        }
    }
}