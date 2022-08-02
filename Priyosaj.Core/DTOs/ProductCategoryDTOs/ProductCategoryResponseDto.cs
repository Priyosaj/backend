using AutoMapper;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.MapperProfile;

namespace Priyosaj.Core.DTOs.ProductCategoryDTOs;

public class ProductCategoryResponseDto : IMapFrom<ProductCategory>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public ICollection<ProductCategoryResponseDto> SubCategories { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductCategory, ProductCategoryResponseDto>()
            .ForMember(d => d.SubCategories, opt => opt.MapFrom(s => s.SubCategories));
    }
}