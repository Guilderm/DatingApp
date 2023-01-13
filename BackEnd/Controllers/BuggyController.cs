using BackEnd.Data;
using BackEnd.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers;

[Route("api/[controller]")]
public class BuggyController : BaseController
{
    private readonly DataContext _context;

    public BuggyController(DataContext context)
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
    public ActionResult<AppUser> GetnotFound()
    {
        var thing = _context.Users.Find(-1);
        if (thing == null) return NotFound();
        return thing;
    }

    [HttpGet("Server-error")]
    public ActionResult<string> GetServerError()
    {
        var thing = _context.Users.Find(-1);
            var thingToReturn = thing.ToString();
            return thingToReturn;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("this was not a good request");
    }
}