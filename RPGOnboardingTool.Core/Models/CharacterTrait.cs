// RPGOnboardingTool.Core/Models/CharacterTrait.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace RPGOnboardingTool.Core.Models
{
    public class CharacterTrait
    {
        public int Id { get; set; }
        public Guid CharacterId { get; set; }
        public Guid TraitId { get; set; }

        public int Rank { get; set; } 
        public string? Detail { get; set; } 

        public Character Character { get; set; } = null!;
        public Trait Trait { get; set; } = null!;
    }
}