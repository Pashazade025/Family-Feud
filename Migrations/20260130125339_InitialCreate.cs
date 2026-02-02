using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Family_Feud.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    DeveloperName = table.Column<string>(type: "TEXT", nullable: false),
                    DeveloperEmail = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    RateLimit = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    PreferredLanguage = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestionTextEN = table.Column<string>(type: "TEXT", nullable: false),
                    QuestionTextPL = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Difficulty = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestionId = table.Column<int>(type: "INTEGER", nullable: false),
                    AnswerTextEN = table.Column<string>(type: "TEXT", nullable: false),
                    AnswerTextPL = table.Column<string>(type: "TEXT", nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: false),
                    Rank = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HostUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentQuestionId = table.Column<int>(type: "INTEGER", nullable: true),
                    Team1Score = table.Column<int>(type: "INTEGER", nullable: false),
                    Team2Score = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HostId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Questions_CurrentQuestionId",
                        column: x => x.CurrentQuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Games_Users_HostId",
                        column: x => x.HostId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameParticipants_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameParticipants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Category", "CreatedById", "CreatedByUserId", "Difficulty", "IsActive", "QuestionTextEN", "QuestionTextPL" },
                values: new object[,]
                {
                    { 1, "General", null, 1, 1, true, "Name something people bring to the beach", "Wymień coś, co ludzie zabierają na plażę" },
                    { 2, "Animals", null, 1, 1, true, "Name a popular pet", "Wymień popularne zwierzę domowe" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "PreferredLanguage", "Role", "Username" },
                values: new object[] { 1, new DateTime(2026, 1, 30, 12, 53, 39, 653, DateTimeKind.Utc).AddTicks(7590), "admin@familyfeud.com", "$2a$11$ZUAT4VZFGRShjI.GPv/4/OMOBp2qpCJPqy0cQmlin6dZ9/FbFtZ/.", "en", "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "AnswerTextEN", "AnswerTextPL", "Points", "QuestionId", "Rank" },
                values: new object[,]
                {
                    { 1, "Towel", "Ręcznik", 45, 1, 1 },
                    { 2, "Sunscreen", "Krem z filtrem", 25, 1, 2 },
                    { 3, "Umbrella", "Parasol", 15, 1, 3 },
                    { 4, "Water", "Woda", 10, 1, 4 },
                    { 5, "Snacks", "Przekąski", 5, 1, 5 },
                    { 6, "Dog", "Pies", 50, 2, 1 },
                    { 7, "Cat", "Kot", 35, 2, 2 },
                    { 8, "Fish", "Ryba", 10, 2, 3 },
                    { 9, "Bird", "Ptak", 5, 2, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_GameParticipants_GameId",
                table: "GameParticipants",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameParticipants_UserId",
                table: "GameParticipants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_CurrentQuestionId",
                table: "Games",
                column: "CurrentQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_HostId",
                table: "Games",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatedById",
                table: "Questions",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "ApiKeys");

            migrationBuilder.DropTable(
                name: "GameParticipants");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
