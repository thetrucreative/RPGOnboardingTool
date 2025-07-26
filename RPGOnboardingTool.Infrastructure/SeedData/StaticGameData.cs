// RPGOnboardingTool.Infrastructure/SeedData/StaticGameData.cs
using RPGOnboardingTool.Core.Models;
using RPGOnboardingTool.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using RPGOnboardingTool.Core.Models.Items;

namespace RPGOnboardingTool.Infrastructure.SeedData
{
    public static class StaticGameData
    {
        private static readonly List<Race> _races = new()
        {
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445500"), Name = "Advanced Carrien", Description = "Description for Advanced Carrien.", ImageUrl = "/images/races/advanced-carrien.png" },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), Name = "Ebonite [Ebon]", Description = "Description for Ebonite [Ebon].", CanHaveFinanceChip = false, ImageUrl = "/images/races/ebonite-ebon.png" },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), Name = "Ebonite [Waister]", Description = "Description for Ebonite [Waister].", CanHaveFinanceChip = false, ImageUrl = "/images/races/ebonite-waister.png" },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), Name = "Frother", Description = "Description for Frother.", ImageUrl = "/images/races/frother.png" },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), Name = "Human", Description = "Balanced and adaptable.", ImageUrl = "/images/races/human.png" },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), Name = "Neophron", Description = "Intelligent and diplomatic.", ImageUrl = "/images/races/neophron.png" },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), Name = "Shaktar", Description = "Resilient and cunning.", ImageUrl = "/images/races/shaktar.png" },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), Name = "Stormer 313 'Malice'", Description = "Brutal and intimidating.", ImageUrl = "/images/races/stormer-313-malice.png" },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), Name = "Stormer 711 'Xeno'", Description = "Stealthy and agile.", ImageUrl = "/images/races/stormer-711-xeno.png" },
            new Race { Id = new Guid("a1b2c3d4-e5f6-4000-8000-112233445509"), Name = "Wraithen", Description = "Observant and tracking.", ImageUrl = "/images/races/wraithen.png" }
        };

        private static readonly List<TrainingPackage> _trainingPackages = new()
        {
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445501"), Name = "Bureaucrat", Description = "A training package for bureaucrats." },
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445502"), Name = "Close Assault", Description = "A training package for close assault." },
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445503"), Name = "Heavy Support", Description = "A training package for heavy support." },
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445504"), Name = "Investigation & Interrogation", Description = "A training package for investigation and interrogation." },
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445505"), Name = "Medic", Description = "A training package for medics." },
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445506"), Name = "Scout", Description = "A training package for scouts." },
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445507"), Name = "Strike & Sweep", Description = "A training package for strike and sweep operations." },
            new TrainingPackage { Id = new Guid("b1b2c3d4-e5f6-4000-8000-112233445508"), Name = "Technical", Description = "A training package for technical experts." }
        };

        public static List<Race> GetRaces() => _races;

        public static List<TrainingPackage> GetTrainingPackages() => _trainingPackages;

