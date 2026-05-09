using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    ContactNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CandidateSkills",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSkills", x => new { x.CandidateId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_CandidateSkills_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "ContactNumber", "DateOfBirth", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "+381601234567", new DateOnly(2000, 1, 1), "anne.smith@gmail.com", "Anne Smith" },
                    { 2, "+381601234568", new DateOnly(1998, 3, 15), "john.peterson@gmail.com", "John Peterson" },
                    { 3, "+381601234569", new DateOnly(1997, 7, 22), "emily.johnson@gmail.com", "Emily Johnson" },
                    { 4, "+381601234570", new DateOnly(1995, 11, 5), "michael.brown@gmail.com", "Michael Brown" },
                    { 5, "+381601234571", new DateOnly(2001, 2, 10), "sophia.davis@gmail.com", "Sophia Davis" },
                    { 6, "+381601234572", new DateOnly(1996, 9, 18), "daniel.wilson@gmail.com", "Daniel Wilson" },
                    { 7, "+381601234573", new DateOnly(1999, 12, 3), "olivia.martinez@gmail.com", "Olivia Martinez" },
                    { 8, "+381601234574", new DateOnly(1994, 6, 27), "james.anderson@gmail.com", "James Anderson" },
                    { 9, "+381601234575", new DateOnly(2002, 4, 14), "emma.thomas@gmail.com", "Emma Thomas" },
                    { 10, "+381601234576", new DateOnly(1993, 8, 30), "william.taylor@gmail.com", "William Taylor" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "C# programming" },
                    { 2, "Java programming" },
                    { 3, "Python" },
                    { 4, "JavaScript" },
                    { 5, "SQL" },
                    { 6, "Database design" },
                    { 7, "NoSQL databases" },
                    { 8, "English" },
                    { 9, "German" },
                    { 10, "Russian" },
                    { 11, "React" },
                    { 12, ".NET" },
                    { 13, "C programming" },
                    { 14, "C++ programming" },
                    { 15, "DevOps" },
                    { 16, "PHP" },
                    { 17, "Jira" },
                    { 18, "Git" },
                    { 19, "Docker" },
                    { 20, "AWS" }
                });

            migrationBuilder.InsertData(
                table: "CandidateSkills",
                columns: new[] { "CandidateId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 5 },
                    { 1, 12 },
                    { 1, 18 },
                    { 2, 2 },
                    { 2, 5 },
                    { 2, 17 },
                    { 2, 20 },
                    { 3, 3 },
                    { 3, 7 },
                    { 3, 19 },
                    { 4, 4 },
                    { 4, 11 },
                    { 4, 18 },
                    { 5, 1 },
                    { 5, 6 },
                    { 5, 12 },
                    { 6, 13 },
                    { 6, 14 },
                    { 6, 18 },
                    { 7, 8 },
                    { 7, 9 },
                    { 7, 17 },
                    { 8, 15 },
                    { 8, 19 },
                    { 8, 20 },
                    { 9, 5 },
                    { 9, 16 },
                    { 9, 18 },
                    { 10, 1 },
                    { 10, 11 },
                    { 10, 12 },
                    { 10, 19 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_ContactNumber",
                table: "Candidates",
                column: "ContactNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Email",
                table: "Candidates",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkills_SkillId",
                table: "CandidateSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Name",
                table: "Skills",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateSkills");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
