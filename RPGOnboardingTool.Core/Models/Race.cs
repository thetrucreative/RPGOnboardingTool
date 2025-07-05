using RPGOnboardingTool.Core.Enums;

namespace RPGOnboardingTool.Core.Models
{
    public class Race
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BaseInitiative { get; set; }
        public int BaseMovement { get; set; }
        public int MaxHp { get; set; }
        public int MaxLuck { get; set; }

        // Navigation properties for related data (limits, skills)
        public List<RaceStatLimit> StatLimits { get; set; } = new List<RaceStatLimit>();
        public List<RaceSkill> SpeciesSkills { get; set; } = new List<RaceSkill>();
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
