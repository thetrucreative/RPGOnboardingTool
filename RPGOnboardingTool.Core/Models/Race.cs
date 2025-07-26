// RPGOnboardingTool.Core/Models/Race.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RPGOnboardingTool.Core.Enums;

namespace RPGOnboardingTool.Core.Models
{
    public class Race
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool CanHaveFinanceChip { get; set; } = true;

        // Navigation properties for related data
        public ICollection<RaceSkill> SpeciesSkills { get; set; } = new List<RaceSkill>();
        public ICollection<Character> Characters { get; set; } = new List<Character>();
        public ICollection<RaceStatLimit> StatLimits { get; set; } = new List<RaceStatLimit>();
    }

    public class RaceSkill
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RaceId { get; set; } // Foreign key to Race
        public Race Race { get; set; } = null!; // Navigation property
        public string SkillName { get; set; } = string.Empty;
        public int Rank { get; set; }
    }

    public class RaceStatLimit
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RaceId { get; set; }
        public Race Race { get; set; } = null!;
        public StatType StatType { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
}