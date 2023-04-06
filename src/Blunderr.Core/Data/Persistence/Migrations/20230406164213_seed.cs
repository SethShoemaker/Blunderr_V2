using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blunderr.Core.Data.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "PasswordSalt", "Phone", "Role" },
                values: new object[,]
                {
                    { 1, "Sally@Client.com", "Sally Client", "�\"�4K�����f4$�Y�!", "1319448964", 1234567890, 0 },
                    { 2, "James@Client.com", "James Client", "���~�/���O����Ű�ƭ", "1503377391", 1234567890, 0 },
                    { 3, "Thomas@Client.com", "Thomas Client", "��p3����z�))uIi", "2026339243", 1234567890, 0 },
                    { 4, "Lucas@Client.com", "Lucas Client", "m�ۯ�\"�����~QI�����", "373485992", 1234567890, 0 },
                    { 5, "Jamie@LoremIpsum.com", "Jamie Dev", "Ť����С�v��Qw\\", "1456109873", 1234567890, 1 },
                    { 6, "Sam@LoremIpsum.com", "Sam Dev", "/���VZ�%J�]:���dv�k", "231204354", 1234567890, 1 },
                    { 7, "Jonathan@LoremIpsum.com", "Jonathan Manager", "�I����m��j��", "1908393835", 1234567890, 2 },
                    { 8, "Gerald@LoremIpsum.com", "Gerald Manager", "g�����!�wj#��2�?", "1141757280", 1234567890, 2 }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "ClientId", "Created", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 4, 6, 12, 42, 13, 595, DateTimeKind.Local).AddTicks(3730), "Sally's Website" },
                    { 2, 1, new DateTime(2023, 4, 6, 12, 42, 13, 595, DateTimeKind.Local).AddTicks(3780), "Sally's Mobile App" },
                    { 3, 2, new DateTime(2023, 4, 6, 12, 42, 13, 595, DateTimeKind.Local).AddTicks(3790), "James' Website" },
                    { 4, 2, new DateTime(2023, 4, 6, 12, 42, 13, 595, DateTimeKind.Local).AddTicks(3790), "James' Mobile App" },
                    { 5, 3, new DateTime(2023, 4, 6, 12, 42, 13, 595, DateTimeKind.Local).AddTicks(3800), "Thomas' Website" },
                    { 6, 3, new DateTime(2023, 4, 6, 12, 42, 13, 595, DateTimeKind.Local).AddTicks(3810), "Thomas' Mobile App" },
                    { 7, 4, new DateTime(2023, 4, 6, 12, 42, 13, 595, DateTimeKind.Local).AddTicks(3810), "Lucas' Website" },
                    { 8, 4, new DateTime(2023, 4, 6, 12, 42, 13, 595, DateTimeKind.Local).AddTicks(3820), "Lucas' Mobile App" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Created", "Description", "DeveloperId", "Priority", "ProjectId", "Status", "SubmitterId", "Title", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 6, 12, 42, 13, 595, DateTimeKind.Local).AddTicks(3830), "I need help", null, 0, 1, 0, 1, "Ticket", 2 },
                    { 2, new DateTime(2023, 4, 6, 12, 42, 13, 595, DateTimeKind.Local).AddTicks(3840), "I need more help", null, 1, 1, 0, 1, "Ticket 2", 0 },
                    { 3, new DateTime(2023, 4, 6, 12, 42, 13, 595, DateTimeKind.Local).AddTicks(3850), "Could you build this?", null, 0, 1, 0, 1, "Ticket 3", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
