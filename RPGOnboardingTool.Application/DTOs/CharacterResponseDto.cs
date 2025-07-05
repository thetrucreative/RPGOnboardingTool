// RPGOnboardingTool.Application/DTOs/CharacterResponseDto.cs
using System;
using System.Collections.Generic;
using RPGOnboardingTool.Core.Enums; 

namespace RPGOnboardingTool.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for returning character details.
    /// This defines the output structure to the client.
    /// </summary>
    public class CharacterResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RaceName { get; set; } = string.Empty;
        public string? TrainingPackageName { get; set; } 

        public int StatPointsRemaining { get; set; }
        public int SkillPointsRemaining { get; set; }
        public int Credits { get; set; }
        public int Unis { get; set; }
        public bool HasFinanceChip { get; set; }
        public int SCL { get; set; }

        public int HitPoints { get; set; }
        public int MaxHitPoints { get; set; }
        public int ClosingSpeed { get; set; }
        public int RushingSpeed { get; set; }
        public int Movement { get; set; }
        public int EncumbranceValue { get; set; }
        public int CurrentWeightCarried { get; set; }

        public List<StatDto> Stats { get; set; } = new List<StatDto>();
        public List<SkillDto> Skills { get; set; } = new List<SkillDto>();
        public List<TraitDto> Traits { get; set; } = new List<TraitDto>();
        public List<EquipmentItemDto> Equipment { get; set; } = new List<EquipmentItemDto>();
    }

    public class StatDto
    {
        public StatType Type { get; set; }
        public string TypeName => Type.ToString(); 
        public int CurrentRank { get; set; }
        public int MinRank { get; set; }
        public int MaxRank { get; set; }
    }

    public class SkillDto
    {
        public string Name { get; set; } = string.Empty;
        public int Rank { get; set; }
        public StatType RelatedStat { get; set; }
        public string RelatedStatName => RelatedStat.ToString(); 
    }

    public class TraitDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TraitType Type { get; set; }
        public string TypeName => Type.ToString(); 
        public int Rank { get; set; }
        public string? Detail { get; set; } 
    }

    public class EquipmentItemDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Cost { get; set; }
        public int WeightFactor { get; set; }
        public EquipmentType Type { get; set; }
        public string TypeName => Type.ToString(); 
        public int Quantity { get; set; }
    }
}