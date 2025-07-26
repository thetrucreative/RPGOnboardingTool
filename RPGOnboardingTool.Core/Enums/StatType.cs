using System.Text.Json.Serialization;

namespace RPGOnboardingTool.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatType
    {
        Strength,
        Dexterity,
        Knowledge,
        Concentration,
        Charisma,
        Cool,
        Luck
    }
}