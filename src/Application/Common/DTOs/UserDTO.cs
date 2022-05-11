using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.DTOs
{
    public class UserDTO: IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string Login { get; set; }= string.Empty;
    }
}