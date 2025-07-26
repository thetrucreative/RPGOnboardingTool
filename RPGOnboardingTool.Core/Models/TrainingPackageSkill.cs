using System;

namespace RPGOnboardingTool.Core.Models
{
    public class TrainingPackageSkill
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TrainingPackageId { get; set; }
        public TrainingPackage TrainingPackage { get; set; } = null!;
        public string SkillName { get; set; } = string.Empty;
        public int Rank { get; set; }
    }
}