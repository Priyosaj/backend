using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Priyosaj.Core.Constants;

namespace Priyosaj.Api.Controllers.SuperControllers;


[ApiController]
[Route("api-editor/[controller]")]
[Authorize(Policy = UserRolePoliciesConstants.RequireEditorRole)]
public class BaseEditorSuperController : ControllerBase
{

}