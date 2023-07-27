using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeepIn.Chatting.Infrastructure.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "chatting");

            migrationBuilder.CreateTable(
                name: "chat",
                schema: "chatting",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    avatar_id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    link = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_by = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_private = table.Column<bool>(type: "boolean", nullable: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_member",
                schema: "chatting",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    chat_id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    user_id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    alias = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    is_admin = table.Column<bool>(type: "boolean", nullable: false),
                    is_owner = table.Column<bool>(type: "boolean", nullable: false),
                    is_bot = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_member", x => x.id);
                    table.ForeignKey(
                        name: "FK_chat_member_chat_chat_id",
                        column: x => x.chat_id,
                        principalSchema: "chatting",
                        principalTable: "chat",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_chat_member_chat_id",
                schema: "chatting",
                table: "chat_member",
                column: "chat_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chat_member",
                schema: "chatting");

            migrationBuilder.DropTable(
                name: "chat",
                schema: "chatting");
        }
    }
}
