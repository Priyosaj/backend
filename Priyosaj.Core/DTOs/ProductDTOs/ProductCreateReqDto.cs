using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using AutoMapper;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.MapperProfile;

namespace Priyosaj.Core.DTOs.ProductDTOs;

public class ProductCreateReqDto : IMapFrom<Product>
{
    [Required] public string Title { get; set; } = string.Empty;

    [Required] public string Description { get; set; } = string.Empty;

    [Required] public Decimal Price { get; set; }

    public ICollection<Guid> ProductCategoriesId { get; set; } = new List<Guid>();
    public ICollection<Specification> Specifications { get; set; } = new List<Specification>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductCreateReqDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DiscountPrice, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
            .ForMember(dest => dest.RegularPrice, opt => opt.MapFrom(src => src.Price))
            // .ForMember(dest => dest.Specification, opt => opt.MapFrom(src => src.Specification))
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