using Microsoft.AspNetCore.Mvc;

namespace Scenario.Web.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
