﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dotnettest.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {


        public IndexModel()
        {
        }

        public void OnGet()
        {
        }
    }
}