        public static List<RaceSkill> GetRaceSkills()
        {
            return new List<RaceSkill>
            {
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Advanced Carrien").Id, SkillName = "Intimidate", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Advanced Carrien").Id, SkillName = "Language: Gristle", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Advanced Carrien").Id, SkillName = "Lore: Sector", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Advanced Carrien").Id, SkillName = "Survival", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Advanced Carrien").Id, SkillName = "Unarmed Combat", Rank = 1 },

                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Ebon]").Id, SkillName = "Detect", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Ebon]").Id, SkillName = "Education: Academic", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Ebon]").Id, SkillName = "EBB - Heal", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Ebon]").Id, SkillName = "Protect", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Ebon]").Id, SkillName = "Thermal: Blue", Rank = 1 },

                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Waister]").Id, SkillName = "Detect", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Waister]").Id, SkillName = "Education: Academic", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Waister]").Id, SkillName = "EBB - Blast", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Waister]").Id, SkillName = "Protect", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Waister]").Id, SkillName = "Thermal: Red", Rank = 1 },

                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Frother").Id, SkillName = "Detect", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Frother").Id, SkillName = "Melee Weapons", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Frother").Id, SkillName = "Streetwise", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Frother").Id, SkillName = "Unarmed Combat", Rank = 1 },

                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, SkillName = "Detect", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, SkillName = "Education: Academic", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, SkillName = "Streetwise", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, SkillName = "Unarmed Combat", Rank = 1 },

                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, SkillName = "Education: Academic", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, SkillName = "Education: Natural", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, SkillName = "Bribery", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, SkillName = "Diplomacy", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, SkillName = "Language: Neophron", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, SkillName = "Language +1", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, SkillName = "Leadership", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, SkillName = "Oratory", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, SkillName = "Persuasion", Rank = 1 },

                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, SkillName = "Climbing", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, SkillName = "Detect", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, SkillName = "Language: Shaktar", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, SkillName = "Survival", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, SkillName = "Unarmed Combat", Rank = 1 },

                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 'Malice'").Id, SkillName = "Athletics", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 'Malice'").Id, SkillName = "Intimidate", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 'Malice'").Id, SkillName = "Throw", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 'Malice'").Id, SkillName = "Unarmed Combat", Rank = 1 },

                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 'Xeno'").Id, SkillName = "Climbing", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 'Xeno'").Id, SkillName = "Stealth", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 'Xeno'").Id, SkillName = "Unarmed Combat", Rank = 1 },

                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, SkillName = "Athletics", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, SkillName = "Detect", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, SkillName = "Language: Wraithen", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, SkillName = "Unarmed Combat", Rank = 1 },
                new RaceSkill { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, SkillName = "Tracking", Rank = 1 }
            };
        }
        public static List<RaceStatLimit> GetRaceStatLimits()
        {
            return new List<RaceStatLimit>
            {
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Advanced Carrien").Id, StatType = StatType.Strength, MinValue = 3, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Advanced Carrien").Id, StatType = StatType.Dexterity, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Advanced Carrien").Id, StatType = StatType.Knowledge, MinValue = 0, MaxValue = 2 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Advanced Carrien").Id, StatType = StatType.Concentration, MinValue = 1, MaxValue = 4 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Advanced Carrien").Id, StatType = StatType.Charisma, MinValue = 0, MaxValue = 3 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Advanced Carrien").Id, StatType = StatType.Cool, MinValue = 3, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Advanced Carrien").Id, StatType = StatType.Luck, MinValue = 0, MaxValue = 3 },

                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Ebon]").Id, StatType = StatType.Strength, MinValue = 0, MaxValue = 3 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Ebon]").Id, StatType = StatType.Dexterity, MinValue = 1, MaxValue = 4 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Ebon]").Id, StatType = StatType.Knowledge, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Ebon]").Id, StatType = StatType.Concentration, MinValue = 2, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Ebon]").Id, StatType = StatType.Charisma, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Ebon]").Id, StatType = StatType.Cool, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Ebon]").Id, StatType = StatType.Luck, MinValue = 2, MaxValue = 6 },

                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Waister]").Id, StatType = StatType.Strength, MinValue = 0, MaxValue = 3 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Waister]").Id, StatType = StatType.Dexterity, MinValue = 1, MaxValue = 4 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Waister]").Id, StatType = StatType.Knowledge, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Waister]").Id, StatType = StatType.Concentration, MinValue = 2, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Waister]").Id, StatType = StatType.Charisma, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Waister]").Id, StatType = StatType.Cool, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Ebonite [Waister]").Id, StatType = StatType.Luck, MinValue = 2, MaxValue = 6 },

                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Frother").Id, StatType = StatType.Strength, MinValue = 2, MaxValue = 4 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Frother").Id, StatType = StatType.Dexterity, MinValue = 2, MaxValue = 4 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Frother").Id, StatType = StatType.Knowledge, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Frother").Id, StatType = StatType.Concentration, MinValue = 1, MaxValue = 3 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Frother").Id, StatType = StatType.Charisma, MinValue = 0, MaxValue = 4 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Frother").Id, StatType = StatType.Cool, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Frother").Id, StatType = StatType.Luck, MinValue = 1, MaxValue = 3 },

                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, StatType = StatType.Strength, MinValue = 1, MaxValue = 3 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, StatType = StatType.Dexterity, MinValue = 1, MaxValue = 4 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, StatType = StatType.Knowledge, MinValue = 2, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, StatType = StatType.Concentration, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, StatType = StatType.Charisma, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, StatType = StatType.Cool, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Human").Id, StatType = StatType.Luck, MinValue = 1, MaxValue = 6 },

                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, StatType = StatType.Strength, MinValue = 0, MaxValue = 2 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, StatType = StatType.Dexterity, MinValue = 0, MaxValue = 3 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, StatType = StatType.Knowledge, MinValue = 2, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, StatType = StatType.Concentration, MinValue = 2, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, StatType = StatType.Charisma, MinValue = 3, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, StatType = StatType.Cool, MinValue = 1, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Neophron").Id, StatType = StatType.Luck, MinValue = 0, MaxValue = 3 },

                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, StatType = StatType.Strength, MinValue = 3, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, StatType = StatType.Dexterity, MinValue = 2, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, StatType = StatType.Knowledge, MinValue = 1, MaxValue = 4 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, StatType = StatType.Concentration, MinValue = 0, MaxValue = 3 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, StatType = StatType.Charisma, MinValue = 1, MaxValue = 3 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, StatType = StatType.Cool, MinValue = 1, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Shaktar").Id, StatType = StatType.Luck, MinValue = 0, MaxValue = 3 },

                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 'Malice'").Id, StatType = StatType.Strength, MinValue = 3, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 'Malice'").Id, StatType = StatType.Dexterity, MinValue = 2, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 'Malice'").Id, StatType = StatType.Knowledge, MinValue = 0, MaxValue = 2 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 'Malice'").Id, StatType = StatType.Concentration, MinValue = 0, MaxValue = 3 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 'Malice'").Id, StatType = StatType.Charisma, MinValue = 0, MaxValue = 3 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 'Malice'").Id, StatType = StatType.Cool, MinValue = 3, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 313 'Malice'").Id, StatType = StatType.Luck, MinValue = 0, MaxValue = 2 },

                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 'Xeno'").Id, StatType = StatType.Strength, MinValue = 2, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 'Xeno'").Id, StatType = StatType.Dexterity, MinValue = 3, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 'Xeno'").Id, StatType = StatType.Knowledge, MinValue = 0, MaxValue = 3 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 'Xeno'").Id, StatType = StatType.Concentration, MinValue = 1, MaxValue = 4 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 'Xeno'").Id, StatType = StatType.Charisma, MinValue = 0, MaxValue = 2 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 'Xeno'").Id, StatType = StatType.Cool, MinValue = 2, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Stormer 711 'Xeno'").Id, StatType = StatType.Luck, MinValue = 0, MaxValue = 2 },

                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, StatType = StatType.Strength, MinValue = 1, MaxValue = 3 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, StatType = StatType.Dexterity, MinValue = 3, MaxValue = 6 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, StatType = StatType.Knowledge, MinValue = 1, MaxValue = 4 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, StatType = StatType.Concentration, MinValue = 1, MaxValue = 4 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, StatType = StatType.Charisma, MinValue = 1, MaxValue = 4 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, StatType = StatType.Cool, MinValue = 0, MaxValue = 5 },
                new RaceStatLimit { Id = Guid.NewGuid(), RaceId = _races.First(r => r.Name == "Wraithen").Id, StatType = StatType.Luck, MinValue = 1, MaxValue = 4 }
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
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Bureaucrat").Id, SkillName = "Admin & Finance", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Bureaucrat").Id, SkillName = "Computer", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Bureaucrat").Id, SkillName = "Diplomacy", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Bureaucrat").Id, SkillName = "Haggle", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Bureaucrat").Id, SkillName = "Leadership", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Bureaucrat").Id, SkillName = "Oratory", Rank = 1 },

                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Close Assault").Id, SkillName = "Acrobatics", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Close Assault").Id, SkillName = "Athletics", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Close Assault").Id, SkillName = "Melee/Polearm/Throw or Unarmed", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Close Assault").Id, SkillName = "Melee/Polearm/Throw or Unarmed", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Close Assault").Id, SkillName = "Climbing", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Close Assault").Id, SkillName = "Stealth", Rank = 1 },

                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Heavy Support").Id, SkillName = "Demolitions", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Heavy Support").Id, SkillName = "Rifle", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Heavy Support").Id, SkillName = "Support Weapons", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Heavy Support").Id, SkillName = "Tactics", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Heavy Support").Id, SkillName = "Throw", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Heavy Support").Id, SkillName = "Technical: Weapons", Rank = 1 },

                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Investigation & Interrogation").Id, SkillName = "Computer", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Investigation & Interrogation").Id, SkillName = "Detect", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Investigation & Interrogation").Id, SkillName = "Forensics", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Investigation & Interrogation").Id, SkillName = "Interrgate", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Investigation & Interrogation").Id, SkillName = "Streetwise", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Investigation & Interrogation").Id, SkillName = "Torture", Rank = 1 },

                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Medic").Id, SkillName = "Computer", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Medic").Id, SkillName = "Detect", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Medic").Id, SkillName = "Education: Academic", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Medic").Id, SkillName = "Education: Natural", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Medic").Id, SkillName = "Forensics", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Medic").Id, SkillName = "Medical", Rank = 2 },

                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Scout").Id, SkillName = "Detect", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Scout").Id, SkillName = "Read Lips", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Scout").Id, SkillName = "Rifle", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Scout").Id, SkillName = "Stealth", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Scout").Id, SkillName = "Survival", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Scout").Id, SkillName = "Tracking", Rank = 2 },

                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Strike & Sweep").Id, SkillName = "Athletics", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Strike & Sweep").Id, SkillName = "Detect", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Strike & Sweep").Id, SkillName = "Drive Civilian", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Strike & Sweep").Id, SkillName = "Drive Military", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Strike & Sweep").Id, SkillName = "Melee or Unarmed", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Strike & Sweep").Id, SkillName = "Medical", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Strike & Sweep").Id, SkillName = "Pistol", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Strike & Sweep").Id, SkillName = "Rifle", Rank = 1 },

                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Technical").Id, SkillName = "Computer", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Technical").Id, SkillName = "Detect", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Technical").Id, SkillName = "Haggle", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Technical").Id, SkillName = "Lockpick (Pick One)", Rank = 1 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Technical").Id, SkillName = "Technical (Pick One)", Rank = 2 },
                new TrainingPackageSkill { Id = Guid.NewGuid(), TrainingPackageId = _trainingPackages.First(tp => tp.Name == "Technical").Id, SkillName = "Technical (Pick One)", Rank = 1 },
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
            const string equipmentImageBasePath = "/images/equipment";
            
            return new List<EquipmentItem>
            {
                new EquipmentItem { Id = Guid.NewGuid(), Name = "Standard Pistol", Description = "A common semi-automatic pistol, valued for its reliability and ease of use.", Cost = 200, Weight = 2, ImageUrl = $"{equipmentImageBasePath}/standard-pistol.png" },
                new EquipmentItem { Id = Guid.NewGuid(), Name = "Combat Knife", Description = "A basic but effective single-edged combat knife.", Cost = 50, Weight = 1, ImageUrl = $"{equipmentImageBasePath}/combat-knife.png" },
                new EquipmentItem { Id = Guid.NewGuid(), Name = "Light Body Armor", Description = "Standard-issue body armor, offering basic protection without hindering mobility.", Cost = 500, Weight = 10, ImageUrl = $"{equipmentImageBasePath}/light-body-armor.png" },
                new EquipmentItem { Id = Guid.NewGuid(), Name = "Medkit", Description = "A compact kit with essentials for treating common injuries.", Cost = 100, Weight = 2, ImageUrl = $"{equipmentImageBasePath}/medkit.png" },
                new EquipmentItem { Id = Guid.NewGuid(), Name = "Heavy Rifle", Description = "A high-caliber rifle for when you absolutely need to make a statement.", Cost = 1000, Weight = 15, ImageUrl = $"{equipmentImageBasePath}/heavy-rifle.png" },
            };
        }

        public static List<SkillDefinition> GetSkillDefinitions()
        {
            return new List<SkillDefinition>
            {
                new SkillDefinition { Name = "CLIMBING", RelatedStat = StatType.Strength },
                new SkillDefinition { Name = "MELEE WEAPONS", RelatedStat = StatType.Strength },
                new SkillDefinition { Name = "POLEARM", RelatedStat = StatType.Strength },
                new SkillDefinition { Name = "SHIELD CRAFT", RelatedStat = StatType.Strength },
                new SkillDefinition { Name = "SUPPORT WEAPONS", RelatedStat = StatType.Strength },
                new SkillDefinition { Name = "SWIMMING", RelatedStat = StatType.Strength },
                new SkillDefinition { Name = "THROW", RelatedStat = StatType.Strength },
                new SkillDefinition { Name = "UNARMED COMBAT", RelatedStat = StatType.Strength },

                new SkillDefinition { Name = "ACROBATICS", RelatedStat = StatType.Dexterity },
                new SkillDefinition { Name = "ATHLETICS", RelatedStat = StatType.Dexterity },
                new SkillDefinition { Name = "DRIVE [MOTORCYCLE]", RelatedStat = StatType.Dexterity },
                new SkillDefinition { Name = "FORGERY", RelatedStat = StatType.Dexterity },
                new SkillDefinition { Name = "PISTOL", RelatedStat = StatType.Dexterity },
                new SkillDefinition { Name = "RIFLE", RelatedStat = StatType.Dexterity },
                new SkillDefinition { Name = "SLEIGHT", RelatedStat = StatType.Dexterity },
                new SkillDefinition { Name = "STEALTH", RelatedStat = StatType.Dexterity },

                new SkillDefinition { Name = "ADMIN & FINANCE", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "COMPUTER", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "EDUCATION [ACADEMIC]", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "EDUCATION [NATURAL]", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "FORENSICS", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "LANGUAGE [WRAITHEN]", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "LANGUAGE [SHAKTARIAN]", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "LANGUAGE [NEOPHRON]", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "LANGUAGE [SIGN]", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "LANGUAGE [GRISTLE]", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "LANGUAGE [BIYA]", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "LORE [CULT]", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "LORE [DREAM]", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "LORE [SECTOR]", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "MEDICAL", RelatedStat = StatType.Knowledge },
                new SkillDefinition { Name = "STREETWISE", RelatedStat = StatType.Knowledge },

                new SkillDefinition { Name = "DEMOLITIONS", RelatedStat = StatType.Concentration },
                new SkillDefinition { Name = "DRIVE [CIVILIAN]", RelatedStat = StatType.Concentration },
                new SkillDefinition { Name = "DRIVE [MILITARY]", RelatedStat = StatType.Concentration },
                new SkillDefinition { Name = "DRIVE [PILOT]", RelatedStat = StatType.Concentration },
                new SkillDefinition { Name = "DETECT", RelatedStat = StatType.Concentration },
                new SkillDefinition { Name = "LOCK PICK [MANUAL]", RelatedStat = StatType.Concentration },
                new SkillDefinition { Name = "LOCK PICK [ELECTRONIC]", RelatedStat = StatType.Concentration },
                new SkillDefinition { Name = "READ LIPS", RelatedStat = StatType.Concentration },
                new SkillDefinition { Name = "TACTICS", RelatedStat = StatType.Concentration },
                new SkillDefinition { Name = "TECHNICAL [ELECTRICAL]", RelatedStat = StatType.Concentration },
                new SkillDefinition { Name = "TECHNICAL [MECHANICAL]", RelatedStat = StatType.Concentration },
                new SkillDefinition { Name = "TECHNICAL [WEAPONS]", RelatedStat = StatType.Concentration },
                new SkillDefinition { Name = "TRACKING", RelatedStat = StatType.Concentration },

                new SkillDefinition { Name = "DIPLOMACY", RelatedStat = StatType.Charisma },
                new SkillDefinition { Name = "HAGGLE", RelatedStat = StatType.Charisma },
                new SkillDefinition { Name = "LEADERSHIP", RelatedStat = StatType.Charisma },
                new SkillDefinition { Name = "ORATORY", RelatedStat = StatType.Charisma },
                new SkillDefinition { Name = "PERSUASION", RelatedStat = StatType.Charisma },
                new SkillDefinition { Name = "SEDUCTION", RelatedStat = StatType.Charisma },

                new SkillDefinition { Name = "BRIBERY", RelatedStat = StatType.Cool },
                new SkillDefinition { Name = "GAMBLING", RelatedStat = StatType.Cool },
                new SkillDefinition { Name = "INTERROGATE", RelatedStat = StatType.Cool },
                new SkillDefinition { Name = "INTIMIDATE", RelatedStat = StatType.Cool },
                new SkillDefinition { Name = "SURVIVAL", RelatedStat = StatType.Cool },
                new SkillDefinition { Name = "TORTURE", RelatedStat = StatType.Cool },
            };
        }

        public static List<GeneralItem> GetGeneralItems()
        {
            return new List<GeneralItem>
            {
                new GeneralItem { Id = Guid.NewGuid(), Name = "Medkit", Cost = 100, Weight = 2, Effect = "Heals 10 HP" },
                new GeneralItem { Id = Guid.NewGuid(), Name = "Stimpack", Cost = 50, Weight = 1, Effect = "Grants +1 to all stats for 1 minute" }
            };
        }
    }
}