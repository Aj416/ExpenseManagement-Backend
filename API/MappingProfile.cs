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

            CreateMap<Source, CreateUpdateSourceDto>()
            .ReverseMap();

            CreateMap<Expense, ExpenseWithDetailDto>()
            .ForMember(dest => dest.ExpenseId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ExpenseDetailId, opt => opt.MapFrom(src => src.DetailId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Detail.Amount))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Detail.CategoryId))
            .ForMember(dest => dest.SourceId, opt => opt.MapFrom(src => src.Detail.SourceId))
            .ReverseMap();

            CreateMap<Expense, CreateUpdateExpenseDetailDto>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Detail.CategoryId))
            .ForMember(dest => dest.SourceId, opt => opt.MapFrom(src => src.Detail.SourceId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Detail.Amount))
            .ReverseMap();

            CreateMap<ExpenseDetail, ExpenseDetailDto>()
            .ReverseMap();
        }
    }
}
