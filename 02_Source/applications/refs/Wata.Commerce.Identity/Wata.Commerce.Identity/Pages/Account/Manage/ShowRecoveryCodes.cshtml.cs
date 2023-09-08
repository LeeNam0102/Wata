using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wata.Commerce.Identity.Pages.Account.Manage
{
    public class ShowRecoveryCodesModel : PageModel
    {
        public string[] RecoveryCodes { get; private set; }

        public IActionResult OnGet()
        {
            RecoveryCodes = (string[])TempData["RecoveryCodes"];
            if (RecoveryCodes == null)
            {
                return RedirectToPage("TwoFactorAuthentication");
            }

            return Page();
        }
    }
}