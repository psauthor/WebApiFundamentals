using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCodeCamp.Models;

namespace TheCodeCamp.Data
{
    public class CampMappingProfile : Profile
    {
        public CampMappingProfile()
        {
            CreateMap<Camp, CampModel>()
                .ForMember(c => c.Venue, opt => opt.MapFrom(m => m.Location.VenueName))
                .ForMember(c => c.Address1, opt => opt.MapFrom(m => m.Location.Address1))
                .ForMember(c => c.Address2, opt => opt.MapFrom(m => m.Location.Address2))
                .ForMember(c => c.Address3, opt => opt.MapFrom(m => m.Location.Address3))
                .ForMember(c => c.City, opt => opt.MapFrom(m => m.Location.CityTown))
                .ForMember(c => c.State, opt => opt.MapFrom(m => m.Location.StateProvince))
                .ForMember(c => c.PostalCode, opt => opt.MapFrom(m => m.Location.PostalCode))
                .ForMember(c => c.Country, opt => opt.MapFrom(m => m.Location.Country));
            CreateMap<Talk, TalkModel>();
            CreateMap<Speaker, SpeakerModel>();
        }
    }
}
