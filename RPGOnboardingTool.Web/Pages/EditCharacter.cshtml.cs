using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPGOnboardingTool.Application.Services;
using RPGOnboardingTool.Core.Models;
using System;
using System.Threading.Tasks;

namespace RPGOnboardingTool.Web.Pages
{
    public class EditCharacterModel : PageModel
    {
        private readonly ICharacterService _characterService;

        public EditCharacterModel(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public Character? Character { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Character = await _characterService.GetCharacterForDetailByIdAsync(Id);
            if (Character == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
