using System.Text.Json.Serialization;

namespace RPGOnboardingTool.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatType
    {
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma,
        Knowledge,
        Coordination,
        Cool,
        Composure
    }
}
