using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.MapperProfile;

namespace Priyosaj.Core.DTOs.ProductCategoryDTOs;
public class ProductCategoryUpdateDto: IMapFrom<ProductCategory>
{
    public string Title { get; set; }
    public Guid? ParentId { get; set; } = null;
}