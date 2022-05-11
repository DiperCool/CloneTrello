using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Common.DTOs
{
    public class BoardDTO: IMapFrom<Board>
    {
        public Guid Id { get; set; }
        public UserDTO? Owner { get; set; }
        public string Title { get ;set; }= String.Empty;
        public Visibility Visibility { get; set; }
    }
}