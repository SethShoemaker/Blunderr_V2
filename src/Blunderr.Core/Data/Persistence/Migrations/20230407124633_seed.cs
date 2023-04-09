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
                    { 1, "Sally@Client.com", "Sally Client", "e�5M�\\i�n�.^�9BQ�G", "25947343", 1234567890, 0 },
                    { 2, "James@Client.com", "James Client", "�V,��������z�|!�*", "24759923", 1234567890, 0 },
                    { 3, "Thomas@Client.com", "Thomas Client", "���[���ކ��(WPGL�y", "345533444", 1234567890, 0 },
                    { 4, "Lucas@Client.com", "Lucas Client", "�9\r2�e�����5�K�e�", "394961789", 1234567890, 0 },
                    { 5, "Jamie@LoremIpsum.com", "Jamie Dev", "��|lls�P��T�W�0�", "1320745238", 1234567890, 1 },
                    { 6, "Sam@LoremIpsum.com", "Sam Dev", "`U�F╟{5�f� �\r%�;��", "744166733", 1234567890, 1 },
                    { 7, "Jonathan@LoremIpsum.com", "Jonathan Manager", "e\"�軂k�zPe�>��tpz", "25548138", 1234567890, 2 },
                    { 8, "Gerald@LoremIpsum.com", "Gerald Manager", "�Q�A�8#�L��d(�ʹ0�=", "1863971063", 1234567890, 2 }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "ClientId", "Created", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 4, 7, 8, 46, 33, 689, DateTimeKind.Local).AddTicks(5830), "Sally's Website" },
                    { 2, 1, new DateTime(2023, 4, 7, 8, 46, 33, 689, DateTimeKind.Local).AddTicks(5880), "Sally's Mobile App" },
                    { 3, 2, new DateTime(2023, 4, 7, 8, 46, 33, 689, DateTimeKind.Local).AddTicks(5890), "James' Website" },
                    { 4, 2, new DateTime(2023, 4, 7, 8, 46, 33, 689, DateTimeKind.Local).AddTicks(5900), "James' Mobile App" },
                    { 5, 3, new DateTime(2023, 4, 7, 8, 46, 33, 689, DateTimeKind.Local).AddTicks(5900), "Thomas' Website" },
                    { 6, 3, new DateTime(2023, 4, 7, 8, 46, 33, 689, DateTimeKind.Local).AddTicks(5910), "Thomas' Mobile App" },
                    { 7, 4, new DateTime(2023, 4, 7, 8, 46, 33, 689, DateTimeKind.Local).AddTicks(5920), "Lucas' Website" },
                    { 8, 4, new DateTime(2023, 4, 7, 8, 46, 33, 689, DateTimeKind.Local).AddTicks(5920), "Lucas' Mobile App" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Created", "Description", "DeveloperId", "Priority", "ProjectId", "Status", "SubmitterId", "Title", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 7, 8, 46, 33, 689, DateTimeKind.Local).AddTicks(5940), "I need help", null, 0, 1, 0, 1, "Ticket", 2 },
                    { 2, new DateTime(2023, 4, 7, 8, 46, 33, 689, DateTimeKind.Local).AddTicks(5960), "I need more help", null, 1, 1, 0, 1, "Ticket 2", 0 },
                    { 3, new DateTime(2023, 4, 7, 8, 46, 33, 689, DateTimeKind.Local).AddTicks(5970), "Could you build this?", null, 0, 1, 0, 1, "Ticket 3", 1 }
                });

            migrationBuilder.InsertData(
                table: "TicketComment",
                columns: new[] { "Id", "Content", "Created", "SubmitterId", "TicketId" },
                values: new object[,]
                {
                    { 1, "You need to do X and Y in order for the feature to work", new DateTime(2023, 4, 7, 8, 46, 33, 689, DateTimeKind.Local).AddTicks(5940), 1, 1 },
                    { 2, "Can you do this for me?", new DateTime(2023, 4, 7, 8, 46, 33, 689, DateTimeKind.Local).AddTicks(5950), 1, 1 }
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
                table: "TicketComment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TicketComment",
                keyColumn: "Id",
                keyValue: 2);

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
                table: "Tickets",
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
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
