using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Priyosaj.Api.Controllers;
using Xunit;

namespace Priyosaj.Tests.Api;

public class ProductControllerTest
{
    [Fact]
    public async Task TestProducts()
    {
        var logger = new Mock<ILogger<ProductController>>();
        var controller = new ProductController(logger.Object);
        var res = await controller.GetProductAsync();
        Assert.True(res is OkObjectResult);
    }
}