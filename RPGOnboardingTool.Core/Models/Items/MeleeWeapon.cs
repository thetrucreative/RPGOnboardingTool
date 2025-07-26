// RPGOnboardingTool.Core/Models/Items/MeleeWeapon.cs
namespace RPGOnboardingTool.Core.Models.Items
{
    public class MeleeWeapon : Item
    {
        public string Damage { get; set; } = string.Empty;
        public string DamageType { get; set; } = string.Empty; // e.g., "Slashing", "Piercing", "Blunt"
        public int RequiredStrength { get; set; }
        public string SpecialEffects { get; set; } = string.Empty;
    }
}