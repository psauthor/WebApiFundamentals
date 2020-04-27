using AutoMapper;
using Microsoft.Ajax.Utilities;
using TheCodeCamp.Models;

namespace TheCodeCamp.Data
{
    public class CampMappingProfile : Profile
    {
        public CampMappingProfile()
        {
            CreateMap<Camp, CampModel>()
                .ForMember(m => m.Venue, opt => opt.MapFrom(m => m.Location.VenueName));
        }
    }
}