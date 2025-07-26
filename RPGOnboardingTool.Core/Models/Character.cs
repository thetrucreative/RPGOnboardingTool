using System.ComponentModel.DataAnnotations;

namespace RPGOnboardingTool.Core.Models
{
    public class Character
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public Guid UserId { get; set; } 
        public string Name { get; set; } = string.Empty;        
        public Guid? RaceId { get; set; } 
        public Race? CharacterRace { get; set; } 

        public Guid? TrainingPackageId { get; set; } 
        public TrainingPackage? CharacterTrainingPackage { get; set; } 
        public int StatPointsRemaining { get; set; } 
        public int SkillPointsRemaining { get; set; } 
        public int Credits { get; set; } 
        public int Unis { get; set; } 
        public int SCL { get; set; } // Security Clearance Level
        public bool HasFinanceChip { get; set; } 

        [Timestamp]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        // Navigation properties
        public ICollection<Stat> Stats { get; set; } = new List<Stat>();
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public ICollection<CharacterTrait> CharacterTraits { get; set; } = new List<CharacterTrait>();
        public ICollection<CharacterEquipment> CharacterEquipment { get; set; } = new List<CharacterEquipment>();
        public ICollection<CharacterGeneralItem> CharacterGeneralItems { get; set; } = new List<CharacterGeneralItem>();

        public string? AvatarUrl { get; set; }

        public Character() { } 

        public Character(Guid userId, string name)
        {
            UserId = userId;
            Name = name;
            StatPointsRemaining = 12; // Starting stat points
            SkillPointsRemaining = 30; // Starting skill points
            Credits = 1500; // Starting credits
        }
    }
}