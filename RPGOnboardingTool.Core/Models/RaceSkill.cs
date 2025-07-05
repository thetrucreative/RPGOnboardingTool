namespace RPGOnboardingTool.Core.Models
{
    public class RaceSkill
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RaceId { get; set; } // Foreign key to Race
        public Race Race { get; set; } = null!; // Navigation property
        public string SkillName { get; set; } = string.Empty;
        public int Rank { get; set; }
    }
}