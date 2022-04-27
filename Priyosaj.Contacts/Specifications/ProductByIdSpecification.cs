using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Priyosaj.Contacts.Entities.Product;

namespace Priyosaj.Contacts.Specifications;

public class ProductByIdSpecification : BaseSpecification<Product>
{
    public ProductByIdSpecification(Guid id) : base(p => p.Id == id)
    {
        AddInclude(x => x.Include(p => p.ProductCategories));
    }
}