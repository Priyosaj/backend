using System.Linq.Expressions;
using Priyosaj.Contacts.Models;

namespace Priyosaj.Contacts.Specifications;

public class ProductByIdSpecification : BaseSpecification<Product>
{
    public ProductByIdSpecification(Guid id) : base(p => p.Id == id)
    {
        AddInclude(x => x.ProductCategories);
    }
}