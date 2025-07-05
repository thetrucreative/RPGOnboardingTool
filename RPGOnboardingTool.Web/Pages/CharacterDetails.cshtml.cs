using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace RPGOnboardingTool.Web.Pages
{
    public class CharacterDetailsModel : PageModel
    {
        [FromRoute]
        public Guid Id { get; set; }

        public void OnGet()
        {
        }
    }
}
