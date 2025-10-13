using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace sepending.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_budgets_categories_CategoryId",
                table: "budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_categories_CategoryId",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "budgets");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "budgets");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "budgets");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "transactions",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "transactions",
                newName: "note");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "transactions",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "transactions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "transactions",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                table: "transactions",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "transactions",
                newName: "category_id");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_CategoryId",
                table: "transactions",
                newName: "IX_transactions_category_id");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "categories",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "categories",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "categories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "categories",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "categories",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Period",
                table: "budgets",
                newName: "period");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "budgets",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "budgets",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "budgets",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "budgets",
                newName: "category_id");

            migrationBuilder.RenameIndex(
                name: "IX_budgets_CategoryId",
                table: "budgets",
                newName: "IX_budgets_category_id");

            migrationBuilder.AddColumn<DateTime>(
                name: "transaction_date",
                table: "transactions",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "period",
                table: "budgets",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "budgets",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "budgets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "end_date",
                table: "budgets",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "start_date",
                table: "budgets",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false, defaultValue: "USER"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    from_user_id = table.Column<int>(type: "integer", nullable: false),
                    to_user_id = table.Column<int>(type: "integer", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "sent"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_messages_users_from_user_id",
                        column: x => x.from_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_messages_users_to_user_id",
                        column: x => x.to_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_messages_from_user_id",
                table: "messages",
                column: "from_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_to_user_id",
                table: "messages",
                column: "to_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_budgets_categories_category_id",
                table: "budgets",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_categories_category_id",
                table: "transactions",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_budgets_categories_category_id",
                table: "budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_categories_category_id",
                table: "transactions");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropColumn(
                name: "transaction_date",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "budgets");

            migrationBuilder.DropColumn(
                name: "end_date",
                table: "budgets");

            migrationBuilder.DropColumn(
                name: "start_date",
                table: "budgets");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "transactions",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "note",
                table: "transactions",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "transactions",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "transactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "transactions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "transactions",
                newName: "TransactionDate");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "transactions",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_category_id",
                table: "transactions",
                newName: "IX_transactions_CategoryId");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "categories",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "categories",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "categories",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "period",
                table: "budgets",
                newName: "Period");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "budgets",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "budgets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "budgets",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "budgets",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_budgets_category_id",
                table: "budgets",
                newName: "IX_budgets_CategoryId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "transactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Period",
                table: "budgets",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "budgets",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "budgets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "budgets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "budgets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_budgets_categories_CategoryId",
                table: "budgets",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_categories_CategoryId",
                table: "transactions",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
