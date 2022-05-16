using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Card:AuditableEntity,IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }=String.Empty;
        public int IndexNumber { get; set; }
        public ListCards ListCards { get; set; }
        public Guid ListCardsId { get; set; }
        
    }
}