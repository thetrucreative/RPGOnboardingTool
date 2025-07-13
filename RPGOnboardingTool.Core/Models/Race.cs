// RPGOnboardingTool.Core/Models/Race.cs
using RPGOnboardingTool.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPGOnboardingTool.Core.Models
{
    public class Race
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BaseInitiative { get; set; }
        public int BaseMovement { get; set; }
        public int MaxHp { get; set; } // Represents BaseHitPoints
        public int MaxLuck { get; set; }
        public int BaseClosingSpeed { get; set; }
        public int BaseRushingSpeed { get; set; }
        public int BaseEncumbrance { get; set; }
        public bool CanHaveFinanceChip { get; set; } // For Ebonite rule

        // Navigation properties for related data
        public ICollection<RaceStatLimit> StatLimits { get; set; } = new List<RaceStatLimit>();
        public ICollection<RaceSkill> SpeciesSkills { get; set; } = new List<RaceSkill>();
        public ICollection<Character> Characters { get; set; } = new List<Character>();
    }

    public class RaceStatLimit
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RaceId { get; set; } // Foreign key to Race
        public Race Race { get; set; } = null!; // Navigation property
        public StatType StatType { get; set; } // Using the enum
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }

    public class RaceSkill
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RaceId { get; set; } // Foreign key to Race
        public Race Race { get; set; } = null!; // Navigation property
        public string SkillName { get; set; } = string.Empty;
        public int Rank { get; set; }
    }
}