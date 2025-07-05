using System;

namespace RPGOnboardingTool.Application.DTOs
{
    public class CharacterSummaryDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string RaceName { get; set; }
        public required string TrainingPackageName { get; set; }
    }
}
