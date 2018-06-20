using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EMS.Domain.Models;
using EMS.Web.Services;
using EMS.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EMS.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }
        public IEmailSender EmailSender { get; }
        public ILogger Logger { get; }
        public IEmailViewRenderer EmailViewRenderer { get; }

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            ILoggerFactory loggerFactory,
            IEmailViewRenderer emailViewRenderer)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            EmailSender = emailSender;
            Logger = loggerFactory.CreateLogger<AccountController>();
            EmailViewRenderer = emailViewRenderer;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel viewModel, string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            IActionResult actionResult = View(viewModel);

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(viewModel.Email);
                if (user != null)
                {
                    if (!(await UserManager.IsEmailConfirmedAsync(user)))
                    {
                        ModelState.AddModelError("Email", "Your account has not been activated yet. Please activate your account first.");
                    }
                    else
                    {
                        var result = await SignInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, false);
                        if (result.Succeeded)
                        {
                            Logger.LogInformation(1, "User logged in.");
                            actionResult = RedirectToLocal(returnUrl);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid username or password");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }
            return actionResult;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            ViewBag.StateProvinceList = RetrieveStatesProvinces("US");
            RegisterViewModel viewModel = new RegisterViewModel();
            viewModel.Country = "US";
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            ActionResult actionResult = View(viewModel);
            string confirmationToken;

            if (viewModel.Country == "Other" && string.IsNullOrWhiteSpace(viewModel.OtherCountry))
            {
                ModelState.AddModelError("OtherCountry", "If you select other for your country, you must specify the country you are from.");
            }

            if (viewModel.Country == "Other" && string.IsNullOrWhiteSpace(viewModel.OtherStateProvince))
            {
                ModelState.AddModelError("OtherStateProvince", "If you select other for your country, you must specify the state or province you are from.");
            }

            if (ModelState.IsValid)
            {
                var manager = new User()
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Address1 = viewModel.Address1,
                    Address2 = viewModel.Address2,
                    Country = viewModel.Country,
                    OtherCountry = viewModel.OtherCountry,
                    City = viewModel.City,
                    StateProvince = viewModel.StateProvince,
                    OtherStateProvince = viewModel.OtherStateProvince,
                    Zip = viewModel.Zip,
                    PhoneNumber = viewModel.PhoneNumber,
                    SecondaryPhoneNumber = viewModel.SecondaryPhoneNumber,
                    UserName = viewModel.Email,
                    Email = viewModel.Email,
                    EmailConfirmed = false,
                    MemberSince = DateTimeOffset.Now,
                    HowDidYouHearAboutUs = viewModel.HowDidYouHearAboutUs
                };

                var result = await UserManager.CreateAsync(manager, viewModel.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(manager, "Manager");
                    confirmationToken = await UserManager.GenerateEmailConfirmationTokenAsync(manager);
                    await SendVerificationEmailAsync(manager, confirmationToken);
                    actionResult = RedirectToAction("ThankYou", "Account", new { email = manager.Email });
                }
                else
                {
                    ViewBag.StateProvinceList = RetrieveStatesProvinces(viewModel.Country);
                    AddErrors(result);
                }
            }
            else
            {
                ViewBag.StateProvinceList = RetrieveStatesProvinces(viewModel.Country);
            }

            return actionResult;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await SignInManager.SignOutAsync();
            Logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Team");
        }

        [AllowAnonymous]
        public IActionResult ThankYou(string email)
        {
            ViewBag.EmailAddress = email;
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> ActivateUser(string u, string v)
        {
            string username = WebUtility.UrlDecode(u);
            ViewBag.MessageText = "<p>Your account is still being created, please come back in about 5 minutes and try to verify your account again.</p>";
            User user;
            IdentityResult result;

            if (!string.IsNullOrWhiteSpace(v))
            {
                user = await UserManager.FindByEmailAsync(username);
                result = await UserManager.ConfirmEmailAsync(user, v);
                if (result.Succeeded)
                {
                    await SignInAsync(user, false);
                    await SendCongratulationsEmail(user);
                    return RedirectToAction("Index", "Team");
                }
                else
                {
                    ViewBag.MessageText = "<p>Your account cannot be activated since the user cannot be found. Please try to create your account again.</p>";
                }
            }

            return View();
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            ActionResult actionResult = View(model);
            string code, callbackUrl;
            User user;

            if (ModelState.IsValid)
            {
                user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user)))
                {
                    ModelState.AddModelError("", "The user either does not exist or is not confirmed.");
                }
                else
                {
                    code = await UserManager.GeneratePasswordResetTokenAsync(user);
                    callbackUrl = $"{Request.Scheme}://{Request.Host.Value}{Url.RouteUrl("default", new { Controller = "Account", Action = "ResetPassword", userId = user.Id, code = code })}";
                    //callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Protocol);
                    await SendForgotPasswordEmail(user, callbackUrl);
                    actionResult = RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
            }

            // If we got this far, something failed, redisplay form
            return actionResult;
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                return View("Error");
            }
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "No user found.");
                    return View();
                }
                IdentityResult result = await UserManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                else
                {
                    AddErrors(result);
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #region Helpers
        private async Task SignInAsync(User user, bool isPersistent)
        {
            await SignInManager.SignOutAsync();
            await SignInManager.SignInAsync(user, isPersistent);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(TeamController.Index), "Team");
            }
        }

        private async Task SendVerificationEmailAsync(User user, string confirmationToken)
        {
            var viewDataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            string verifyUrl = $"{Request.Scheme}://{Request.Host.Value}{Url.RouteUrl("default", new { Controller = "Account", Action = "ActivateUser", u = WebUtility.UrlEncode(user.Email), v = confirmationToken })}";
            viewDataDictionary.Model = new VerificationEmailViewModel
            {
                EmailAddress = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                VerifyUrl = verifyUrl
            };

            var message = await EmailViewRenderer.RenderAsync("Verification", viewDataDictionary);

            await EmailSender.SendEmailAsync(user.Email, "Account Activation", message);
        }

        private async Task SendCongratulationsEmail(User user)
        {
            var viewDataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            string createTeamUrl = $"{Request.Scheme}://{Request.Host.Value}{Url.RouteUrl("default", new { Controller = "Team", Action = "Index" })}";
            viewDataDictionary.Model = new CongratulationsViewModel
            {
                FirstName = user.FirstName,
                CreateTeamUrl = createTeamUrl
            };

            var emailRenderer = HttpContext.RequestServices.GetRequiredService<IEmailViewRenderer>();
            var message = await emailRenderer.RenderAsync("Congratulations", viewDataDictionary);

            await EmailSender.SendEmailAsync(user.Email, "Welcome!", message);
        }

        private async Task SendForgotPasswordEmail(User user, string callbackUrl)
        {
            var viewDataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            string createTeamUrl = $"{Request.Scheme}://{Request.Host.Value}{Url.RouteUrl("default", new { Controller = "Team", Action = "Create" })}";
            viewDataDictionary.Model = new ForgotPasswordViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                CallbackUrl = callbackUrl
            };

            var emailRenderer = HttpContext.RequestServices.GetRequiredService<IEmailViewRenderer>();
            var message = await emailRenderer.RenderAsync("ForgotPassword", viewDataDictionary);

            await EmailSender.SendEmailAsync(user.Email, "Forgot Password", message);
        }

        internal static List<SelectListItem> RetrieveStatesProvinces(string country)
        {
            List<SelectListItem> stateProvinceItems = new List<SelectListItem>();

            if (country == "US")
            {
                foreach (string state in EMS.Domain.Models.Constants.States)
                {
                    stateProvinceItems.Add(new SelectListItem()
                    {
                        Value = state,
                        Text = state
                    });
                }
            }
            else
            {
                foreach (string province in EMS.Domain.Models.Constants.Provinces)
                {
                    stateProvinceItems.Add(new SelectListItem()
                    {
                        Value = province,
                        Text = province
                    });
                }
            }

            return stateProvinceItems;
        }
        #endregion
    }
}