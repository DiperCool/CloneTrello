using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Member : IEntity
    {
        public Guid Id { get; set; }
        public Board Board { get; set; }
        public Guid BoardId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public MemberType MemberType { get; set; }
    }
}