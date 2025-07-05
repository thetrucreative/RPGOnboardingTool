using RPGOnboardingTool.Core.Enums;

namespace RPGOnboardingTool.Core.Models
{
    public class Skill
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CharacterId { get; set; } 
        public Character Character { get; set; } = null!; 
        public string Name { get; set; } = string.Empty;
        public int Rank { get; set; }
        public int Bonus { get; set; }
        public int SuccessDie { get; set; }
        public int SkillDice { get; set; }

        public StatType RelatedStat { get; set; }
    }
}