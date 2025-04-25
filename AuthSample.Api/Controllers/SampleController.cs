using AuthSample.Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthSample.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{
    [HttpGet]
    [Route("/[controller]/[action]")]
    [AllowAnonymous]
    public ActionResult Anonymous()
    {
        return Ok();
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    [Authorize(Policy = AuthPolicy.NO_RESTRICTIONS_POLICY)]
    public ActionResult NoRestrictions()
    {
        return Ok();
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    [Authorize(Policy = AuthPolicy.ADMIN_POLICY)]
    public ActionResult Admin()
    {
        return Ok();
    }

    [HttpGet]
    [Authorize(Policy = AuthPolicy.VIEWER_POLICY)]
    public ActionResult Get()
    {
        return Ok();
    }

    [HttpPost]
    [Authorize(Policy = AuthPolicy.MODIFIER_POLICY)]
    public ActionResult Post()
    {
        return Ok();
    }

    [HttpPut]
    [Authorize(Policy = AuthPolicy.MODIFIER_POLICY)]
    public ActionResult Put()
    {
        return Ok();
    }

    [HttpDelete]
    [Authorize(Policy = AuthPolicy.MODIFIER_POLICY)]
    public ActionResult Delete()
    {
        return Ok();
    }
}
