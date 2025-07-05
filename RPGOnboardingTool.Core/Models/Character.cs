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
        public int SCL { get; set; } 
        public int MaxHitPoints { get; set; } 
        public int HitPoints { get; set; } 
        public int Closing { get; set; } 
        public int Rushing { get; set; } 
        public int Movement { get; set; } 
        public int EncumbranceValue { get; set; } 
        public int CurrentWeightCarried { get; set; } 
        public bool HasFinanceChip { get; set; } 
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
        public ICollection<Stat> Stats { get; set; } = new List<Stat>();
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public ICollection<CharacterTrait> CharacterTraits { get; set; } = new List<CharacterTrait>();
        public ICollection<CharacterEquipment> CharacterEquipment { get; set; } = new List<CharacterEquipment>();

        public Character() { } 

        public Character(Guid userId, string name, Race race)
        {
            UserId = userId;
            Name = name;
            RaceId = race.Id;
            CharacterRace = race;

            // Initialize base values from the race
            StatPointsRemaining = 12; // Starting stat points
            SkillPointsRemaining = 30; // Starting skill points
            Credits = 1500; // Starting credits
            MaxHitPoints = race.MaxHp;
            HitPoints = race.MaxHp;
            Movement = race.BaseMovement;
            Closing = race.BaseClosingSpeed;
            Rushing = race.BaseRushingSpeed;
            EncumbranceValue = race.BaseEncumbrance;
            RowVersion = Array.Empty<byte>(); // Initialize empty array
        }
    }
}
