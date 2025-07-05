using RPGOnboardingTool.Core.Enums;

namespace RPGOnboardingTool.Core.Models
{
    public class Stat
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CharacterId { get; set; } 
        public Character Character { get; set; } = null!; 
        public StatType Type { get; set; } 
        public int Value { get; set; }
    }
}
