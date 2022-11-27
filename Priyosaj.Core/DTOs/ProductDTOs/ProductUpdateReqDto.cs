using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using AutoMapper;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.MapperProfile;

namespace Priyosaj.Core.DTOs.ProductDTOs;
public class ProductUpdateReqDto : IMapFrom<Product>
{
    [Required] public Guid Id { get; set; }
    [Required] public string Title { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;
    [Required] public decimal RegularPrice { get; set; }
    [Required] public decimal? DiscountPrice { get; set; }
    [Required] public int StockCount { get; set; }
    public ImageDto? DisplayImageToUpdate { get; set; } = null;
    // [Required] public ICollection<ImageDto> Images { get; set; } = null!;
    public ICollection<Specification> Specifications { get; set; } = new List<Specification>();
    public ICollection<Guid>? ImagesToDelete { get; set; } = null;
    

    // Guid to avoid object cycle
    [Required] public ICollection<Guid>? ProductCategories { get; set; } = null;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductUpdateReqDto, Product>()
            .ForMember(
                dest => dest.DisplayImageId,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.Images,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.ProductCategories,
                opt => opt.Ignore())
            .ForMember(dest => dest.Specifications, opt => opt.MapFrom(
                src => JsonSerializer.Serialize(
                    src.Specifications,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    })))
            .ReverseMap();
    }

}