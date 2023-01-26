using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class AdminController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
