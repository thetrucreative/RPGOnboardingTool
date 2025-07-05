using RPGOnboardingTool.Core.Enums;

namespace RPGOnboardingTool.Core.Models
{
    public class SkillDefinition
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public StatType RelatedStat { get; set; }
    }
}