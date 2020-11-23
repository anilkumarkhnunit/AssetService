using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetRepository.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsfixIncome = table.Column<bool>(type: "bit", nullable: false),
                    IsConvertible = table.Column<bool>(type: "bit", nullable: false),
                    IsSwap = table.Column<bool>(type: "bit", nullable: false),
                    IsCash = table.Column<bool>(type: "bit", nullable: false),
                    IsFuture = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetId);
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "AssetId", "IsCash", "IsConvertible", "IsFuture", "IsSwap", "IsfixIncome", "Name" },
                values: new object[,]
                {
                    { 1, true, false, false, false, false, "Asset 1" },
                    { 2, true, false, false, false, false, "Asset 2" },
                    { 3, false, true, false, false, false, "Asset 3" },
                    { 4, false, false, false, false, true, "Asset 4" },
                    { 5, false, true, false, false, false, "Asset 5" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
