// RPGOnboardingTool.Application/DTOs/CharacterUpdateDto.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RPGOnboardingTool.Core.Enums;

namespace RPGOnboardingTool.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for updating an existing character.
    /// </summary>
    public class CharacterUpdateDto
    {
        [Required]
        public Guid Id { get; set; }

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
        public int StatPointsRemaining { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Skill points remaining cannot be negative.")]
        public int SkillPointsRemaining { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Credits cannot be negative.")]
        public int Credits { get; set; }

        public string? RowVersion { get; set; }
    }
}