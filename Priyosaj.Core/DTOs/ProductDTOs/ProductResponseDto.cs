using AutoMapper;
using Priyosaj.Core.DTOs.ProductCategoryDTOs;
using Priyosaj.Core.Entities;
using Priyosaj.Core.Entities.ProductEntities;
// using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.MapperProfile;

namespace Priyosaj.Core.DTOs.ProductDTOs;

public class ProductResponseDto : IMapFrom<Product>
{
    public string Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal RegularPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int? StockCount { get; set; }
    public ImageDto? DisplayImage { get; set; } = null;
    public ICollection<ImageDto> Images { get; set; }
    public ICollection<ProductCategoryResponseDto> ProductCategories { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductResponseDto>()
            .ForMember(
                dest => dest.DisplayImage, 
                opt => opt.MapFrom(
                    src => src.Images.FirstOrDefault(x => x.Id == src.DisplayImageId)))
            .ForMember(
                dest => dest.Images, 
                opt=>opt.MapFrom(
                    src=>src.Images))
            .ReverseMap();
    }
}
