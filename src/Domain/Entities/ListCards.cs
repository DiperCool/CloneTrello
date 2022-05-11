using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class ListCards
    {
        public Guid Id { get ;set; }
        public string Title { get; set; } = String.Empty;
        public List<Card> Cards { get; set; } = new List<Card>();
        public Board? Board { get; set; }
        public Guid BoardId { get; set; }

    }
}