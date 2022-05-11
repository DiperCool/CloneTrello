using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set;}
        public string Login { get; set; }= string.Empty;
        public string Password { get; set; }= string.Empty;
        public List<Board> Boards { get; set; }= new List<Board>();
    }
}