namespace Priyosaj.Contacts.DTOs.ProductCategoryDTOs;
public class ProductCategoryCreateDto
{
    public string Title { get; set; }
    public Guid? ParentId { get; set; }=null;
}