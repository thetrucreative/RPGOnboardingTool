using RPGOnboardingTool.Core.Models.Items;

namespace RPGOnboardingTool.Core.Models
{
    public class CharacterGeneralItem
    {
        public Guid CharacterId { get; set; }
        public Character? Character { get; set; }

        public Guid GeneralItemId { get; set; }
        public GeneralItem? GeneralItem { get; set; }

        public int Quantity { get; set; }
    }
}