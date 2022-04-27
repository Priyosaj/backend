using AutoMapper;
using Priyosaj.Api.DTOs;
using Priyosaj.Contacts.Entities.Product;

namespace Priyosaj.Api.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Product, ProductResponseDto>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductCategory, ProductCategoryResponseDto>()
            .ForMember(d => d.SubCategories, opt => opt.MapFrom(s => s.SubCategories));
    }
}