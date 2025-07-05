using Microsoft.AspNetCore.Mvc.RazorPages;
using RPGOnboardingTool.Application.Services;
using RPGOnboardingTool.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace RPGOnboardingTool.Web.Pages
{
    public class CharacterListModel : PageModel
    {
        private readonly ICharacterService _characterService;

        public CharacterListModel(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        public List<CharacterSummaryDto> Characters { get; set; } = new List<CharacterSummaryDto>();

        public async Task OnGetAsync()
        {
            // In a real app, you'd get the user ID from authentication.
            var userId = new Guid("00000000-0000-0000-0000-000000000001"); 
            Characters = await _characterService.GetCharactersByUserIdAsync(userId);
        }
    }
}
