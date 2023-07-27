using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeepIn.Emailing.API.Migrations
{
    /// <inheritdoc />
    public partial class initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "emailing");

            migrationBuilder.CreateTable(
                name: "mail_object",
                schema: "emailing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    From = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    To = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    CC = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    Subject = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    Body = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    IsBodyHtml = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mail_object", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mail_object",
                schema: "emailing");
        }
    }
}
