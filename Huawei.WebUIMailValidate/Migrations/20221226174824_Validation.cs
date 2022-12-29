using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Huawei.WebUIMailValidate.Migrations
{
    /// <inheritdoc />
    public partial class Validation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFile",
                table: "UserFile");

            migrationBuilder.RenameTable(
                name: "UserFile",
                newName: "UserFiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFiles",
                table: "UserFiles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Validation",
                columns: table => new
                {
                    recID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userID = table.Column<int>(type: "integer", nullable: false),
                    mailAddress = table.Column<string>(type: "text", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ResultDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Result = table.Column<string>(type: "text", nullable: true),
                    ResultDescription = table.Column<string>(type: "text", nullable: true),
                    BatchId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validation", x => x.recID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Validation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFiles",
                table: "UserFiles");

            migrationBuilder.RenameTable(
                name: "UserFiles",
                newName: "UserFile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFile",
                table: "UserFile",
                column: "Id");
        }
    }
}
