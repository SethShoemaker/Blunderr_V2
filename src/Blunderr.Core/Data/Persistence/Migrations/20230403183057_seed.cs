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
                    { 1, "Sally@Client.com", "Sally Client", "��onuϲ�$����t`\r�D", "1803046854", 1234567890, 0 },
                    { 2, "James@Client.com", "James Client", "���5b�J�<��{�!g�", "401217990", 1234567890, 0 },
                    { 3, "Thomas@Client.com", "Thomas Client", "QfǸ�}�{�f\r�<Q'~��", "66316628", 1234567890, 0 },
                    { 4, "Lucas@Client.com", "Lucas Client", "���n���|i\r�|�D1�tSH", "264547270", 1234567890, 0 },
                    { 5, "Jamie@LoremIpsum.com", "Jamie Dev", "g(L�Φ�ɝ���SP�ݜ;", "171349475", 1234567890, 1 },
                    { 6, "Sam@LoremIpsum.com", "Sam Dev", "kdG�gTw@�����AhD", "399332407", 1234567890, 1 },
                    { 7, "Jonathan@LoremIpsum.com", "Jonathan Manager", "�����wlT�~�G�L%�zY�?", "223294836", 1234567890, 2 },
                    { 8, "Gerald@LoremIpsum.com", "Gerald Manager", "T�����b��UӼ�a{�b�", "2124259902", 1234567890, 2 }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "ClientId", "Created", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 4, 3, 14, 30, 56, 911, DateTimeKind.Local).AddTicks(6920), "Sally's Website" },
                    { 2, 1, new DateTime(2023, 4, 3, 14, 30, 56, 911, DateTimeKind.Local).AddTicks(6970), "Sally's Mobile App" },
                    { 3, 2, new DateTime(2023, 4, 3, 14, 30, 56, 911, DateTimeKind.Local).AddTicks(6990), "James' Website" },
                    { 4, 2, new DateTime(2023, 4, 3, 14, 30, 56, 911, DateTimeKind.Local).AddTicks(7000), "James' Mobile App" },
                    { 5, 3, new DateTime(2023, 4, 3, 14, 30, 56, 911, DateTimeKind.Local).AddTicks(7010), "Thomas' Website" },
                    { 6, 3, new DateTime(2023, 4, 3, 14, 30, 56, 911, DateTimeKind.Local).AddTicks(7020), "Thomas' Mobile App" },
                    { 7, 4, new DateTime(2023, 4, 3, 14, 30, 56, 911, DateTimeKind.Local).AddTicks(7020), "Lucas' Website" },
                    { 8, 4, new DateTime(2023, 4, 3, 14, 30, 56, 911, DateTimeKind.Local).AddTicks(7030), "Lucas' Mobile App" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Created", "Description", "DeveloperId", "Priority", "ProjectId", "Status", "SubmitterId", "Title", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 3, 14, 30, 56, 911, DateTimeKind.Local).AddTicks(7040), "I need help", null, 0, 1, 0, 1, "Ticket", 2 },
                    { 2, new DateTime(2023, 4, 3, 14, 30, 56, 911, DateTimeKind.Local).AddTicks(7050), "I need more help", null, 1, 1, 0, 1, "Ticket 2", 0 },
                    { 3, new DateTime(2023, 4, 3, 14, 30, 56, 911, DateTimeKind.Local).AddTicks(7050), "Could you build this?", null, 0, 1, 0, 1, "Ticket 3", 1 }
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
