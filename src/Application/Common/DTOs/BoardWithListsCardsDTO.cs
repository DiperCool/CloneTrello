using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Common.DTOs
{
    public class BoardWithListsCardsDTO: IMapFrom<Board>
    {
        public Guid Id { get; set; }
        public UserDTO Owner { get; set; }
        public string Title { get ;set; }= String.Empty;
        public Visibility Visibility { get; set; }
        public DateTime Created { get; set; }
        public List<ListCardsDTO> ListCards { get; set; } = new List<ListCardsDTO>();
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Board, BoardWithListsCardsDTO>()
                .ForMember(x=>x.ListCards, opt=>opt.MapFrom(x=>x.ListCards.OrderBy(x=>x.IndexNumber)));            
        }
    }
}