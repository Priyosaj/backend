using Microsoft.AspNetCore.Mvc;

namespace Priyosaj.Api.Controllers.SuperControllers;

[ApiController]
[Route("api-editor/[controller]")]
// [Authorize(Policy = UserRolePoliciesConstants.RequireEditorRole)]
public class BaseEditorSuperController : ControllerBase
{
}