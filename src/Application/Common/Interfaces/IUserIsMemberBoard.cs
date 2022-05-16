using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IUserIsMemberBoard
    {
        public Guid Id { get; set; }
    }
}