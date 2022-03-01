using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Priyosaj.Api.Errors;
using Priyosaj.Business.Data;

namespace Priyosaj.Api.Controllers;

public class BuggyController : BaseApiController
{
    private readonly StoreContext _context;
    private readonly Guid _guid = Guid.Parse("b40cc5e0-4295-4ce8-beb2-3a9706e6a1e2");

    public BuggyController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
        var thing = _context.Products.Find(_guid);

        if (thing == null) return NotFound(new ApiResponse(404));

        return Ok();
    }
    
    [HttpGet("test-auth")]
    [Authorize]
    public ActionResult GetSecretStuff()
    {
        return Ok("Super Secret Stuff");
    }

    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
        var thing = _context.Products.Find(_guid);

        if (thing != null)
        {
            var thingToReturn = thing.ToString();
        }

        return Ok();
    }

    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }

    [HttpGet("badrequest/{id}")]
    public ActionResult GetNotFoundRequest(int id)
    {
        return Ok();
    }
}