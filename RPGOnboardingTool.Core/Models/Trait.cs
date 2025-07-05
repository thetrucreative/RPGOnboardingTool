namespace RPGOnboardingTool.Core.Models
{
    public class Trait
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<CharacterTrait> CharacterTraits { get; set; } = new List<CharacterTrait>();
    }

    // Junction table for Character and Trait (many-to-many)
    public class CharacterTrait
    {
        public Guid CharacterId { get; set; }
        public Character Character { get; set; } = null!;

        public Guid TraitId { get; set; }
        public Trait Trait { get; set; } = null!;
    }
}
