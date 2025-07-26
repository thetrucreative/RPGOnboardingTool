using Microsoft.EntityFrameworkCore;
using RPGOnboardingTool.Core.Models;
using RPGOnboardingTool.Core.Models.Items;
using RPGOnboardingTool.Infrastructure.SeedData;
using RPGOnboardingTool.Core.Enums;

namespace RPGOnboardingTool.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        //map to Db tables
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Character> Characters { get; set; } = null!;
        public DbSet<Race> Races { get; set; } = null!;
        public DbSet<TrainingPackage> TrainingPackages { get; set; } = null!;
        public DbSet<TrainingPackageSkill> TrainingPackageSkills { get; set; } = null!;
        public DbSet<TrainingPackageStatRequirement> TrainingPackageStatRequirements { get; set; } = null!;
        public DbSet<Stat> Stats { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;
        public DbSet<Trait> Traits { get; set; } = null!;
        public DbSet<CharacterTrait> CharacterTraits { get; set; } = null!;
        public DbSet<EquipmentItem> EquipmentItems { get; set; } = null!;
        public DbSet<CharacterEquipment> CharacterEquipments { get; set; } = null!;
        public DbSet<CharacterGeneralItem> CharacterGeneralItems { get; set; } = null!;
        public DbSet<RaceSkill> RaceSkills { get; set; } = null!;
        public DbSet<RaceStatLimit> RaceStatLimits { get; set; } = null!;
        public DbSet<SkillDefinition> SkillDefinitions { get; set; } = null!;
        public DbSet<GeneralItem> GeneralItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // many-to-many relationships
            base.OnModelCreating(modelBuilder);

            //character & race many-to-many relationships
            modelBuilder.Entity<Character>()
                .HasOne(c => c.CharacterRace)
                .WithMany(r => r.Characters)
                .HasForeignKey(c => c.RaceId)
                .IsRequired(false);//allows charcater creation in stages

            modelBuilder.Entity<Character>()
                .HasOne(c => c.CharacterTrainingPackage)
                .WithMany(tp => tp.Characters)
                .HasForeignKey(c => c.TrainingPackageId)
                .IsRequired(false); // traing pckge can be empty at character creation

            modelBuilder.Entity<Stat>()
                .HasOne(s => s.Character)
                .WithMany(c => c.Stats) //character has many stats
                .HasForeignKey(s => s.CharacterId);

            modelBuilder.Entity<Skill>()
                .HasOne(s => s.Character)
                .WithMany(c => c.Skills) //character has many skills
                .HasForeignKey(s => s.CharacterId);

            modelBuilder.Entity<CharacterTrait>()
                .HasKey(ct => new { ct.CharacterId, ct.TraitId });

            modelBuilder.Entity<CharacterTrait>()
                .HasOne(ct => ct.Character)
                .WithMany(c => c.CharacterTraits)
                .HasForeignKey(ct => ct.CharacterId);

            modelBuilder.Entity<CharacterTrait>()
                .HasOne(ct => ct.Trait)
                .WithMany(t => t.CharacterTraits)
                .HasForeignKey(ct => ct.TraitId);

            // CharacterEquipment: Junction table for Character and EquipmentItem
            modelBuilder.Entity<CharacterEquipment>()
                .HasKey(ce => new { ce.CharacterId, ce.EquipmentItemId }); // Define composite primary key

            modelBuilder.Entity<CharacterEquipment>()
                .HasOne(ce => ce.Character) // CharacterEquipment has one Character
                .WithMany(c => c.CharacterEquipment) // Character has many CharacterEquipment
                .HasForeignKey(ce => ce.CharacterId); // Foreign key in CharacterEquipment table

            modelBuilder.Entity<CharacterEquipment>()
                .HasOne(ce => ce.EquipmentItem) // CharacterEquipment has one EquipmentItem
                .WithMany(ei => ei.CharacterEquipment) // EquipmentItem has many CharacterEquipment
                .HasForeignKey(ce => ce.EquipmentItemId); // Foreign key in CharacterEquipment table

            modelBuilder.Entity<CharacterGeneralItem>()
                .HasKey(cgi => new { cgi.CharacterId, cgi.GeneralItemId });

            modelBuilder.Entity<CharacterGeneralItem>()
                .HasOne(cgi => cgi.Character)
                .WithMany(c => c.CharacterGeneralItems)
                .HasForeignKey(cgi => cgi.CharacterId);

            modelBuilder.Entity<CharacterGeneralItem>()
                .HasOne(cgi => cgi.GeneralItem)
                .WithMany(gi => gi.CharacterGeneralItems)
                .HasForeignKey(cgi => cgi.GeneralItemId);

            // RaceSkill: One-to-many from Race to RaceSkill
            modelBuilder.Entity<RaceSkill>()
                .HasOne(rs => rs.Race) // A RaceSkill belongs to one Race
                .WithMany(r => r.SpeciesSkills) // A Race has many SpeciesSkills
                .HasForeignKey(rs => rs.RaceId); // Foreign key in RaceSkill table

            modelBuilder.Entity<RaceStatLimit>()
                .HasOne(rsl => rsl.Race)
                .WithMany(r => r.StatLimits)
                .HasForeignKey(rsl => rsl.RaceId);

            // --- Training Package-Specific Mappings ---
            // TrainingPackageStatRequirement: One-to-many from TrainingPackage to TrainingPackageStatRequirement
            modelBuilder.Entity<TrainingPackageStatRequirement>()
                .HasOne(tpsr => tpsr.TrainingPackage) // A TrainingPackageStatRequirement belongs to one TrainingPackage
                .WithMany(tp => tp.StatRequirements) // A TrainingPackage has many StatRequirements
                .HasForeignKey(tpsr => tpsr.TrainingPackageId); // Foreign key in TrainingPackageStatRequirement table

            // TrainingPackageSkill: One-to-many from TrainingPackage to TrainingPackageSkill
            modelBuilder.Entity<TrainingPackageSkill>()
                .HasOne(tps => tps.TrainingPackage) // A TrainingPackageSkill belongs to one TrainingPackage
                .WithMany(tp => tp.PackageSkills) // A TrainingPackage has many PackageSkills
                .HasForeignKey(tps => tps.TrainingPackageId); // Foreign key in TrainingPackageSkill table

            modelBuilder.Entity<Character>()
                .Property(c => c.RowVersion)
                .IsRowVersion();

            // --- Seed Data ---
            // Use the StaticGameData class to seed initial game data
            // These methods will provide the initial data for your tables.
            modelBuilder.Entity<Race>().HasData(StaticGameData.GetRaces());
            modelBuilder.Entity<RaceSkill>().HasData(StaticGameData.GetRaceSkills());
            modelBuilder.Entity<RaceStatLimit>().HasData(StaticGameData.GetRaceStatLimits());

            modelBuilder.Entity<TrainingPackage>().HasData(StaticGameData.GetTrainingPackages());
            modelBuilder.Entity<TrainingPackageStatRequirement>().HasData(StaticGameData.GetTrainingPackageStatRequirements());
            modelBuilder.Entity<TrainingPackageSkill>().HasData(StaticGameData.GetTrainingPackageSkills());

            modelBuilder.Entity<Trait>().HasData(StaticGameData.GetTraits());
            modelBuilder.Entity<EquipmentItem>().HasData(StaticGameData.GetEquipmentItems());
            modelBuilder.Entity<SkillDefinition>().HasData(StaticGameData.GetSkillDefinitions());
            modelBuilder.Entity<GeneralItem>().HasData(StaticGameData.GetGeneralItems());
        }
    }
}