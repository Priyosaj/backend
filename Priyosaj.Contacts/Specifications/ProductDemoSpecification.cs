using Priyosaj.Contacts.Models;

namespace Priyosaj.Contacts.Specifications;

public class ProductDemoSpecification : BaseSpecification<Product>
{
    public ProductDemoSpecification()
    {
        AddInclude(x => x.ProductCategories);
    }
    
    public ProductDemoSpecification(Guid id) : base(x => x.Id == id)
    {
        AddInclude(x => x.ProductCategories);
    }
}