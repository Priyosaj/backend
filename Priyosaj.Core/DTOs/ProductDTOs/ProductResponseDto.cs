// using System.Text.Json;

using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Priyosaj.Core.DTOs.OrderDTOs;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
using Priyosaj.Core.DTOs.ProductCategoryDTOs;
using Priyosaj.Core.Entities;
using Priyosaj.Core.Entities.ProductEntities;
// using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.MapperProfile;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Priyosaj.Core.DTOs.ProductDTOs;

public class ProductResponseDto : IMapFrom<Product>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal RegularPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int? StockCount { get; set; }
    public ImageDto? DisplayImage { get; set; } = null;
    public ICollection<ImageDto> Images { get; set; }
    public ICollection<Specification> Specifications { get; set; } = new List<Specification>();
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
                opt => opt.MapFrom(
                    src => src.Images))
            .ForMember(dest => dest.Specifications, opt => opt.MapFrom<ProductSpecificationsResolver>())
            .ReverseMap();
    }
}

public class ProductSpecificationsResolver : IValueResolver<Product, ProductResponseDto, ICollection<Specification>>
{
    private readonly IConfiguration _config;

    public ProductSpecificationsResolver(IConfiguration config)
    {
        _config = config;
    }

    public ICollection<Specification> Resolve(Product source, ProductResponseDto destination,
        ICollection<Specification> destMember,
        ResolutionContext context)
    {
        var specs = new List<Specification>();
        if (!string.IsNullOrEmpty(source.Specifications))
        {
            specs = JsonSerializer.Deserialize<List<Specification>>(source.Specifications);
        }

        return specs!;
    }
}