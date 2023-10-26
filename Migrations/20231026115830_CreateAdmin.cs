using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    username = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    password = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.id);
                    table.ForeignKey(
                        name: "FK_admins_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "id", "created_at", "email", "is_active", "password", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { 1, null, "store.admin@bookstore.com", false, "password", 1, null, "admin" },
                    { 2, null, "store.admin2@bookstore.com", false, "password", 1, null, "admin2" },
                    { 3, null, "store.admin3@bookstore.com", false, "password", 1, null, "admin3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_admins_email",
                table: "admins",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_admins_role_id",
                table: "admins",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");
        }
    }
}
