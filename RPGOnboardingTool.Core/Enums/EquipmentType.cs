using System.Text.Json.Serialization;

namespace RPGOnboardingTool.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EquipmentType
    {
        Weapon,
        Armor,
        Accessory,
        Consumable,
        Miscellaneous,
        Utility
    }
}
