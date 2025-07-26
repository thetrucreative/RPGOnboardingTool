// RPGOnboardingTool.Core/Models/Items/GeneralItem.cs
using System.Collections.Generic;

namespace RPGOnboardingTool.Core.Models.Items
{
    public class GeneralItem : Item
    {
        public string Effect { get; set; } = string.Empty;
        public ICollection<CharacterGeneralItem> CharacterGeneralItems { get; set; } = new List<CharacterGeneralItem>();
    }
}