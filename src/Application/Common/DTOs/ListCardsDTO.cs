using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.DTOs
{
    public class ListCardsDTO: IMapFrom<ListCards>
    {
        public Guid Id { get ;set; }
        public string Title { get; set; } = String.Empty;
        public int IndexNumber { get; set; }
        public Guid BoardId { get; set; }
        public List<CardDTO> Cards { get; set; } = new List<CardDTO>();
        public DateTime Created { get; set; } 
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ListCards, ListCardsDTO>()
                .ForMember(x=>x.Cards, opt=>opt.MapFrom(x=>x.Cards.OrderBy(x=>x.IndexNumber)));            
        }
    }
}