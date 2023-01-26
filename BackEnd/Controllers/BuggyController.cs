using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
public class BuggyApiController : BaseApiController
{
    private readonly DataContext _context;

    public BuggyApiController(DataContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetSecret()
    {
        return "secret text";
    }

    [HttpGet("Not-Found")]
    public ActionResult<AppUser> GetNotFound()
    {
        AppUser thing = _context.Users.Find(-1);
        return thing == null ? (ActionResult<AppUser>)NotFound() : (ActionResult<AppUser>)thing;
    }

    [HttpGet("Server-error")]
    public ActionResult<string> GetServerError()
    {
        AppUser thing = _context.Users.Find(-1);
        string thingToReturn = thing.ToString();
        return thingToReturn;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("this was not a good request");
    }
}