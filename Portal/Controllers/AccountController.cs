
using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;

namespace Portal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;

        public AccountController(
            UserManager<Person> userManager,
            SignInManager<Person> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Action for user registration
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new user object with the provided information
                var user = new Person
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Gender = model.Gender,
                    Address = model.Address,
                    Birthdate = (DateTime)model.BirthDate!,
                    Name = model.Name
                };

                // Attempt to create the user
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Sign in the user after a successful registration
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    // Redirect to the home page or another desired page
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        if (error.Code == "DuplicateEmail")
                        {
                            ModelState.AddModelError("Email", "Dit e-mailadres is al in gebruik.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            // If registration fails, return to the registration page with error messages
            return View(model);
        }

        // Action for user login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to sign in the user
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        // Redirect to the home page or another desired page
                        return RedirectToAction("Index", "Home");
                    }
                }

                // Display the custom error message when the email does not exist in the database
                ModelState.AddModelError("Email", "Account bestaat niet of wachtwoord is onjuist.");
            }

            // If login fails, return to the login page with error messages
            return View(model);
        }

        // Action for user logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View();
        }

        // Remote validation for email availability
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailAvailable(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            return Json($"Dit e-mailadres ({email}) is al in gebruik.");
        }
    }
}

