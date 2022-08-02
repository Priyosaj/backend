using AutoMapper;
using Priyosaj.Core.DTOs.ProductCategoryDTOs;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Service.DTOs.ProductDTOs;

namespace Priyosaj.Business.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Product, ProductResponseDto>();

        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DiscountPrice, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
            .ForMember(dest => dest.RegularPrice, opt => opt.MapFrom(src => src.Price));

        CreateMap<ProductCategory, ProductCategoryResponseDto>()
            .ForMember(d => d.SubCategories, opt => opt.MapFrom(s => s.SubCategories));
        
        CreateMap<ProductCategoryCreateDto, ProductCategory>();
    }
}