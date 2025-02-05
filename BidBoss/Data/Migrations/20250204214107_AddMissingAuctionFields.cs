using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BidBoss.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingAuctionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Auctions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Auctions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumBidIncrement",
                table: "Auctions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ReservePrice",
                table: "Auctions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Auctions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "MinimumBidIncrement",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "ReservePrice",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Auctions");
        }
    }
}
