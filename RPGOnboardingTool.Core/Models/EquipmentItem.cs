using RPGOnboardingTool.Core.Enums;

namespace RPGOnboardingTool.Core.Models
{
    public class EquipmentItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Cost { get; set; }
        public int WeightFactor { get; set; }
        public bool IsStartingGear { get; set; } = false;
        public EquipmentType Type { get; set; }
        public ICollection<CharacterEquipment> CharacterEquipment { get; set; } = new List<CharacterEquipment>();
    }
}