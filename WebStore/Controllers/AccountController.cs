using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data.Entities.Account;
using WebStore.Data.Entities;
using WebStore.Data;
using WebStore.Services;
using WebStore.Models.AccountModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using Stripe;
using WebStore.Models;
//using NETCore.MailKit.Core;

namespace WebStore.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;
        private readonly CommonService commonService;
        private readonly IEmailService emailService;
        private readonly Services.AccountService accountService;
        public AccountController(
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
            ApplicationDbContext context,
            Services.AccountService _accountService,
            CommonService commonService,
            IEmailService emailService):base(commonService)
        {
            this.commonService = commonService; this.emailService = emailService;
            accountService = _accountService;
            userManager = _userManager;
            signInManager = _signInManager;
            this.context = context;
            this.emailService = emailService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Check User Exist
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                ViewData["Email"] = userExists.Email;
                return View("UserAlreadyExists");
            }
            //

            Cart cart = new Cart();
            var user = new ApplicationUser()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                //SecurityStamp = Guid.NewGuid().ToString(),
                //EmailConfirmed = true,
                LastName = model.LastName,
                UserName = model.Email,
                Cart = cart
            };
            var result = await userManager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                //await signInManager.SignInAsync(user, isPersistent: false);
                // Signin in EmailVerification

                //Add Token to Verify the email...
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(EmailVerification), "Account", new { token, email = user.Email }, Request.Scheme);
                var message = new Message((new string[] { user.Email! }).ToList(), "Confirmation email link", confirmationLink!);
                emailService.SendEmail(message);

                //return RedirectToAction("Index", "Home");
                ViewData["Email"] = user.Email;
                return View("EmailVerification");
                //
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }


            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {

                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    //Check if logged user is admin then redirect to admin view
                    bool isAdmin = await userManager.IsInRoleAsync(user, "Admin");
                    if (isAdmin)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    
                    if (model.ReturnUrl != null)
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult TestEmail()
        {
            this.emailService.SendEmail(
                new Message((new string[] { "kal04an@gmail.com" }).ToList(),
                "Testing Email Service",
                "Test Email"
                 ));

            return Redirect("/");
        }

        [HttpGet("EmailVerification")]
        public async Task<IActionResult> EmailVerification(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                    //
                }
            }
            //TODO return view да се направи error view
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Details(string username)
        {

            return View(accountService.GetUser(username));
        }

        [HttpGet]
        public async Task<IActionResult> Orders(string username)
        {
            var user = accountService.GetUser(username);
            ViewBag.Orders = await accountService.GetLast10Orders(user.Cart.Id);
            return View();
        }
        [HttpGet("/Account/Order/Details/{id}")]
        public IActionResult OrderDetails(int id)
        {
            Payment payment = accountService.GetOrder(id);
            
            return View(payment);
        }

        
    }
}

