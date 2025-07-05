using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RPGOnboardingTool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    WeightFactor = table.Column<int>(type: "int", nullable: false),
                    IsStartingGear = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseInitiative = table.Column<int>(type: "int", nullable: false),
                    BaseMovement = table.Column<int>(type: "int", nullable: false),
                    MaxHp = table.Column<int>(type: "int", nullable: false),
                    MaxLuck = table.Column<int>(type: "int", nullable: false),
                    BaseClosingSpeed = table.Column<int>(type: "int", nullable: false),
                    BaseRushingSpeed = table.Column<int>(type: "int", nullable: false),
                    BaseEncumbrance = table.Column<int>(type: "int", nullable: false),
                    CanHaveFinanceChip = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedStat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Traits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    BasePointCost = table.Column<int>(type: "int", nullable: false),
                    IsUnique = table.Column<bool>(type: "bit", nullable: false),
                    MaxRankAtCreation = table.Column<int>(type: "int", nullable: false),
                    RequiresDetail = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RaceSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceSkills_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceStatLimit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatType = table.Column<int>(type: "int", nullable: false),
                    MinValue = table.Column<int>(type: "int", nullable: false),
                    MaxValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceStatLimit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceStatLimit_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TrainingPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatPointsRemaining = table.Column<int>(type: "int", nullable: false),
                    SkillPointsRemaining = table.Column<int>(type: "int", nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    Unis = table.Column<int>(type: "int", nullable: false),
                    SCL = table.Column<int>(type: "int", nullable: false),
                    MaxHitPoints = table.Column<int>(type: "int", nullable: false),
                    HitPoints = table.Column<int>(type: "int", nullable: false),
                    Closing = table.Column<int>(type: "int", nullable: false),
                    Rushing = table.Column<int>(type: "int", nullable: false),
                    Movement = table.Column<int>(type: "int", nullable: false),
                    EncumbranceValue = table.Column<int>(type: "int", nullable: false),
                    CurrentWeightCarried = table.Column<int>(type: "int", nullable: false),
                    HasFinanceChip = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characters_TrainingPackages_TrainingPackageId",
                        column: x => x.TrainingPackageId,
                        principalTable: "TrainingPackages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrainingPackageSkill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainingPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPackageSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPackageSkill_TrainingPackages_TrainingPackageId",
                        column: x => x.TrainingPackageId,
                        principalTable: "TrainingPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPackageStatRequirement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainingPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatType = table.Column<int>(type: "int", nullable: false),
                    MinValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPackageStatRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPackageStatRequirement_TrainingPackages_TrainingPackageId",
                        column: x => x.TrainingPackageId,
                        principalTable: "TrainingPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterEquipments",
                columns: table => new
                {
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEquipments", x => new { x.CharacterId, x.EquipmentItemId });
                    table.ForeignKey(
                        name: "FK_CharacterEquipments_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterEquipments_EquipmentItems_EquipmentItemId",
                        column: x => x.EquipmentItemId,
                        principalTable: "EquipmentItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterTraits",
                columns: table => new
                {
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TraitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterTraits", x => new { x.CharacterId, x.TraitId });
                    table.ForeignKey(
                        name: "FK_CharacterTraits_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterTraits_Traits_TraitId",
                        column: x => x.TraitId,
                        principalTable: "Traits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Bonus = table.Column<int>(type: "int", nullable: false),
                    SuccessDie = table.Column<int>(type: "int", nullable: false),
                    SkillDice = table.Column<int>(type: "int", nullable: false),
                    RelatedStat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stats_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "Id", "BaseClosingSpeed", "BaseEncumbrance", "BaseInitiative", "BaseMovement", "BaseRushingSpeed", "CanHaveFinanceChip", "Description", "MaxHp", "MaxLuck", "Name" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 0, 0, 10, 6, 0, true, "Balanced and adaptable.", 50, 3, "Human" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 0, 0, 8, 5, 0, false, "Stalwart and protective.", 60, 2, "Ebonite (Blue)" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 0, 0, 7, 5, 0, false, "Aggressive and powerful.", 70, 1, "Ebonite (Red Frother)" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 0, 0, 9, 7, 0, true, "Intelligent and diplomatic.", 45, 4, "Neophron" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 0, 0, 9, 6, 0, true, "Resilient and cunning.", 55, 3, "Shaktar" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 0, 0, 11, 7, 0, true, "Brutal and intimidating.", 65, 2, "Stormer 313 (Malice)" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 0, 0, 12, 8, 0, true, "Stealthy and agile.", 50, 3, "Stormer 711 (Xeno)" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 0, 0, 10, 6, 0, true, "Observant and tracking.", 50, 3, "Wraithen" }
                });

            migrationBuilder.InsertData(
                table: "SkillDefinitions",
                columns: new[] { "Id", "Name", "RelatedStat" },
                values: new object[,]
                {
                    { new Guid("0b5cd697-9fee-4056-abba-31d17a4626ec"), "Intimidation", 8 },
                    { new Guid("58fb471d-ec02-493e-a336-dfe6e22a798e"), "History", 6 },
                    { new Guid("8ace087f-9b8e-414b-be06-2c6ce789f9dc"), "Stealth", 1 },
                    { new Guid("9d021f3b-0f9c-4b86-953a-069b437f587e"), "Insight", 9 },
                    { new Guid("a2e33479-dccd-4cd1-8a61-8fbcd0ca952a"), "Acrobatics", 1 },
                    { new Guid("c0e5ecef-057d-4970-9a0c-03906e5dda1e"), "Deception", 8 },
                    { new Guid("c9b8b964-0633-4c10-b489-648958340ad0"), "Persuasion", 8 },
                    { new Guid("e6931104-be8d-4919-b5b7-b0c6afa62270"), "Athletics", 0 },
                    { new Guid("fb771c80-279f-4185-8dec-28adf138204b"), "Survival", 9 }
                });

            migrationBuilder.InsertData(
                table: "TrainingPackages",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445501"), "A balanced training package for humans.", "Human Training Package" },
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445502"), "A training package focused on protection and support for Blue Ebonites.", "Ebonite (Blue) Training Package" },
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445503"), "A training package focused on aggression for Red Frother Ebonites.", "Ebonite (Red Frother) Training Package" },
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445504"), "A training package for Neophron diplomats and intellectuals.", "Neophron Training Package" },
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445505"), "A training package for the cunning and resilient Shaktar.", "Shaktar Training Package" },
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445506"), "A brutal training package for the Stormer 313.", "Stormer 313 (Malice) Training Package" },
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445507"), "A stealth-focused training package for the Stormer 711.", "Stormer 711 (Xeno) Training Package" },
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445508"), "A training package for the observant Wraithen.", "Wraithen Training Package" }
                });

            migrationBuilder.InsertData(
                table: "Traits",
                columns: new[] { "Id", "BasePointCost", "Description", "IsUnique", "MaxRankAtCreation", "Name", "RequiresDetail", "Type" },
                values: new object[,]
                {
                    { new Guid("087dc64a-cb3f-4138-8942-2dfa527ecd84"), 0, "A negative trait that imposes penalties.", false, 1, "Cursed", false, 0 },
                    { new Guid("d1bed305-1bc0-4c80-a7db-87658df58779"), 0, "Increases resistance to fear effects.", false, 1, "Bravery", false, 0 }
                });

            migrationBuilder.InsertData(
                table: "RaceSkills",
                columns: new[] { "Id", "RaceId", "Rank", "SkillName" },
                values: new object[,]
                {
                    { new Guid("320988c2-cebc-442a-851f-fc124897dfcc"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "Education: Academic" },
                    { new Guid("648580cd-9609-4406-a404-e3bce0414de4"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "EBB - Heal" },
                    { new Guid("a5df3861-f2f2-4667-b12a-376c4ffce138"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "Detect" },
                    { new Guid("b3dec809-7143-48cc-9b08-2b39b7dcd1f2"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "Protect" },
                    { new Guid("d1010e1f-ac91-45c5-879b-a54d9d53b868"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "Thermal: Blue Ebonite" }
                });

            migrationBuilder.InsertData(
                table: "RaceStatLimit",
                columns: new[] { "Id", "MaxValue", "MinValue", "RaceId", "StatType" },
                values: new object[,]
                {
                    { new Guid("02e91a5d-45e8-4451-8126-6f5de50fd869"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 8 },
                    { new Guid("19b84944-a385-40f2-aed6-3879ca292457"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 9 },
                    { new Guid("1ac0a675-38fd-422e-b777-d9f3fd845d27"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 7 },
                    { new Guid("48aee723-bb23-4806-9cb0-bf51bd1cdcad"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 0 },
                    { new Guid("64159957-dd22-4886-bb9e-7023813d2156"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 0 },
                    { new Guid("8338f01d-b1cf-434e-89b0-5a46466c3fe2"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 8 },
                    { new Guid("87fbbdb3-42a9-440a-8429-87183f63124e"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 7 },
                    { new Guid("94f07249-bef9-4692-967b-5846f5103d81"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 6 },
                    { new Guid("a2a1af3c-2b01-4ed7-9054-43079bf02831"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 9 },
                    { new Guid("b944ccbb-f8c9-4eaa-a5a1-37dd7440d870"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 6 },
                    { new Guid("bb4a35cf-6411-4573-b793-927cbb50653e"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1 },
                    { new Guid("e3be5f67-fd2f-436e-93de-d0f3dff09e81"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEquipments_EquipmentItemId",
                table: "CharacterEquipments",
                column: "EquipmentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_RaceId",
                table: "Characters",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_TrainingPackageId",
                table: "Characters",
                column: "TrainingPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterTraits_TraitId",
                table: "CharacterTraits",
                column: "TraitId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceSkills_RaceId",
                table: "RaceSkills",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceStatLimit_RaceId",
                table: "RaceStatLimit",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CharacterId",
                table: "Skills",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_CharacterId",
                table: "Stats",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPackageSkill_TrainingPackageId",
                table: "TrainingPackageSkill",
                column: "TrainingPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPackageStatRequirement_TrainingPackageId",
                table: "TrainingPackageStatRequirement",
                column: "TrainingPackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterEquipments");

            migrationBuilder.DropTable(
                name: "CharacterTraits");

            migrationBuilder.DropTable(
                name: "RaceSkills");

            migrationBuilder.DropTable(
                name: "RaceStatLimit");

            migrationBuilder.DropTable(
                name: "SkillDefinitions");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "TrainingPackageSkill");

            migrationBuilder.DropTable(
                name: "TrainingPackageStatRequirement");

            migrationBuilder.DropTable(
                name: "EquipmentItems");

            migrationBuilder.DropTable(
                name: "Traits");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "TrainingPackages");
        }
    }
}
