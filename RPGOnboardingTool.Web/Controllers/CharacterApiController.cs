using Microsoft.AspNetCore.Mvc;
using RPGOnboardingTool.Application.DTOs;
using RPGOnboardingTool.Application.Services;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPGOnboardingTool.Core.Models;

namespace RPGOnboardingTool.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterApiController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterApiController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("races")]
        public async Task<IActionResult> GetRaces()
        {
            var races = await _characterService.GetAllRacesAsync();
            return Ok(races);
        }

        [HttpGet("training-packages")]
        public async Task<IActionResult> GetTrainingPackages()
        {
            var packages = await _characterService.GetAllTrainingPackagesAsync();
            return Ok(packages);
        }

        [HttpGet("traits")]
        public async Task<IActionResult> GetTraits()
        {
            var traits = await _characterService.GetAllTraitsAsync();
            return Ok(traits);
        }

        [HttpGet("equipment")]
        public async Task<IActionResult> GetEquipment()
        {
            var equipment = await _characterService.GetAllEquipmentAsync();
            return Ok(equipment);
        }

        [HttpGet("skills")]
        public async Task<IActionResult> GetSkills()
        {
            var skills = await _characterService.GetAllSkillsAsync();
            return Ok(skills);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharacter([FromBody] CharacterCreationDto characterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var character = await _characterService.CreateCharacterAsync(characterDto);
                return CreatedAtAction(nameof(GetCharacter), new { id = character.Id }, character);
            }
            catch (Exception ex)
            {
                // log  exception.
                return StatusCode(500, $"An error occurred while creating the character: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacter(Guid id)
        {
            var character = await _characterService.GetCharacterForDetailByIdAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            
            return Ok(character);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter(Guid id, [FromBody] CharacterUpdateDto characterDto)
        {
            if (id != characterDto.Id)
            {
                return BadRequest("Character ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _characterService.UpdateCharacterAsync(id, characterDto);
                
                var updatedCharacter = await _characterService.GetCharacterForDetailByIdAsync(id);
                if (updatedCharacter == null)
                {
                    return NotFound();
                }

                return Ok(updatedCharacter);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(new { message = "The character was modified by another user. Please reload and try again.", error = ex.Message });
            }
            catch (Exception ex)
            {
                // log  exception.
                return StatusCode(500, $"An error occurred while updating the character: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(Guid id)
        {
            try
            {
                // rmbr to get the user ID from the authenticated user's claims.
                var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
                await _characterService.DeleteCharacterAsync(id, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                // In a real app, you'd log this exception.
                return StatusCode(500, $"An error occurred while deleting the character: {ex.Message}");
            }
        }
    }
}