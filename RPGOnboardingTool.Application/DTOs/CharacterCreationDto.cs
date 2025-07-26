// RPGOnboardingTool.Application/DTOs/CharacterCreationDto.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
using RPGOnboardingTool.Core.Enums; 

namespace RPGOnboardingTool.Application.DTOs
{
    /// Data Transfer Object for creating a new character.
    public class CharacterCreationDto
    {
        [Required(ErrorMessage = "Character Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Character Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "User ID is required.")]
        public Guid UserId { get; set; } 

        [Required(ErrorMessage = "Race selection is required.")]
        public Guid RaceId { get; set; }

        public Guid? TrainingPackageId { get; set; } 

        public Dictionary<string, int> AllocatedStats { get; set; } = new Dictionary<string, int>();

        public List<SkillSelectionDto> SelectedSkills { get; set; } = new List<SkillSelectionDto>();

        public List<TraitSelectionDto> SelectedTraits { get; set; } = new List<TraitSelectionDto>();

        public List<EquipmentSelectionDto> SelectedEquipment { get; set; } = new List<EquipmentSelectionDto>();

        public bool ChooseFinanceChip { get; set; } = false; 

        [Range(0, int.MaxValue, ErrorMessage = "Stat points remaining cannot be negative.")]
        public int StatPointsRemaining { get; set; } = 0; 

        [Range(0, int.MaxValue, ErrorMessage = "Skill points remaining cannot be negative.")]
        public int SkillPointsRemaining { get; set; } = 0; 

        [Range(0, int.MaxValue, ErrorMessage = "Credits cannot be negative.")]
        public int Credits { get; set; } = 0; 
    }

    public class SkillSelectionDto
    {
        [Required(ErrorMessage = "Skill Name is required.")]
        public string SkillName { get; set; } = string.Empty;

        [Range(0, 5, ErrorMessage = "Skill Rank must be between 0 and 5.")] 
        public int Rank { get; set; }
    }

    public class TraitSelectionDto
    {
        [Required(ErrorMessage = "Trait ID is required.")]
        public Guid TraitId { get; set; }

        [Range(1, 3, ErrorMessage = "Trait Rank must be between 1 and 3.")] 
        public int Rank { get; set; }

        public string? Detail { get; set; } 
    }

    public class EquipmentSelectionDto
    {
        [Required(ErrorMessage = "Equipment Item ID is required.")]
        public Guid EquipmentItemId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; } = 1;
    }

    public class GeneralItemSelectionDto
    {
        [Required(ErrorMessage = "General Item ID is required.")]
        public Guid GeneralItemId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; } = 1;
    }
}