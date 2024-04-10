using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addoptionalusersettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSettings_IFirmaSettings_IFirmaSettingId",
                table: "UserSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSettings_MongoDBSettings_MongoDBSettingId",
                table: "UserSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSettings_PolcarSettings_PolcarSettingId",
                table: "UserSettings");

            migrationBuilder.AlterColumn<int>(
                name: "PolcarSettingId",
                table: "UserSettings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "MongoDBSettingId",
                table: "UserSettings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "IFirmaSettingId",
                table: "UserSettings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettings_IFirmaSettings_IFirmaSettingId",
                table: "UserSettings",
                column: "IFirmaSettingId",
                principalTable: "IFirmaSettings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettings_MongoDBSettings_MongoDBSettingId",
                table: "UserSettings",
                column: "MongoDBSettingId",
                principalTable: "MongoDBSettings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettings_PolcarSettings_PolcarSettingId",
                table: "UserSettings",
                column: "PolcarSettingId",
                principalTable: "PolcarSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSettings_IFirmaSettings_IFirmaSettingId",
                table: "UserSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSettings_MongoDBSettings_MongoDBSettingId",
                table: "UserSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSettings_PolcarSettings_PolcarSettingId",
                table: "UserSettings");

            migrationBuilder.AlterColumn<int>(
                name: "PolcarSettingId",
                table: "UserSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MongoDBSettingId",
                table: "UserSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IFirmaSettingId",
                table: "UserSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettings_IFirmaSettings_IFirmaSettingId",
                table: "UserSettings",
                column: "IFirmaSettingId",
                principalTable: "IFirmaSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettings_MongoDBSettings_MongoDBSettingId",
                table: "UserSettings",
                column: "MongoDBSettingId",
                principalTable: "MongoDBSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettings_PolcarSettings_PolcarSettingId",
                table: "UserSettings",
                column: "PolcarSettingId",
                principalTable: "PolcarSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
