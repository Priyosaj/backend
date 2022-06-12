namespace Priyosaj.Contacts.DTOs.ProductCategoryDTOs;
public class ProductCategoryUpdateDto
{
    public string Title { get; set; }
    public Guid? ParentId { get; set; } = null;
}