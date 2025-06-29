using System.Text.Json.Serialization;

namespace RPGOnboardingTool.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TraitType
    {
        Positive,
        Negative,
        Neutral
    }
}
