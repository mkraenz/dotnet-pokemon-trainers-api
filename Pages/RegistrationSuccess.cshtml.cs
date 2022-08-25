using dotnettest.Extensions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dotnettest.Pages
{
    [AllowAnonymous]
    public class RegistrationSuccess : PageModel
    {

        public AuthenticationScheme[]? AuthenticationSchemes { get; set; }

        public async Task OnGetAsync()
        {
            AuthenticationSchemes = await HttpContext.GetExternalProvidersAsync();
        }
    }
}