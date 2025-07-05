// RPGOnboardingTool.Core/Models/CharacterEquipment.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace RPGOnboardingTool.Core.Models
{
    public class CharacterEquipment
    {
        public Guid CharacterId { get; set; }
        public Character Character { get; set; } = null!;

        public Guid EquipmentItemId { get; set; }
        public EquipmentItem EquipmentItem { get; set; } = null!;
        public int Quantity { get; set; }
    }
}