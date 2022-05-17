using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Common.DTOs;
public class MemberDTO: IMapFrom<Member>
{
    public UserDTO User { get; set; }
    public MemberType MemberType { get; set; }
}