// RPGOnboardingTool.Core/Models/Stat.cs
using RPGOnboardingTool.Core.Enums;
using System;

namespace RPGOnboardingTool.Core.Models
{
    public class Stat
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier for each stat
        public StatType Type { get; set; } // The type of the stat (e.g., Strength, Intelligence)
        public int Value { get; set; } // The numerical value of the stat
        public Guid CharacterId { get; set; } // Foreign key to the Character this stat belongs to

        // Navigation property to the related Character entity
        public Character Character { get; set; } = null!;
    }
}