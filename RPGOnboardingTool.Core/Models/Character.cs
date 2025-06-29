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
        public List<Stat> Stats { get; set; } = new List<Stat>();
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public List<Trait> CharacterTraits { get; set; } = new List<Trait>();
        public List<EquipmentItem> CharacterEquipment { get; set; } = new List<EquipmentItem>();

        // Derived Attributes
        public int HitPoints { get; set; }
        public int Luck { get; set; }
        public int Initiative { get; set; }
        public int Movement { get; set; }
        public int Closing { get; set; } // (e.g., Movement * 2)
        public int Rushing { get; set; } // (e.g., Movement * 4)

        // Financials
        public int Credits { get; set; }
        public int Units { get; set; }
        public int LadBalance { get; set; }
        public int OperativeRangePoint { get; set; }

        public Character() { } // Parameterless constructor for EF Core
        public Character(Guid userId, string name)
        {
            UserId = userId;
            Name = name;
            // Initialize collections to empty lists to avoid null reference exceptions
            Stats = [];
            Skills = [];
            CharacterTraits = [];
            CharacterEquipment = [];
        }
    }
}
