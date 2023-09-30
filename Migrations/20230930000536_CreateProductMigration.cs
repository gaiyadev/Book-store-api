using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookstoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateProductMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    author = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    product_slug = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    product_url = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    Is_available = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    Book_Genre_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_book_genres_Book_Genre_id",
                        column: x => x.Book_Genre_id,
                        principalTable: "book_genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_Book_Genre_id",
                table: "products",
                column: "Book_Genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_price",
                table: "products",
                column: "price");

            migrationBuilder.CreateIndex(
                name: "IX_products_product_slug",
                table: "products",
                column: "product_slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_user_id",
                table: "products",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
