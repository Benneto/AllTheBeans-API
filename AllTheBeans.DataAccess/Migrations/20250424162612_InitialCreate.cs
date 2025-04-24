using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllTheBeans.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeanImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeanImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBeanOfTheDay = table.Column<bool>(type: "bit", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beans_BeanImages_ImageId",
                        column: x => x.ImageId,
                        principalTable: "BeanImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beans_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beans_CountryId",
                table: "Beans",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Beans_ImageId",
                table: "Beans",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Beans_ImportId",
                table: "Beans",
                column: "ImportId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beans");

            migrationBuilder.DropTable(
                name: "BeanImages");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
