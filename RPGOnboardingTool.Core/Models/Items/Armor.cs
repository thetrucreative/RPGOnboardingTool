// RPGOnboardingTool.Core/Models/Items/Armor.cs
namespace RPGOnboardingTool.Core.Models.Items
{
    public class Armor : Item
    {
        public int ArmorValue { get; set; }
        public string ArmorType { get; set; } = string.Empty; // e.g., "Light", "Medium", "Heavy"
        public int Penalty { get; set; } // e.g., penalty to Dexterity-based skills
        public string SpecialEffects { get; set; } = string.Empty;
    }
}
