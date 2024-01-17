using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETS.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                schema: "dbo",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                schema: "dbo",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                schema: "dbo",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                schema: "dbo",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "Payment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                schema: "dbo",
                table: "Payment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                schema: "dbo",
                table: "Payment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                schema: "dbo",
                table: "ExpenseCategory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "ExpenseCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "ExpenseCategory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                schema: "dbo",
                table: "ExpenseCategory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                schema: "dbo",
                table: "ExpenseCategory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                schema: "dbo",
                table: "Expense",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "Expense",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "Expense",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                schema: "dbo",
                table: "Expense",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                schema: "dbo",
                table: "Expense",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertDate",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "InsertDate",
                schema: "dbo",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                schema: "dbo",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                schema: "dbo",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "InsertDate",
                schema: "dbo",
                table: "ExpenseCategory");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "ExpenseCategory");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "ExpenseCategory");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                schema: "dbo",
                table: "ExpenseCategory");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                schema: "dbo",
                table: "ExpenseCategory");

            migrationBuilder.DropColumn(
                name: "InsertDate",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                schema: "dbo",
                table: "Expense");
        }
    }
}
