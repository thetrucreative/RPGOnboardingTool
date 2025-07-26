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
                    Type = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Effect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "RaceStatLimits",
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
                    table.PrimaryKey("PK_RaceStatLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceStatLimits_Races_RaceId",
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
                    HasFinanceChip = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "TrainingPackageSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainingPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPackageSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPackageSkills_TrainingPackages_TrainingPackageId",
                        column: x => x.TrainingPackageId,
                        principalTable: "TrainingPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPackageStatRequirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainingPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatType = table.Column<int>(type: "int", nullable: false),
                    MinValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPackageStatRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPackageStatRequirements_TrainingPackages_TrainingPackageId",
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
                name: "CharacterGeneralItems",
                columns: table => new
                {
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneralItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterGeneralItems", x => new { x.CharacterId, x.GeneralItemId });
                    table.ForeignKey(
                        name: "FK_CharacterGeneralItems_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterGeneralItems_GeneralItems_GeneralItemId",
                        column: x => x.GeneralItemId,
                        principalTable: "GeneralItems",
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
                table: "EquipmentItems",
                columns: new[] { "Id", "Cost", "Description", "ImageUrl", "Name", "Type", "Weight" },
                values: new object[,]
                {
                    { new Guid("060e44f8-b1d7-42d5-88fa-e095b5296b9b"), 500, "", "/images/equipment/light-body-armor.png", "Light Body Armor", 0, 10 },
                    { new Guid("48a8c1d3-94f9-4d9e-9041-66abb6ccaef6"), 1000, "", "/images/equipment/heavy-rifle.png", "Heavy Rifle", 0, 15 },
                    { new Guid("8301833f-4417-48f5-bc25-d87cd1ea8d0b"), 100, "", "/images/equipment/medkit.png", "Medkit", 0, 2 },
                    { new Guid("92c6b11d-140c-4f9d-99c9-d2ee9a05d4d0"), 50, "", "/images/equipment/combat-knife.png", "Combat Knife", 0, 1 },
                    { new Guid("e8619478-fcb6-4ec7-a3a6-5bb5ffbd9789"), 200, "", "/images/equipment/standard-pistol.png", "Standard Pistol", 0, 2 }
                });

            migrationBuilder.InsertData(
                table: "GeneralItems",
                columns: new[] { "Id", "Cost", "Description", "Effect", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("b049ebb0-3ad5-49e2-add0-7469dceeb539"), 50, "", "Grants +1 to all stats for 1 minute", "Stimpack", 1 },
                    { new Guid("d4c80fa0-861a-41b7-b65e-f74d0c6080ee"), 100, "", "Heals 10 HP", "Medkit", 2 }
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "Id", "CanHaveFinanceChip", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445500"), true, "Description for Advanced Carrien.", "/images/races/advanced-carrien.png", "Advanced Carrien" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), false, "Description for Ebonite [Ebon].", "/images/races/ebonite-ebon.png", "Ebonite [Ebon]" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), false, "Description for Ebonite [Waister].", "/images/races/ebonite-waister.png", "Ebonite [Waister]" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), true, "Description for Frother.", "/images/races/frother.png", "Frother" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), true, "Balanced and adaptable.", "/images/races/human.png", "Human" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), true, "Intelligent and diplomatic.", "/images/races/neophron.png", "Neophron" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), true, "Resilient and cunning.", "/images/races/shaktar.png", "Shaktar" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), true, "Brutal and intimidating.", "/images/races/stormer-313-malice.png", "Stormer 313 'Malice'" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), true, "Stealthy and agile.", "/images/races/stormer-711-xeno.png", "Stormer 711 'Xeno'" },
                    { new Guid("a1b2c3d4-e5f6-4000-8000-112233445509"), true, "Observant and tracking.", "/images/races/wraithen.png", "Wraithen" }
                });

            migrationBuilder.InsertData(
                table: "SkillDefinitions",
                columns: new[] { "Id", "Name", "RelatedStat" },
                values: new object[,]
                {
                    { new Guid("02bb0efa-6716-4b53-9e28-186cf2f144dc"), "LANGUAGE [WRAITHEN]", 2 },
                    { new Guid("0763b41b-4b17-4699-89a0-d3f4cfc3eeec"), "INTERROGATE", 5 },
                    { new Guid("07bac885-fd04-4e66-ad5a-e2b781eaec17"), "READ LIPS", 3 },
                    { new Guid("088f7eb5-5a96-407f-91be-cb9499ead3ab"), "LANGUAGE [SHAKTARIAN]", 2 },
                    { new Guid("0ff222bb-bc04-435c-b030-bc0234b29cc5"), "DETECT", 3 },
                    { new Guid("10c19513-29a3-4cfc-8f9a-2b47e9fe0291"), "ORATORY", 4 },
                    { new Guid("191b9c5a-0452-46d0-86a5-678243b36b75"), "MELEE WEAPONS", 0 },
                    { new Guid("1b192aaa-14b3-42b6-920e-b6692bb6a3f1"), "DEMOLITIONS", 3 },
                    { new Guid("1c39e0f7-91dc-4792-9e84-932c843e838f"), "ATHLETICS", 1 },
                    { new Guid("23285564-b6ee-4454-b4ad-07ada636ec47"), "COMPUTER", 2 },
                    { new Guid("2e872027-22ad-4469-a730-439bc17a28e8"), "POLEARM", 0 },
                    { new Guid("3156361a-86e6-4a14-8c35-7289ac569bd3"), "DIPLOMACY", 4 },
                    { new Guid("3165237e-87f8-4eb4-8382-73b11f3d60c9"), "HAGGLE", 4 },
                    { new Guid("33591db2-3e55-4441-ad6e-ca73b78182b6"), "LOCK PICK [MANUAL]", 3 },
                    { new Guid("36e9a75d-6c64-4541-9375-02022fe121a2"), "STEALTH", 1 },
                    { new Guid("383af723-51a4-4267-9147-e46ac7c4f2a8"), "THROW", 0 },
                    { new Guid("38946cc9-90b8-4a64-9047-0db075b5ed0b"), "DRIVE [PILOT]", 3 },
                    { new Guid("3e769d18-5952-4f28-b4cf-c77ee088433c"), "CLIMBING", 0 },
                    { new Guid("45f81549-9b5b-4b85-8ff6-5461410d4e3e"), "TORTURE", 5 },
                    { new Guid("47f0d581-9e84-4b82-af1c-016ec3e635a7"), "LORE [CULT]", 2 },
                    { new Guid("4bf445c8-3524-4515-b477-86b60c0d0b28"), "TECHNICAL [ELECTRICAL]", 3 },
                    { new Guid("4c2a7420-346a-40b9-826d-2621fc534865"), "EDUCATION [NATURAL]", 2 },
                    { new Guid("50c896ea-ec64-44e3-a2a3-49e09198a085"), "STREETWISE", 2 },
                    { new Guid("53430e5f-93fb-4b30-ac2f-271adb2d35e6"), "LANGUAGE [NEOPHRON]", 2 },
                    { new Guid("576905b1-4b56-4b7d-897c-5b7d1884dd5d"), "ACROBATICS", 1 },
                    { new Guid("57a23add-621b-43c5-94b6-ade267497a53"), "PERSUASION", 4 },
                    { new Guid("5af5bd63-646c-47b4-bcca-564bfdbc81e8"), "PISTOL", 1 },
                    { new Guid("5cd53470-fb02-4d40-a337-b32dfcd3ac3b"), "DRIVE [MILITARY]", 3 },
                    { new Guid("61ccb4cb-49ec-4333-a444-1433711ff3f5"), "LANGUAGE [GRISTLE]", 2 },
                    { new Guid("651accde-c1b6-4a9d-9de9-b2954001304f"), "SURVIVAL", 5 },
                    { new Guid("6b58db73-4cc0-4b6c-bd35-95f1666d591a"), "SLEIGHT", 1 },
                    { new Guid("6b5d3f10-930b-4304-a771-3ce469519788"), "LORE [SECTOR]", 2 },
                    { new Guid("6c850676-60e5-42eb-b0fd-a001b477f3bb"), "LOCK PICK [ELECTRONIC]", 3 },
                    { new Guid("718b81f4-8da5-4b23-b1a0-58c3be5b1b34"), "BRIBERY", 5 },
                    { new Guid("74dec3fe-2a8d-4509-87e7-c17953907586"), "DRIVE [CIVILIAN]", 3 },
                    { new Guid("7565e1a5-5622-4026-b41e-1c88b4736a0a"), "EDUCATION [ACADEMIC]", 2 },
                    { new Guid("79a5df2a-6179-4b87-9122-a5b711b8d1fb"), "LORE [DREAM]", 2 },
                    { new Guid("7e3fd7c6-df08-4f5c-8496-9aa8315dfa53"), "RIFLE", 1 },
                    { new Guid("83da8c95-fca6-4d7e-bdb6-2e6c12905bbe"), "TECHNICAL [MECHANICAL]", 3 },
                    { new Guid("958f7378-60c4-4a61-9b61-6918873779ae"), "ADMIN & FINANCE", 2 },
                    { new Guid("9d09cbd5-c355-4d64-aea3-a221e480259d"), "TRACKING", 3 },
                    { new Guid("9e71312e-6bf8-411d-b3ab-a1fd7982b05b"), "SWIMMING", 0 },
                    { new Guid("9f8b2200-aa84-4723-80d8-cf10dd6d645f"), "MEDICAL", 2 },
                    { new Guid("a2757858-b380-433e-8d98-8673ad9cf624"), "DRIVE [MOTORCYCLE]", 1 },
                    { new Guid("ac7581a9-39e4-4749-b944-3c27b48213e6"), "LANGUAGE [SIGN]", 2 },
                    { new Guid("b3cec7f5-06cf-4c33-876c-8a8806838b58"), "GAMBLING", 5 },
                    { new Guid("b7c8b635-b390-4601-aff5-b674efca3511"), "SHIELD CRAFT", 0 },
                    { new Guid("b987d6ee-a26f-4c79-a6dd-e6eeb99af97f"), "UNARMED COMBAT", 0 },
                    { new Guid("c1bedcdd-83e1-4576-9ea0-778d4828e0a1"), "TACTICS", 3 },
                    { new Guid("cbe4f3d1-7114-4d78-aca1-ea027661a723"), "TECHNICAL [WEAPONS]", 3 },
                    { new Guid("d1f20962-d1cb-4232-b7ea-9c4983150900"), "LEADERSHIP", 4 },
                    { new Guid("d6b6c75f-01fa-4725-ac5b-ec93e1853294"), "SEDUCTION", 4 },
                    { new Guid("eb250f61-63c8-4414-844e-8ad512dc480d"), "FORGERY", 1 },
                    { new Guid("f19804bf-7533-497b-8494-862fd43cb419"), "LANGUAGE [BIYA]", 2 },
                    { new Guid("f4cd0cf6-2d4c-40ea-a3fb-bc2710293de8"), "FORENSICS", 2 },
                    { new Guid("fa78eeea-37a8-4146-a960-b76ff6e21071"), "SUPPORT WEAPONS", 0 },
                    { new Guid("fc0f7ce8-e881-4087-9788-a1fb51fcf2bd"), "INTIMIDATE", 5 }
                });

            migrationBuilder.InsertData(
                table: "TrainingPackages",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445501"), "A training package for bureaucrats.", "Bureaucrat" },
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445502"), "A training package for close assault.", "Close Assault" },
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445503"), "A training package for heavy support.", "Heavy Support" },
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445504"), "A training package for investigation and interrogation.", "Investigation & Interrogation" },
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445505"), "A training package for medics.", "Medic" },
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445506"), "A training package for scouts.", "Scout" },
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445507"), "A training package for strike and sweep operations.", "Strike & Sweep" },
                    { new Guid("b1b2c3d4-e5f6-4000-8000-112233445508"), "A training package for technical experts.", "Technical" }
                });

            migrationBuilder.InsertData(
                table: "Traits",
                columns: new[] { "Id", "BasePointCost", "Description", "IsUnique", "MaxRankAtCreation", "Name", "RequiresDetail", "Type" },
                values: new object[,]
                {
                    { new Guid("247b96cb-c655-4ac8-875c-24edef35fd50"), 10, "A negative trait that imposes penalties.", false, 1, "Cursed", false, 1 },
                    { new Guid("beb9bd8f-9bc1-4ee1-ba22-6fa8f48735b4"), -5, "Increases resistance to fear effects.", false, 1, "Bravery", false, 0 }
                });

            migrationBuilder.InsertData(
                table: "RaceSkills",
                columns: new[] { "Id", "RaceId", "Rank", "SkillName" },
                values: new object[,]
                {
                    { new Guid("00d2c415-07dc-4ea9-b930-7ea2c73979e1"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 1, "Detect" },
                    { new Guid("027bd773-9c80-4295-96e2-a4fb775706b3"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 1, "Language: Shaktar" },
                    { new Guid("066670e4-8857-4f6a-9ab4-d11863213b14"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 1, "Melee Weapons" },
                    { new Guid("0e7e5c7e-8310-4626-9010-f6064f6370eb"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 1, "Leadership" },
                    { new Guid("10992c2a-25ef-4ecf-8563-4b0263f19dfc"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445500"), 1, "Unarmed Combat" },
                    { new Guid("1c030b2b-06f1-4855-9364-1600816f9453"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 1, "Stealth" },
                    { new Guid("217525a1-5973-4a75-9fad-f1e7d44e1fbd"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 1, "Education: Natural" },
                    { new Guid("2d7b459c-0403-4f74-8341-62ebb5e76b9c"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 1, "Education: Academic" },
                    { new Guid("3349e3b1-a8b0-4a68-9e60-63bf9ff57be1"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 1, "Protect" },
                    { new Guid("39175773-ea2e-40cb-a722-09a75b456651"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 1, "Detect" },
                    { new Guid("43f000e3-9f16-4006-97b2-fd07e4cd2e4e"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 1, "Persuasion" },
                    { new Guid("4570f104-ac96-4e08-9c63-808d1053d21c"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 1, "Education: Academic" },
                    { new Guid("467f5f2a-e4ee-415b-acf5-37d3206b88c6"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "Thermal: Red" },
                    { new Guid("4d4ee7d3-ae8d-4a90-8080-2a87fc5997c5"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 1, "Language +1" },
                    { new Guid("4d5a5e49-f567-4c0c-a18f-33ed28bc489a"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 1, "Thermal: Blue" },
                    { new Guid("4ed778e6-eef5-4c0a-b482-5837ba39ebb7"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445500"), 1, "Lore: Sector" },
                    { new Guid("55ce64ec-a729-433c-8d3a-18ead979ee86"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "Detect" },
                    { new Guid("58e96d4c-6a3f-4e97-83bc-edd21ef8ee0d"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445509"), 1, "Unarmed Combat" },
                    { new Guid("5aa446bd-75ed-4c9b-8698-437f1c4ff782"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 1, "Unarmed Combat" },
                    { new Guid("600f855e-78a2-48f7-88ba-8cf43eb8425c"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "EBB - Blast" },
                    { new Guid("6490c7b1-38af-4c5e-b115-bb0d670a6a37"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 1, "Unarmed Combat" },
                    { new Guid("64b9f6f2-a6f3-4a2d-ac59-c0986beaed2d"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 1, "Throw" },
                    { new Guid("752af0c2-965e-4666-975b-8d436058523f"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 1, "Survival" },
                    { new Guid("7d48240f-b6ad-4788-b5e3-0fb69bf708b0"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 1, "Oratory" },
                    { new Guid("7e6826c1-2eb2-4ef8-8cb8-115e189d1183"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445500"), 1, "Language: Gristle" },
                    { new Guid("80b8c71b-63e5-48f0-a698-3adf60f036ff"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 1, "Language: Neophron" },
                    { new Guid("8634c49c-48cf-4aca-b319-b1c460b8b157"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "Protect" },
                    { new Guid("8b180d60-1a82-438d-a0aa-d49266b47e7c"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 1, "Climbing" },
                    { new Guid("8ce5cd4e-8d6e-491c-83db-8bddd8201d9f"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445509"), 1, "Language: Wraithen" },
                    { new Guid("8f7480af-38d5-4f6d-b7a0-82c5ff59bf51"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 1, "Education: Academic" },
                    { new Guid("9c7204fa-7952-4ea8-8cc1-08504f96229a"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 1, "Bribery" },
                    { new Guid("a74e444c-de5f-4b2f-8987-d85c52f2f6b7"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 1, "Detect" },
                    { new Guid("ad129f84-3015-4a07-8bd2-e6119dd899f1"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445509"), 1, "Athletics" },
                    { new Guid("b5905b09-989d-435e-b37f-0f970569518a"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445509"), 1, "Detect" },
                    { new Guid("bb906f69-5237-4d5b-9fd4-516aa9757261"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 1, "Unarmed Combat" },
                    { new Guid("c2e6d37d-f0d3-4850-b88e-9b95dd315897"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 1, "Unarmed Combat" },
                    { new Guid("c31783d9-7e13-490c-8c24-b59fd1a92979"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 1, "Athletics" },
                    { new Guid("c36711da-ab9a-43c8-8563-f58e2bd2ac11"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445500"), 1, "Intimidate" },
                    { new Guid("c7d86c15-e88f-4524-9bd6-f284fcba1f62"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445500"), 1, "Survival" },
                    { new Guid("c7fce77e-f68e-4b39-a64c-60ec13853dbf"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 1, "Climbing" },
                    { new Guid("cac720b8-a1f9-483a-a182-d594dbc82a24"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445509"), 1, "Tracking" },
                    { new Guid("cce2ed1b-a2c5-4131-b1b0-0e84810fb92e"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 1, "Streetwise" },
                    { new Guid("d41e3555-5032-49bd-adad-e4f919930847"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 1, "Diplomacy" },
                    { new Guid("e5586708-d9e7-4bfa-9100-1e59edf17889"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 1, "Intimidate" },
                    { new Guid("eb736d42-bd03-4bc9-8947-7f390f0fe673"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 1, "Detect" },
                    { new Guid("f47efa46-0aec-40c0-a0b6-a7a56b72b5ca"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 1, "Streetwise" },
                    { new Guid("f561108b-1ebe-4679-9cf2-222f51280407"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "Education: Academic" },
                    { new Guid("f67cea28-5ffd-4650-baa0-3b33a350424f"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 1, "EBB - Heal" },
                    { new Guid("f7694bcb-32ca-49c3-83f6-e3c4f152aea1"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 1, "Unarmed Combat" }
                });

            migrationBuilder.InsertData(
                table: "RaceStatLimits",
                columns: new[] { "Id", "MaxValue", "MinValue", "RaceId", "StatType" },
                values: new object[,]
                {
                    { new Guid("00ce7b7a-7553-4bab-af5a-c9dd68379c9e"), 3, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445509"), 0 },
                    { new Guid("02fb5c01-e5cc-4311-9bde-3ba1822082ce"), 4, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 3 },
                    { new Guid("04d89d8e-38d3-4ad1-a6a6-afd0ab50b8e0"), 6, 3, new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 4 },
                    { new Guid("0d511757-d678-4b27-9a07-22eb6b67130a"), 5, 3, new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 1 },
                    { new Guid("0d9d45be-ba6a-4c90-aa59-fdacf38cf156"), 4, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445509"), 3 },
                    { new Guid("1021e8ee-7186-4ff6-b1ee-3ec79bbae8e9"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 5 },
                    { new Guid("157b56fb-05da-4a31-ad8f-3f8ab7f968ea"), 6, 3, new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 5 },
                    { new Guid("22905ead-dc7d-44cd-b336-4cbccc97a0fd"), 4, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 2 },
                    { new Guid("26a27fd3-9b95-410c-994d-3063e5eb0af1"), 4, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445509"), 2 },
                    { new Guid("29d56051-7635-4d2a-8fd4-357c28c977ab"), 2, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 0 },
                    { new Guid("33f59c54-36a9-41c6-8836-fd9a471f1229"), 5, 3, new Guid("a1b2c3d4-e5f6-4000-8000-112233445500"), 0 },
                    { new Guid("354185f2-e8e5-4221-bd15-c709c57e962d"), 5, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 0 },
                    { new Guid("37fd54b4-5bb6-4631-9da8-098c96a4f357"), 5, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 2 },
                    { new Guid("43171e17-3d00-4824-a919-13e233a006d4"), 3, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 1 },
                    { new Guid("45c652f0-2d8e-4bab-bac5-c009b477afb4"), 3, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 6 },
                    { new Guid("485f9d0e-0c9b-4794-b0d7-59d4bd4fb687"), 3, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 3 },
                    { new Guid("4ad79363-3858-4ccf-a441-4c0bae5e3ca2"), 3, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 0 },
                    { new Guid("4b7ae605-7b92-43a7-a574-fe12ff2b5ad1"), 3, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 4 },
                    { new Guid("4e5b01ad-b88f-429d-9f8d-bdf9a51aa58f"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 4 },
                    { new Guid("51972c97-7331-4e76-a773-ce7a28c93039"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 4 },
                    { new Guid("51a3f888-2649-41ea-a3ef-7945c6a21960"), 6, 3, new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 0 },
                    { new Guid("542b6448-1eaf-44ee-aab4-0f77772f8d08"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 5 },
                    { new Guid("54b0daae-f56d-4760-af3e-813bb5c421e9"), 6, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 1 },
                    { new Guid("5a31a656-7aee-4832-b7cb-05596e738a9f"), 6, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 2 },
                    { new Guid("5b463d4e-9aba-465d-bd19-6d304ac2e92d"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 5 },
                    { new Guid("5eafc658-5328-4710-8a3a-44106863f466"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 2 },
                    { new Guid("6007cef0-d14a-4bd1-9e3c-57b53770409d"), 6, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 6 },
                    { new Guid("62a8761a-00b2-431a-9476-dfade5697589"), 3, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 3 },
                    { new Guid("6351635c-65db-4b91-89b0-3c22a3a5403a"), 5, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445509"), 5 },
                    { new Guid("6acc4ddf-c800-42c2-acc7-db78867d1dcf"), 3, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 6 },
                    { new Guid("6c77355a-f7bc-4e2d-87f9-24e1007a4f54"), 4, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 1 },
                    { new Guid("6cdaf922-05f7-4d44-a2f4-a2dd0872bd2a"), 4, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445500"), 3 },
                    { new Guid("729b869d-bf6b-40bc-9d4a-0c2e3e3d6a76"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 5 },
                    { new Guid("781b8f36-1175-4b6d-aa6d-a94fec0e392e"), 6, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 3 },
                    { new Guid("7a730e36-edb9-4592-94bb-cd656cf7ed82"), 4, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 1 },
                    { new Guid("8037b14b-1bb1-4920-b050-9ab62ac582e7"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 3 },
                    { new Guid("814df63f-2f1b-4ef5-91ee-eb0575eb85cd"), 2, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 4 },
                    { new Guid("8395080e-63b3-4d22-9cc0-a22512db5007"), 3, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 0 },
                    { new Guid("8909d2bd-2cc6-47c4-96d5-e5ed5ede2e10"), 2, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445500"), 2 },
                    { new Guid("9a7ff4e9-4cc0-46d4-8111-942a5750ee22"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 5 },
                    { new Guid("a0c4a78c-78dc-45ed-99d5-b9c496a6c39c"), 3, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 2 },
                    { new Guid("a2efe91d-e265-40b3-86de-9cd0f3c95960"), 2, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 6 },
                    { new Guid("a32a8fe7-5856-4569-b992-c63594abb85d"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445500"), 1 },
                    { new Guid("ab8ee08f-b918-4024-af10-caa20209b901"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 4 },
                    { new Guid("adad620b-6db2-4651-bb80-d81d7e11fc32"), 3, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 6 },
                    { new Guid("b935e9d5-561d-49f7-aa5a-8ac55d1c0861"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 2 },
                    { new Guid("c3f846b5-43a2-4f1a-8fbd-3ba32581599b"), 4, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 4 },
                    { new Guid("c943050b-f171-4a09-944d-4b213b0bc51e"), 6, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 3 },
                    { new Guid("cd2a3da1-504b-4302-9245-a9227c6a3069"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 2 },
                    { new Guid("d1343a90-f889-4c8a-b4de-9ebf0ea1eb19"), 6, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 6 },
                    { new Guid("d76a4a34-9bef-495a-8434-d8a77c88f4cd"), 3, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 4 },
                    { new Guid("d895d63a-a8ec-4eab-b716-bb3e3cb5dae8"), 4, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445509"), 4 },
                    { new Guid("dc084496-48ba-46c7-b0fe-f6e5143e5a6c"), 6, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 3 },
                    { new Guid("ded37c27-8383-4ca0-84b6-e0f9fe12bdae"), 6, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 5 },
                    { new Guid("e01df118-e2ec-4881-ab41-88695e993048"), 5, 3, new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 0 },
                    { new Guid("e093bdfa-69d1-44ac-bbd9-08d4b354fa14"), 3, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 0 },
                    { new Guid("e3480836-efc3-4775-ac91-17dd88693992"), 4, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 0 },
                    { new Guid("e389a4fb-ff37-4c14-a769-dfc2464016b2"), 4, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445509"), 6 },
                    { new Guid("e54ae88f-2907-4f55-a96b-625c6620fe0f"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 6 },
                    { new Guid("e606df08-15e9-49e3-bb9f-36fee6eb445a"), 3, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 3 },
                    { new Guid("ebff996f-0954-4f4d-81ef-f6e189c5aff4"), 3, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445500"), 6 },
                    { new Guid("ee358a02-a062-434f-855e-08a842f76002"), 4, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 1 },
                    { new Guid("f2c2ddab-ef7e-4405-af2a-946842719859"), 6, 3, new Guid("a1b2c3d4-e5f6-4000-8000-112233445509"), 1 },
                    { new Guid("f30e30c1-f34e-4f36-bb99-2a8d5966a7c5"), 2, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 2 },
                    { new Guid("fa0343f4-226c-4b09-874f-52e7f6c9e833"), 5, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 1 },
                    { new Guid("fb216813-efab-460e-8b41-ac7512b6da8f"), 4, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1 },
                    { new Guid("fb44750c-714e-4428-a54a-a43d9d55268e"), 2, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 6 },
                    { new Guid("fd4c5b21-b211-42c4-b4e9-677092204fe6"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 5 },
                    { new Guid("fe03485b-7275-46d3-91c9-42d09e6f1391"), 6, 3, new Guid("a1b2c3d4-e5f6-4000-8000-112233445500"), 5 },
                    { new Guid("fe06b4c1-42bb-44d2-9395-c45cdf03bb0b"), 3, 0, new Guid("a1b2c3d4-e5f6-4000-8000-112233445500"), 4 }
                });

            migrationBuilder.InsertData(
                table: "TrainingPackageSkills",
                columns: new[] { "Id", "Rank", "SkillName", "TrainingPackageId" },
                values: new object[,]
                {
                    { new Guid("00e1ba79-18a6-41f2-960b-f32bde091e03"), 1, "Drive Civilian", new Guid("b1b2c3d4-e5f6-4000-8000-112233445507") },
                    { new Guid("066f3088-d932-4736-8648-7a3b7b805c9b"), 2, "Rifle", new Guid("b1b2c3d4-e5f6-4000-8000-112233445503") },
                    { new Guid("0f6d065b-5e59-4e1e-84d2-d9ba78acc781"), 1, "Forensics", new Guid("b1b2c3d4-e5f6-4000-8000-112233445504") },
                    { new Guid("10e23eaf-fc65-42c8-9c17-93a1c5afd744"), 2, "Medical", new Guid("b1b2c3d4-e5f6-4000-8000-112233445505") },
                    { new Guid("1672eca4-90b6-4905-b832-1e5ddd688184"), 1, "Admin & Finance", new Guid("b1b2c3d4-e5f6-4000-8000-112233445501") },
                    { new Guid("17c3ff6a-1d67-48c4-acfd-3f6166047f4d"), 1, "Acrobatics", new Guid("b1b2c3d4-e5f6-4000-8000-112233445502") },
                    { new Guid("1c3ec084-e022-49b2-be2f-97dedf1b7bde"), 1, "Rifle", new Guid("b1b2c3d4-e5f6-4000-8000-112233445506") },
                    { new Guid("1cd2a7b4-fd37-4863-b8a1-f3cb44bd0a03"), 1, "Technical (Pick One)", new Guid("b1b2c3d4-e5f6-4000-8000-112233445508") },
                    { new Guid("1f02c84e-fc82-4d70-9c04-4ccbbb54abc7"), 1, "Haggle", new Guid("b1b2c3d4-e5f6-4000-8000-112233445501") },
                    { new Guid("1fbbf84c-a149-48dd-82da-c4bacd39a0ac"), 2, "Diplomacy", new Guid("b1b2c3d4-e5f6-4000-8000-112233445501") },
                    { new Guid("249876c7-c0d1-43ef-82e0-02b16b9b3e9b"), 2, "Tracking", new Guid("b1b2c3d4-e5f6-4000-8000-112233445506") },
                    { new Guid("2e5b610d-97c9-49d2-91f5-f4190ad17906"), 1, "Athletics", new Guid("b1b2c3d4-e5f6-4000-8000-112233445507") },
                    { new Guid("30772e38-849d-4d51-b615-e73c8c45de7e"), 1, "Melee or Unarmed", new Guid("b1b2c3d4-e5f6-4000-8000-112233445507") },
                    { new Guid("34bdc9f5-3324-4366-836e-1fd9cd9505f3"), 1, "Education: Academic", new Guid("b1b2c3d4-e5f6-4000-8000-112233445505") },
                    { new Guid("3b3f6a91-53dd-464f-9c56-cacd9ae27700"), 2, "Support Weapons", new Guid("b1b2c3d4-e5f6-4000-8000-112233445503") },
                    { new Guid("4aa520e2-5585-472a-a99e-98585b375f2b"), 1, "Pistol", new Guid("b1b2c3d4-e5f6-4000-8000-112233445507") },
                    { new Guid("4b6f4cfc-8e55-4e5a-9746-9f8359ac09a8"), 1, "Rifle", new Guid("b1b2c3d4-e5f6-4000-8000-112233445507") },
                    { new Guid("55c322c7-b199-4003-a6cb-dd4e91b3153b"), 1, "Technical: Weapons", new Guid("b1b2c3d4-e5f6-4000-8000-112233445503") },
                    { new Guid("55eadd3e-2121-4d90-9ae8-4948769b1b66"), 1, "Survival", new Guid("b1b2c3d4-e5f6-4000-8000-112233445506") },
                    { new Guid("5d85ba43-400c-4cfb-83b5-6d075198d95c"), 1, "Medical", new Guid("b1b2c3d4-e5f6-4000-8000-112233445507") },
                    { new Guid("60a25766-39e0-4444-9d7d-08207a248b10"), 1, "Computer", new Guid("b1b2c3d4-e5f6-4000-8000-112233445504") },
                    { new Guid("653b87fe-1a0d-446d-89ac-5793b781853c"), 2, "Technical (Pick One)", new Guid("b1b2c3d4-e5f6-4000-8000-112233445508") },
                    { new Guid("6999fa6b-2d86-4ef7-b3d4-7023aa210f5b"), 2, "Computer", new Guid("b1b2c3d4-e5f6-4000-8000-112233445508") },
                    { new Guid("6d1374de-257c-4555-bede-b3e3cada3ea9"), 2, "Interrgate", new Guid("b1b2c3d4-e5f6-4000-8000-112233445504") },
                    { new Guid("6e4ca43f-d844-459b-bfaf-c83dd7225a1c"), 1, "Detect", new Guid("b1b2c3d4-e5f6-4000-8000-112233445505") },
                    { new Guid("73e36a23-34ec-4133-b4d2-5e6ebc48afff"), 2, "Stealth", new Guid("b1b2c3d4-e5f6-4000-8000-112233445506") },
                    { new Guid("76001b79-e2a7-47e6-9949-c1b829b0fccf"), 1, "Read Lips", new Guid("b1b2c3d4-e5f6-4000-8000-112233445506") },
                    { new Guid("7ce9c0e0-b492-4771-80b7-3a0bf67f6ed5"), 1, "Oratory", new Guid("b1b2c3d4-e5f6-4000-8000-112233445501") },
                    { new Guid("8f32a701-9100-4f5b-b345-ad7b15ec67ab"), 1, "Stealth", new Guid("b1b2c3d4-e5f6-4000-8000-112233445502") },
                    { new Guid("90c74682-fbc0-47d6-981d-3a2b2c43d7ce"), 1, "Detect", new Guid("b1b2c3d4-e5f6-4000-8000-112233445506") },
                    { new Guid("9343a992-e5c2-45f5-ac57-78245113f64d"), 2, "Leadership", new Guid("b1b2c3d4-e5f6-4000-8000-112233445501") },
                    { new Guid("a3649c0c-d3a6-4af8-ac25-02c09420b682"), 2, "Streetwise", new Guid("b1b2c3d4-e5f6-4000-8000-112233445504") },
                    { new Guid("a4d63514-bd99-4ab9-bc99-519347a459dc"), 1, "Haggle", new Guid("b1b2c3d4-e5f6-4000-8000-112233445508") },
                    { new Guid("ab61a606-67a8-494a-b06c-b602e172e5ca"), 2, "Forensics", new Guid("b1b2c3d4-e5f6-4000-8000-112233445505") },
                    { new Guid("b1898734-bac7-4518-bea4-6a7c84a69300"), 1, "Computer", new Guid("b1b2c3d4-e5f6-4000-8000-112233445505") },
                    { new Guid("b3db8c64-3cf8-48c9-b253-88c25a0572b8"), 2, "Athletics", new Guid("b1b2c3d4-e5f6-4000-8000-112233445502") },
                    { new Guid("b65ad30a-9b73-4a96-8c91-68d466d18b88"), 1, "Detect", new Guid("b1b2c3d4-e5f6-4000-8000-112233445504") },
                    { new Guid("b8257213-e1da-41dd-bfa4-b1e1da9414cd"), 1, "Melee/Polearm/Throw or Unarmed", new Guid("b1b2c3d4-e5f6-4000-8000-112233445502") },
                    { new Guid("b91de2e0-11cd-40ef-8346-234f948516dd"), 1, "Throw", new Guid("b1b2c3d4-e5f6-4000-8000-112233445503") },
                    { new Guid("bf830117-4fe1-47fb-bc50-0fabdbd3f813"), 1, "Torture", new Guid("b1b2c3d4-e5f6-4000-8000-112233445504") },
                    { new Guid("c2c26a8d-402d-497c-979b-088e3f98b00d"), 2, "Melee/Polearm/Throw or Unarmed", new Guid("b1b2c3d4-e5f6-4000-8000-112233445502") },
                    { new Guid("c6a0ac2e-ebac-4651-9f7d-edb87521a945"), 1, "Detect", new Guid("b1b2c3d4-e5f6-4000-8000-112233445507") },
                    { new Guid("ce341fba-f5b9-465c-beb7-e632b51eea9d"), 1, "Detect", new Guid("b1b2c3d4-e5f6-4000-8000-112233445508") },
                    { new Guid("d4ebd9cf-1769-401e-be78-f4c6d4ebf0e5"), 1, "Tactics", new Guid("b1b2c3d4-e5f6-4000-8000-112233445503") },
                    { new Guid("dfe28169-0065-4084-aef3-766df8a0120e"), 1, "Climbing", new Guid("b1b2c3d4-e5f6-4000-8000-112233445502") },
                    { new Guid("e682bff4-d57e-4cff-84e2-18743b656ab6"), 1, "Lockpick (Pick One)", new Guid("b1b2c3d4-e5f6-4000-8000-112233445508") },
                    { new Guid("e84575d7-c5eb-4574-a3c8-970b60a8cdc0"), 1, "Education: Natural", new Guid("b1b2c3d4-e5f6-4000-8000-112233445505") },
                    { new Guid("eaa976a8-f691-410e-9b02-f3df0a9e947d"), 1, "Drive Military", new Guid("b1b2c3d4-e5f6-4000-8000-112233445507") },
                    { new Guid("f0be68dc-9151-47d2-b636-906d2e3fa785"), 1, "Demolitions", new Guid("b1b2c3d4-e5f6-4000-8000-112233445503") },
                    { new Guid("f7c2aff4-ffed-40b9-a6cf-20381ff755d7"), 1, "Computer", new Guid("b1b2c3d4-e5f6-4000-8000-112233445501") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEquipments_EquipmentItemId",
                table: "CharacterEquipments",
                column: "EquipmentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterGeneralItems_GeneralItemId",
                table: "CharacterGeneralItems",
                column: "GeneralItemId");

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
                name: "IX_RaceStatLimits_RaceId",
                table: "RaceStatLimits",
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
                name: "IX_TrainingPackageSkills_TrainingPackageId",
                table: "TrainingPackageSkills",
                column: "TrainingPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPackageStatRequirements_TrainingPackageId",
                table: "TrainingPackageStatRequirements",
                column: "TrainingPackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterEquipments");

            migrationBuilder.DropTable(
                name: "CharacterGeneralItems");

            migrationBuilder.DropTable(
                name: "CharacterTraits");

            migrationBuilder.DropTable(
                name: "RaceSkills");

            migrationBuilder.DropTable(
                name: "RaceStatLimits");

            migrationBuilder.DropTable(
                name: "SkillDefinitions");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "TrainingPackageSkills");

            migrationBuilder.DropTable(
                name: "TrainingPackageStatRequirements");

            migrationBuilder.DropTable(
                name: "EquipmentItems");

            migrationBuilder.DropTable(
                name: "GeneralItems");

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
