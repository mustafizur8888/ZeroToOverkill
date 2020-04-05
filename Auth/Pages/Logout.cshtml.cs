using System.Threading.Tasks;
using Auth.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Auth.Pages
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<PlayBallUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<PlayBallUser> signInManager,
            ILogger<LogoutModel> logger
            )
        {
            _signInManager = signInManager;
            _logger = logger;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            return Page();
        }
    }
}