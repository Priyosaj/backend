using AutoMapper;
using Priyosaj.Api.DTOs.ProductCategoryDTOs;
using Priyosaj.Api.DTOs.ProductDTOs;
using Priyosaj.Contacts.Entities.ProductEntities;

namespace Priyosaj.Api.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Product, ProductResponseDto>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductCategory, ProductCategoryResponseDto>()
            .ForMember(d => d.SubCategories, opt => opt.MapFrom(s => s.SubCategories));
        CreateMap<ProductCategoryCreateDto, ProductCategory>();
    }
}