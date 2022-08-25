/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using dotnettest.Extensions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnettest.Controllers
{
    // TODO use /api/signin etc. Needs adjustments to Keycloak and Program.cs (and more?)
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        [HttpPost("~/signin")]
        public async Task<IActionResult> SignIn([FromForm] string provider)
        {
            // Note: the "provider" parameter corresponds to the external
            // authentication provider choosen by the user agent.
            if (string.IsNullOrWhiteSpace(provider))
            {
                return BadRequest();
            }

            if (!await HttpContext.IsProviderSupportedAsync(provider))
            {
                return BadRequest();
            }
            // after all signin stuff is handled with keycloak, redirect user agent to /Trainer
            return Challenge(new AuthenticationProperties { RedirectUri = "/Trainer" }, provider);
        }

        [HttpGet("~/signout")]
        [HttpPost("~/signout")]
        public async Task SignOutCurrentUserAsync()
        {
            // remove Session in keycloak
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            // remove cookie from User client
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // redirects User agent to Index page because of 
            // options.SignedOutRedirectUri = "/";
            // config for keycloak OIDC in root
        }
    }
}