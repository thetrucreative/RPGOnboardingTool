// RPGOnboardingTool.Core/Models/Items/EquipmentItem.cs
using RPGOnboardingTool.Core.Enums;
using System.Collections.Generic;

namespace RPGOnboardingTool.Core.Models.Items
{
    public class EquipmentItem : Item
    {
        public EquipmentType Type { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<CharacterEquipment> CharacterEquipment { get; set; } = new List<CharacterEquipment>();
    }
}