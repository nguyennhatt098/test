using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scenario.Web.Enum;

namespace Scenario.Web.Controllers
{
	public class LoginController : Controller
	{
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
		{
            var checkUser =await _userManager.FindByNameAsync("admin");
            if(checkUser == null)
            {
                var user = new IdentityUser { UserName = "admin", Email = "abc@gmail.com" };
                var createUser = await _userManager.CreateAsync(user, "Admin@123@123");
                var identityRole = new IdentityRole(ERole.CreateOrder.ToString());
                await _roleManager.CreateAsync(identityRole);
                identityRole= new IdentityRole(ERole.ViewOrder.ToString());
                await _roleManager.CreateAsync(identityRole);
                await _userManager.AddToRoleAsync(user, ERole.CreateOrder.ToString());
                await _userManager.AddToRoleAsync(user, ERole.ViewOrder.ToString());
            }
           
            var result = await _signInManager.PasswordSignInAsync(
                   "admin",
                   "Admin@123@123",
                   true,
                   true
               );

            return Redirect("/order");
		}
        public async Task<IActionResult> NotFound()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}
