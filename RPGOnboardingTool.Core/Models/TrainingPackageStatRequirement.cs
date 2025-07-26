using RPGOnboardingTool.Core.Enums;
using System;

namespace RPGOnboardingTool.Core.Models
{
    public class TrainingPackageStatRequirement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TrainingPackageId { get; set; } // Foreign key to TrainingPackage
        public StatType StatType { get; set; }
        public int MinValue { get; set; }

        public TrainingPackage TrainingPackage { get; set; } = null!; // Navigation property
    }
}