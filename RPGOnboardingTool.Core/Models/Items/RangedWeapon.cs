// RPGOnboardingTool.Core/Models/Items/RangedWeapon.cs
namespace RPGOnboardingTool.Core.Models.Items
{
    public class RangedWeapon : Item
    {
        public string Damage { get; set; } = string.Empty;
        public int AmmoCapacity { get; set; }
        public int RateOfFire { get; set; }
        public string SpecialEffects { get; set; } = string.Empty;
    }
}