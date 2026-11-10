using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TARge25Shop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountController
            (
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = vm.Email,
                    Name = vm.Name,
                    Email = vm.Email,
                    City = vm.City
                };

                var result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeede)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink =
                        Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token },
                        Request.Scheme);

                    EmailTokenDto newsignup = new();
                    newsignup.Token = token;
                    newsignup.Body = $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>Confirm Email</a>";
                    newsignup.Subject = "SHOP Confirm your email";
                    newsignup.To = user.Email;

                }

                // Implement registration logic here
                // For example, create a new user and save it to the database
                // Redirect to a success page or login page after successful registration
                return RedirectToAction("Index", "Home");
            }
            // If we got this far, something failed; redisplay the form
            return View(model);
        }
    }
}
