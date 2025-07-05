using RPGOnboardingTool.Core.Enums;

namespace RPGOnboardingTool.Core.Models
{
    public class TrainingPackage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<TrainingPackageStatRequirement> StatRequirements { get; set; } = new List<TrainingPackageStatRequirement>();
        public List<TrainingPackageSkill> PackageSkills { get; set; } = new List<TrainingPackageSkill>();
    }

    public class TrainingPackageStatRequirement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TrainingPackageId { get; set; } 
        public TrainingPackage TrainingPackage { get; set; } = null!; 
        public StatType StatType { get; set; } // Using the enum
        public int MinValue { get; set; }
    }

    public class TrainingPackageSkill
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TrainingPackageId { get; set; } 
        public TrainingPackage TrainingPackage { get; set; } = null!; 
        public string SkillName { get; set; } = string.Empty;
        public int Rank { get; set; }
    }
}
