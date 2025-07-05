// RPGOnboardingTool.Core/Models/Trait.cs
using RPGOnboardingTool.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace RPGOnboardingTool.Core.Models
{
    public class Trait
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TraitType Type { get; set; } // Advantage or Disadvantage
        public int BasePointCost { get; set; } // Cost in skill points
        public bool IsUnique { get; set; } // Can only be taken once
        public int MaxRankAtCreation { get; set; } = 1; // Max rank allowed at character creation
        public bool RequiresDetail { get; set; } = false; // Does this trait require a specific detail from the player (e.g., "Addiction: Nicotine")

        // Navigation property
        public List<CharacterTrait> CharacterTraits { get; set; } = new List<CharacterTrait>();
    }
}