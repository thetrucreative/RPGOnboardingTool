// RPGOnboardingTool.Core/Models/CharacterEquipment.cs
using System;
using System.ComponentModel.DataAnnotations;
using RPGOnboardingTool.Core.Models.Items;

namespace RPGOnboardingTool.Core.Models
{
    public class CharacterEquipment
    {
        public Guid CharacterId { get; set; }
        public required Character Character { get; set; }

        public Guid EquipmentItemId { get; set; }
        public required EquipmentItem EquipmentItem { get; set; }
        public int Quantity { get; set; }
    }
}