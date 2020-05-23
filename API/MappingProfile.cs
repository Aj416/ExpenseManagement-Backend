using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category1))
            .ReverseMap();

            CreateMap<Category, CreateUpdateCategoryDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category1))
            .ReverseMap();

            CreateMap<Source, SourceDto>()
            .ReverseMap();
        }
    }
}
