using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPGOnboardingTool.Application.Services;
using RPGOnboardingTool.Core.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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

        public required Character Character { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var character = await _characterService.GetCharacterForDetailByIdAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            Character = character;
            return Page();
        }

        public async Task<IActionResult> OnPostUploadAvatarAsync(Guid id, IFormFile avatarFile)
        {
            if (avatarFile == null || avatarFile.Length == 0)
            {
                TempData["ErrorMessage"] = "Please select a file to upload.";
                return RedirectToPage(new { id });
            }

            try
            {
                await _characterService.UpdateCharacterAvatarAsync(id, avatarFile);
                TempData["SuccessMessage"] = "Avatar updated successfully.";
            }
            catch (Exception ex)
            {
                // Log the exception
                TempData["ErrorMessage"] = $"An error occurred while uploading the avatar: {ex.Message}";
            }

            return RedirectToPage(new { id });
        }
    }
}
