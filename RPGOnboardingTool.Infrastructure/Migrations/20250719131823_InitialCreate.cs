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
                    Weight = table.Column<float>(type: "real", nullable: false),
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
                    MaxHitPoints = table.Column<int>(type: "int", nullable: false),
                    HitPoints = table.Column<int>(type: "int", nullable: false),
                    Closing = table.Column<int>(type: "int", nullable: false),
                    Rushing = table.Column<int>(type: "int", nullable: false),
                    Movement = table.Column<int>(type: "int", nullable: false),
                    EncumbranceValue = table.Column<int>(type: "int", nullable: false),
                    CurrentWeightCarried = table.Column<int>(type: "int", nullable: false),
                    HasFinanceChip = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
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
                columns: new[] { "Id", "Cost", "Description", "IsStartingGear", "Name", "Type", "Weight" },
                values: new object[,]
                {
                    { new Guid("5d21f5b7-806d-4342-ab0f-eb4ef7a85a26"), 100, "", false, "Medkit", 0, 2f },
                    { new Guid("641023ac-0c12-4ac0-be89-8126359ac6a5"), 200, "", false, "Standard Pistol", 0, 2f },
                    { new Guid("9094df5a-fdd3-4258-a9e3-f871e8ea7b3c"), 50, "", false, "Combat Knife", 0, 1f },
                    { new Guid("a76a7fec-55be-450c-a639-137a136f38c6"), 500, "", false, "Light Body Armor", 0, 10f },
                    { new Guid("e81ccf8a-324d-41e2-b7bb-22513c406230"), 1000, "", false, "Heavy Rifle", 0, 15f }
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
                    { new Guid("067eb129-da00-4dee-82c5-4e16820c2a4e"), "Stealth", 1 },
                    { new Guid("098af14a-3d3d-47eb-979e-824b0a25647f"), "History", 6 },
                    { new Guid("0d0bb1fa-884b-461f-99f6-bcebad14dc66"), "Detect", 9 },
                    { new Guid("1b7651e6-bee0-496b-8415-defc9f2390e2"), "Acrobatics", 1 },
                    { new Guid("27a07830-1281-4610-a035-c0bdd8e8f9d4"), "Athletics", 0 },
                    { new Guid("2aea19c0-2e4a-4a64-8c1f-2bb42093c090"), "Survival", 9 },
                    { new Guid("35f417e3-4921-426e-95b8-b84ed08b481f"), "Education: Academic", 6 },
                    { new Guid("3cbb9005-9296-43df-a8b9-fc8d36c3a7d5"), "Thermal: Blue Ebonite", 2 },
                    { new Guid("6d522c69-06a4-4ad6-8257-0b78e5e28a6c"), "Persuasion", 8 },
                    { new Guid("706702f6-2f1b-4aec-8770-3409cd78f1f5"), "Protect", 0 },
                    { new Guid("8c233c39-875b-4046-b251-27afb2035b0b"), "EBB - Heal", 6 },
                    { new Guid("b5769551-3af1-4dc6-befe-4d1b93c9c4c4"), "Intimidation", 8 },
                    { new Guid("c5533b29-9e5f-4c49-bed8-957e82621419"), "Insight", 9 },
                    { new Guid("fc369ee1-ff63-4d3b-893e-cbf20a1095b7"), "Deception", 8 }
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
                    { new Guid("21a69994-9b54-4513-b0a2-55f0ed506de8"), 10, "A negative trait that imposes penalties.", false, 1, "Cursed", false, 1 },
                    { new Guid("8f3d30d5-563b-44d0-9a3c-8b44c28d6faa"), -5, "Increases resistance to fear effects.", false, 1, "Bravery", false, 0 }
                });

            migrationBuilder.InsertData(
                table: "RaceSkills",
                columns: new[] { "Id", "RaceId", "Rank", "SkillName" },
                values: new object[,]
                {
                    { new Guid("3f58c20c-3fb0-4aeb-9614-e1316918cff7"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "Detect" },
                    { new Guid("6d9ea673-3e6c-42fe-b564-d0d239c284f5"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "Thermal: Blue Ebonite" },
                    { new Guid("6f6308ef-609c-44f0-b3eb-8f1c8e8f08e8"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "EBB - Heal" },
                    { new Guid("6ffa8f08-0e97-403a-ad70-15d085115355"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "Protect" },
                    { new Guid("c03cb46d-16bf-47d3-9771-65e15787d100"), new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1, "Education: Academic" }
                });

            migrationBuilder.InsertData(
                table: "RaceStatLimits",
                columns: new[] { "Id", "MaxValue", "MinValue", "RaceId", "StatType" },
                values: new object[,]
                {
                    { new Guid("07c9440e-098a-46cc-bf4b-cd5a87d41a1c"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 7 },
                    { new Guid("08d73965-f1ae-459b-9c0f-cb299a14c443"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 8 },
                    { new Guid("090c673e-b0ae-455a-bbce-c6705bd9b889"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 7 },
                    { new Guid("0d969661-1d56-4350-89f4-e76b6ac32751"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 8 },
                    { new Guid("0ea16e41-a0a6-404e-8e22-f7e55c8faa33"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 6 },
                    { new Guid("1504b0e9-e388-4373-94fa-589e59adef8c"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 7 },
                    { new Guid("26620e44-83fd-400e-9cc8-bfc6cacb6aff"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 1 },
                    { new Guid("270303cd-b005-4035-a404-4007e4e259b7"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 0 },
                    { new Guid("3a6cbe0f-6e3e-4a30-b55b-ca49006f6bb0"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 1 },
                    { new Guid("3b3eb0f6-07c6-4a06-a5fc-953422d356a6"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 9 },
                    { new Guid("50ffe149-5a79-4666-96e9-bc071c8eed26"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 9 },
                    { new Guid("5226312e-ac5e-4d59-a047-b028e4289efa"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 9 },
                    { new Guid("528d2920-5d4b-40c4-ae16-3db8ab6983dc"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 9 },
                    { new Guid("53b5908d-575b-46d3-82cf-5f0d265ce854"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 0 },
                    { new Guid("561746b7-394e-4efe-b835-4257c02348df"), 7, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 6 },
                    { new Guid("5f482ece-d297-485a-b8a2-ebfabadfae8a"), 8, 3, new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 0 },
                    { new Guid("7b70b289-01bb-461c-8ab6-0108dee1a93e"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 8 },
                    { new Guid("7cf192c9-1305-43c0-9875-40230202ffa2"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 0 },
                    { new Guid("7f6e5f39-ef9c-4e37-be58-89d2b88e5eef"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 1 },
                    { new Guid("82a6212d-2eab-4c43-935a-31601dbd88f3"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 6 },
                    { new Guid("897b85bd-dc26-442a-84ce-49a8f5a148e7"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 6 },
                    { new Guid("8e9b57d8-4c62-4794-9370-0c1e022571e2"), 8, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 1 },
                    { new Guid("95d4db17-4b95-4bff-8846-c5b0fc7d66c3"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 7 },
                    { new Guid("9e50089f-bb55-48c1-b2da-702aa948f660"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 9 },
                    { new Guid("9f6a87b2-e006-45ca-9981-9db98f905cb8"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 0 },
                    { new Guid("a3d1f9c1-2fc3-41ab-b64f-789b3f6cbf4c"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 7 },
                    { new Guid("a5209533-6c66-4793-99c7-032bd4b83c00"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445507"), 6 },
                    { new Guid("b6fec599-6bf3-4d30-937d-fab1616bf53a"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 1 },
                    { new Guid("ba89c717-d4e5-47be-8f88-fc61121a1500"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445502"), 8 },
                    { new Guid("cd4c7bce-3ad7-433f-818a-7d238fa891ae"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 9 },
                    { new Guid("ce660c5b-da6a-42a6-850a-e50ee87a20dc"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 1 },
                    { new Guid("cfea97e5-29e6-48ae-b711-a84160afa709"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 9 },
                    { new Guid("d555d653-b80b-4ac8-befb-e1190c79c632"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 8 },
                    { new Guid("d95e9449-c57e-4723-9875-61be00c66cc9"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 8 },
                    { new Guid("dc7e0f97-d3c1-437f-a58c-6ed2b414cfd5"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 7 },
                    { new Guid("dd7681b0-2dbc-440b-b061-20ad90b863b1"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 0 },
                    { new Guid("e347cda1-2eb4-4f66-9a8c-adda5b4d6f1c"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445504"), 0 },
                    { new Guid("e3fa9dfb-d258-4b3f-9a40-9194fa242b80"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 8 },
                    { new Guid("e4093d11-6cdb-4b96-b392-29f3357f7f13"), 4, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 6 },
                    { new Guid("e4790827-a247-42a0-aa5d-e3cbe70b668c"), 5, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 6 },
                    { new Guid("e7e108d4-dd22-49a7-8113-c94f5384a50e"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445501"), 9 },
                    { new Guid("e843bba7-ccf4-4ffe-bdfa-27bb74162ced"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 7 },
                    { new Guid("ea9908ec-2a8c-4ea9-8e8b-9a1317b49f1f"), 7, 2, new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 8 },
                    { new Guid("ed6eff98-1b83-4f5b-9748-902c1337eaad"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 0 },
                    { new Guid("f13071ae-18e1-4999-970a-55143ad6a036"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445503"), 1 },
                    { new Guid("f1bd1601-04a5-4cf2-8bad-a86af37c7275"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445506"), 7 },
                    { new Guid("f2cae6d3-bb4b-479a-b15f-13fe64183b04"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445508"), 1 },
                    { new Guid("f6d4c448-3e5d-4115-83be-cc15c64b8426"), 6, 1, new Guid("a1b2c3d4-e5f6-4000-8000-112233445505"), 6 }
                });

            migrationBuilder.InsertData(
                table: "TrainingPackageSkills",
                columns: new[] { "Id", "Rank", "SkillName", "TrainingPackageId" },
                values: new object[,]
                {
                    { new Guid("377ad08d-7f82-4da5-8d8f-84b2c03df76c"), 2, "Protect", new Guid("b1b2c3d4-e5f6-4000-8000-112233445502") },
                    { new Guid("6c3efb81-915f-43a6-9b33-6f8b027f89a1"), 1, "Athletics", new Guid("b1b2c3d4-e5f6-4000-8000-112233445501") },
                    { new Guid("73d59505-96d2-4473-a644-d2f2501dfd30"), 2, "Acrobatics", new Guid("b1b2c3d4-e5f6-4000-8000-112233445507") },
                    { new Guid("828945d3-f55d-4607-9a8a-eb612e352112"), 2, "Athletics", new Guid("b1b2c3d4-e5f6-4000-8000-112233445506") },
                    { new Guid("bc5eaa00-f494-4d68-8db2-66ed4ec854a5"), 2, "Insight", new Guid("b1b2c3d4-e5f6-4000-8000-112233445508") },
                    { new Guid("ddec62e6-6108-4618-b23e-70100b0e9b5e"), 2, "Persuasion", new Guid("b1b2c3d4-e5f6-4000-8000-112233445504") },
                    { new Guid("e00b8558-655e-4571-8472-2bca8b6f34dd"), 2, "Intimidation", new Guid("b1b2c3d4-e5f6-4000-8000-112233445503") },
                    { new Guid("f2fca20a-f4bb-4e30-afcf-7a5dd600e365"), 2, "Stealth", new Guid("b1b2c3d4-e5f6-4000-8000-112233445505") }
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
