namespace RPGOnboardingTool.Core.Models
{
    public class EquipmentItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Weight { get; set; }
        public int Cost { get; set; }
        public List<CharacterEquipment> CharacterEquipment { get; set; } = new List<CharacterEquipment>();
    }

    // Junction table for Character and EquipmentItem (many-to-many)
    public class CharacterEquipment
    {
        public Guid CharacterId { get; set; }
        public Character Character { get; set; } = null!;

        public Guid EquipmentItemId { get; set; }
        public EquipmentItem EquipmentItem { get; set; } = null!;
    }
}
