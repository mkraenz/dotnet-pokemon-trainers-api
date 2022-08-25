using dotnettest.Extensions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dotnettest.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {

        public AuthenticationScheme[] AuthenticationSchemes { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            AuthenticationSchemes = await HttpContext.GetExternalProvidersAsync();
        }
    }
}