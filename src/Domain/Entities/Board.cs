using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Board
    {
        public Guid Id { get; set; }
        public User? Owner { get; set; }
        public Guid OwnerId { get; set; }
        public string Title { get ;set; }= String.Empty;
        public Visibility Visibility { get; set; }
        public List<ListCards> ListCards { get; set; } = new List<ListCards>();
    }
}