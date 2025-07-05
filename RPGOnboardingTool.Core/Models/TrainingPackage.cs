// RPGOnboardingTool.Core/Models/TrainingPackage.cs
using RPGOnboardingTool.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPGOnboardingTool.Core.Models
{
    public class TrainingPackage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation properties for related data
        public ICollection<TrainingPackageStatRequirement> StatRequirements { get; set; } = new List<TrainingPackageStatRequirement>();
        public ICollection<TrainingPackageSkill> PackageSkills { get; set; } = new List<TrainingPackageSkill>();
        public ICollection<Character> Characters { get; set; } = new List<Character>();
    }

    public class TrainingPackageStatRequirement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TrainingPackageId { get; set; } // Foreign key to TrainingPackage
        public StatType StatType { get; set; }
        public int MinValue { get; set; }

        public TrainingPackage TrainingPackage { get; set; } = null!; // Navigation property
    }

    public class TrainingPackageSkill
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TrainingPackageId { get; set; } // Foreign key to TrainingPackage
        public string SkillName { get; set; } = string.Empty;
        public int Rank { get; set; }

        public TrainingPackage TrainingPackage { get; set; } = null!; // Navigation property
    }
}