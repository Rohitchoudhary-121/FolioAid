using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_Project_Industry_Technology_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivationString",
                table: "AspNetUser",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortfolioString",
                table: "AspNetUser",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "AspNetUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Industries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Challenges = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Solutions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BannerImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DemoURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AndroidURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppleURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectImageGalleries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Tittle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectImageGalleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectImageGalleries_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectIndustryMappings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    IndustryId = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectIndustryMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectIndustryMappings_Industries_IndustryId",
                        column: x => x.IndustryId,
                        principalTable: "Industries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectIndustryMappings_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectVideoGalleries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Tittle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectVideoGalleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectVideoGalleries_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTechnologyMappings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    TechnologyId = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTechnologyMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTechnologyMappings_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTechnologyMappings_Technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImageGalleries_ProjectId",
                table: "ProjectImageGalleries",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIndustryMappings_IndustryId",
                table: "ProjectIndustryMappings",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIndustryMappings_ProjectId",
                table: "ProjectIndustryMappings",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTechnologyMappings_ProjectId",
                table: "ProjectTechnologyMappings",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTechnologyMappings_TechnologyId",
                table: "ProjectTechnologyMappings",
                column: "TechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectVideoGalleries_ProjectId",
                table: "ProjectVideoGalleries",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectImageGalleries");

            migrationBuilder.DropTable(
                name: "ProjectIndustryMappings");

            migrationBuilder.DropTable(
                name: "ProjectTechnologyMappings");

            migrationBuilder.DropTable(
                name: "ProjectVideoGalleries");

            migrationBuilder.DropTable(
                name: "Industries");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropColumn(
                name: "ActivationString",
                table: "AspNetUser");

            migrationBuilder.DropColumn(
                name: "PortfolioString",
                table: "AspNetUser");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUser");
        }
    }
}
