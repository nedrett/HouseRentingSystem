using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class HouseIsAvtiveAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Houses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "e58e6ecc-ea43-4e6b-a6d7-c68735602abd", "AQAAAAEAACcQAAAAEHjOWpCH2x4+ZonrIwUjfajrKHdn08als3hZwVyytBRNdiZ1Z53PkmtigvZRTfAVXQ==", "a8977780-9ab9-4d62-b17d-b58b67007ac4" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "5e783f6b-6013-46d2-b940-56c24baa20b2", "AQAAAAEAACcQAAAAEJpzFOCiNxgoWjbOcQhmN5SaqVwknG1aqPaZjLqyNsTzvV2cBL7qsoJEQWAjNFvaLg==", "524571b4-f0bc-4cd1-a224-14ad2990849c" });

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Houses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d665080-e535-4a10-a0e9-f0235fd7606d", "AQAAAAEAACcQAAAAENtMYT4rQK912JD5NlYxq9OoIJngaOpv+/C9cmSDs7LXQ0rWNfQq9T0ao79npSxTBw==", "59c942b3-cc14-483c-a628-08a69c57590c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35a2f5c5-5651-449d-9768-48d2bf20becd", "AQAAAAEAACcQAAAAEL+izeNnRAakeouw9s1X0deGBjNpIzlsHrxuLhCL1p/7O9aztFFSba4gbmB2PANFfQ==", "ea8abea2-a6d8-45fc-aa9a-19fd5dd61829" });
        }
    }
}
