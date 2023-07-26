using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tb_tr_leave_request_leave_types_guid",
                table: "tb_tr_leave_request");

            migrationBuilder.AlterColumn<string>(
                name: "attachment",
                table: "tb_tr_leave_request",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "expired_time",
                table: "tb_m_accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_used",
                table: "tb_m_accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "otp",
                table: "tb_m_accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_leave_request_leave_types_guid",
                table: "tb_tr_leave_request",
                column: "leave_types_guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tb_tr_leave_request_leave_types_guid",
                table: "tb_tr_leave_request");

            migrationBuilder.DropColumn(
                name: "expired_time",
                table: "tb_m_accounts");

            migrationBuilder.DropColumn(
                name: "is_used",
                table: "tb_m_accounts");

            migrationBuilder.DropColumn(
                name: "otp",
                table: "tb_m_accounts");

            migrationBuilder.AlterColumn<byte[]>(
                name: "attachment",
                table: "tb_tr_leave_request",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_leave_request_leave_types_guid",
                table: "tb_tr_leave_request",
                column: "leave_types_guid",
                unique: true);
        }
    }
}
