// RPGOnboardingTool.Core/Models/Items/Item.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace RPGOnboardingTool.Core.Models.Items
{
    public abstract class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = string.Empty;

        public int Cost { get; set; }

        public int Weight { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}