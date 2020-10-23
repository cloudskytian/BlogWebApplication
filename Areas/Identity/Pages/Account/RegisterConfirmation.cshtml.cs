using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogWebApplication.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        [TempData]
        public string RegisterStatusMessage { get; set; }

        public IActionResult OnGet(string registerStatusMessage)
        {
            RegisterStatusMessage = registerStatusMessage;
            return Page();
        }
    }
}
