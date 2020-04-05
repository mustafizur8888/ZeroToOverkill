using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Auth.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Auth.Pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<PlayBallUser> _userManager;
        private readonly SignInManager<PlayBallUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        public string ReturnUrl { get; set; }

        public RegisterModel(UserManager<PlayBallUser> userManager,
            SignInManager<PlayBallUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required]
            [Display(Name = "Password")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} charecters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirm password do not match")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            //returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new PlayBallUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                };
                var result = await _userManager.CreateAsync(user,Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("new User ceated.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbakUrl = Url.Page("/ConfirmEmail",
                        null, new { userId = user.Id, code = code },
                        Request.Scheme);
                    await _emailSender.SendEmailAsync(Input.Email, "Confirm Email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbakUrl)}'>Clicing here </a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return LocalRedirect("~/");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}