using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.DTOs
{
    public class CardDTO: IMapFrom<Card>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }=String.Empty;
        public int IndexNumber { get; set; }
        public Guid ListCardsId { get; set; }
        public DateTime Created { get; set; } 
    }
}