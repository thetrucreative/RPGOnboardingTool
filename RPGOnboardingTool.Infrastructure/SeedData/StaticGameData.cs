// RPGOnboardingTool.Infrastructure/SeedData/StaticGameData.cs
using RPGOnboardingTool.Core.Models;
using RPGOnboardingTool.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGOnboardingTool.Infrastructure.SeedData
{
    public static class StaticGameData
    {
        private static readonly List<Race> _races = new()
        {
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), Name = "Human", Description = "Balanced and adaptable.", BaseInitiative = 10, BaseMovement = 6, MaxHp = 50, MaxLuck = 3, CanHaveFinanceChip = true },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), Name = "Ebonite (Blue)", Description = "Stalwart and protective.", BaseInitiative = 8, BaseMovement = 5, MaxHp = 60, MaxLuck = 2, CanHaveFinanceChip = false },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), Name = "Ebonite (Red Frother)", Description = "Aggressive and powerful.", BaseInitiative = 7, BaseMovement = 5, MaxHp = 70, MaxLuck = 1, CanHaveFinanceChip = false },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), Name = "Neophron", Description = "Intelligent and diplomatic.", BaseInitiative = 9, BaseMovement = 7, MaxHp = 45, MaxLuck = 4, CanHaveFinanceChip = true },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), Name = "Shaktar", Description = "Resilient and cunning.", BaseInitiative = 9, BaseMovement = 6, MaxHp = 55, MaxLuck = 3, CanHaveFinanceChip = true },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), Name = "Stormer 313 (Malice)", Description = "Brutal and intimidating.", BaseInitiative = 11, BaseMovement = 7, MaxHp = 65, MaxLuck = 2, CanHaveFinanceChip = true },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), Name = "Stormer 711 (Xeno)", Description = "Stealthy and agile.", BaseInitiative = 12, BaseMovement = 8, MaxHp = 50, MaxLuck = 3, CanHaveFinanceChip = true },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), Name = "Wraithen", Description = "Observant and tracking.", BaseInitiative = 10, BaseMovement = 6, MaxHp = 50, MaxLuck = 3, CanHaveFinanceChip = true }
        };

        private static readonly List<TrainingPackage> _trainingPackages = new()
        {
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445501"), Name = "Human Training Package", Description = "A balanced training package for humans." },
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445502"), Name = "Ebonite (Blue) Training Package", Description = "A training package focused on protection and support for Blue Ebonites." },
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445503"), Name = "Ebonite (Red Frother) Training Package", Description = "A training package focused on aggression for Red Frother Ebonites." },
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445504"), Name = "Neophron Training Package", Description = "A training package for Neophron diplomats and intellectuals." },
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445505"), Name = "Shaktar Training Package", Description = "A training package for the cunning and resilient Shaktar." },
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445506"), Name = "Stormer 313 (Malice) Training Package", Description = "A brutal training package for the Stormer 313." },
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445507"), Name = "Stormer 711 (Xeno) Training Package", Description = "A stealth-focused training package for the Stormer 711." },
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445508"), Name = "Wraithen Training Package", Description = "A training package for the observant Wraithen." }
        };

        public static List<Race> GetRaces() => _races;

        public static List<TrainingPackage> GetTrainingPackages() => _trainingPackages;

        public static List<RaceStatLimit> GetRaceStatLimits()
        {
            return new List<RaceStatLimit>
            {
                // Human
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, StatType = StatType.Strength, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, StatType = StatType.Dexterity, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, StatType = StatType.Knowledge, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, StatType = StatType.Coordination, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, StatType = StatType.Cool, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, StatType = StatType.Composure, MinValue = 1, MaxValue = 6 },
                // Ebonite (Blue)
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Blue)").Id, StatType = StatType.Strength, MinValue = 2, MaxValue = 7 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Blue)").Id, StatType = StatType.Dexterity, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Blue)").Id, StatType = StatType.Knowledge, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Blue)").Id, StatType = StatType.Coordination, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Blue)").Id, StatType = StatType.Cool, MinValue = 2, MaxValue = 7 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Blue)").Id, StatType = StatType.Composure, MinValue = 2, MaxValue = 7 },
                // Ebonite (Red Frother)
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Red Frother)").Id, StatType = StatType.Strength, MinValue = 3, MaxValue = 8 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Red Frother)").Id, StatType = StatType.Dexterity, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Red Frother)").Id, StatType = StatType.Knowledge, MinValue = 1, MaxValue = 4 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Red Frother)").Id, StatType = StatType.Coordination, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Red Frother)").Id, StatType = StatType.Cool, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Red Frother)").Id, StatType = StatType.Composure, MinValue = 2, MaxValue = 7 },
                // Neophron
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, StatType = StatType.Strength, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, StatType = StatType.Dexterity, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, StatType = StatType.Knowledge, MinValue = 2, MaxValue = 7 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, StatType = StatType.Coordination, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, StatType = StatType.Cool, MinValue = 2, MaxValue = 7 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, StatType = StatType.Composure, MinValue = 1, MaxValue = 6 },
                // Shaktar
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, StatType = StatType.Strength, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, StatType = StatType.Dexterity, MinValue = 2, MaxValue = 7 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, StatType = StatType.Knowledge, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, StatType = StatType.Coordination, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, StatType = StatType.Cool, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, StatType = StatType.Composure, MinValue = 2, MaxValue = 7 },
                 // Stormer 313 (Malice)
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 (Malice)").Id, StatType = StatType.Strength, MinValue = 2, MaxValue = 7 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 (Malice)").Id, StatType = StatType.Dexterity, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 (Malice)").Id, StatType = StatType.Knowledge, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 (Malice)").Id, StatType = StatType.Coordination, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 (Malice)").Id, StatType = StatType.Cool, MinValue = 2, MaxValue = 7 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 (Malice)").Id, StatType = StatType.Composure, MinValue = 1, MaxValue = 6 },
                // Stormer 711 (Xeno)
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 (Xeno)").Id, StatType = StatType.Strength, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 (Xeno)").Id, StatType = StatType.Dexterity, MinValue = 2, MaxValue = 8 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 (Xeno)").Id, StatType = StatType.Knowledge, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 (Xeno)").Id, StatType = StatType.Coordination, MinValue = 2, MaxValue = 7 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 (Xeno)").Id, StatType = StatType.Cool, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 (Xeno)").Id, StatType = StatType.Composure, MinValue = 1, MaxValue = 6 },
                // Wraithen
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, StatType = StatType.Strength, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, StatType = StatType.Dexterity, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, StatType = StatType.Knowledge, MinValue = 1, MaxValue = 7 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, StatType = StatType.Coordination, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, StatType = StatType.Cool, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, StatType = StatType.Composure, MinValue = 2, MaxValue = 7 }
            };
        }

        public static List<RaceSkill> GetRaceSkills()
        {
            return new List<RaceSkill>
            {
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Blue)").Id, SkillName = "Detect", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Blue)").Id, SkillName = "Education: Academic", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Blue)").Id, SkillName = "EBB - Heal", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Blue)").Id, SkillName = "Protect", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite (Blue)").Id, SkillName = "Thermal: Blue Ebonite", Rank = 1 },
            };
        }
        public static List<TrainingPackageStatRequirement> GetTrainingPackageStatRequirements()
        {
            // Implementation for seeding TrainingPackageStatRequirement data  
            return new List<TrainingPackageStatRequirement>();
        }

        public static List<TrainingPackageSkill> GetTrainingPackageSkills()
        {
            // Implementation for seeding TrainingPackageSkills data  
            return new List<TrainingPackageSkill>
            {
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Human Training Package").Id, SkillName = "Athletics", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Ebonite (Blue) Training Package").Id, SkillName = "Protect", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Ebonite (Red Frother) Training Package").Id, SkillName = "Intimidation", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Neophron Training Package").Id, SkillName = "Persuasion", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Shaktar Training Package").Id, SkillName = "Stealth", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Stormer 313 (Malice) Training Package").Id, SkillName = "Athletics", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Stormer 711 (Xeno) Training Package").Id, SkillName = "Acrobatics", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Wraithen Training Package").Id, SkillName = "Insight", Rank = 2 },
            };
        }

        public static List<Trait> GetTraits()
        {
            return new List<Trait>
            {
                new Trait { Id = Guid.NewGuid(), Name = "Bravery", Description = "Increases resistance to fear effects.", BasePointCost = -5, Type = TraitType.Advantage },
                new Trait { Id = Guid.NewGuid(), Name = "Cursed", Description = "A negative trait that imposes penalties.", BasePointCost = 10, Type = TraitType.Disadvantage }
            };
        }

        public static List<EquipmentItem> GetEquipmentItems()
        {
            return new List<EquipmentItem>
            {
                new EquipmentItem { Id = Guid.NewGuid(), Name = "Standard Pistol", Cost = 200, Weight = 2 },
                new EquipmentItem { Id = Guid.NewGuid(), Name = "Combat Knife", Cost = 50, Weight = 1 },
                new EquipmentItem { Id = Guid.NewGuid(), Name = "Light Body Armor", Cost = 500, Weight = 10 },
                new EquipmentItem { Id = Guid.NewGuid(), Name = "Medkit", Cost = 100, Weight = 2 },
                new EquipmentItem { Id = Guid.NewGuid(), Name = "Heavy Rifle", Cost = 1000, Weight = 15 },
            };
        }

        public static List<SkillDefinition> GetSkillDefinitions()
        {
            return new List<SkillDefinition>
            {
                new SkillDefinition { Name = "Athletics", RelatedStat = StatType.Strength },
                new SkillDefinition { Name = "Acrobatics", RelatedStat = StatType.Dexterity },
                new SkillDefinition { Name = "Stealth", RelatedStat = StatType.Dexterity },
                new SkillDefinition { Name = "History", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "Persuasion", RelatedStat = StatType.Cool },
                new SkillDefinition { Name = "Intimidation", RelatedStat = StatType.Cool },
                new SkillDefinition { Name = "Deception", RelatedStat = StatType.Cool },
                new SkillDefinition { Name = "Insight", RelatedStat = StatType.Composure },
                new SkillDefinition { Name = "Survival", RelatedStat = StatType.Composure },
                new SkillDefinition { Name = "Detect", RelatedStat = StatType.Composure },
                new SkillDefinition { Name = "Education: Academic", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "EBB - Heal", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "Protect", RelatedStat = StatType.Strength },
                new SkillDefinition { Name = "Thermal: Blue Ebonite", RelatedStat = StatType.Constitution },
            };
        }
    }
}