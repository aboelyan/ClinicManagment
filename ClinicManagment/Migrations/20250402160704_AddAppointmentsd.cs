using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagment.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentsd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Appointments_AppointmentId",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Appointments",
                newName: "AppointmentDate");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Appointments",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Appointments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "Appointments",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "EndTime", "Notes", "StartTime", "Status" },
                values: new object[] { new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), new TimeSpan(0, 10, 30, 0, 0), null, new TimeSpan(0, 10, 0, 0, 0), "Scheduled" });

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AppointmentDate", "EndTime", "Notes", "StartTime", "Status" },
                values: new object[] { new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Local), new TimeSpan(0, 14, 45, 0, 0), null, new TimeSpan(0, 14, 0, 0, 0), "Scheduled" });

            migrationBuilder.UpdateData(
                table: "Clinics",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastSync",
                value: new DateTime(2025, 4, 2, 18, 7, 2, 413, DateTimeKind.Local).AddTicks(8783));

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 4, 1, 18, 7, 2, 413, DateTimeKind.Local).AddTicks(9413));

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 4, 2, 18, 7, 2, 413, DateTimeKind.Local).AddTicks(9419));

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Appointments_AppointmentId",
                table: "Prescriptions",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Appointments_AppointmentId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "AppointmentDate",
                table: "Appointments",
                newName: "Date");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Status" },
                values: new object[] { new DateTime(2025, 4, 3, 17, 35, 8, 758, DateTimeKind.Local).AddTicks(418), "قادم" });

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "Status" },
                values: new object[] { new DateTime(2025, 4, 4, 17, 35, 8, 758, DateTimeKind.Local).AddTicks(429), "قادم" });

            migrationBuilder.UpdateData(
                table: "Clinics",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastSync",
                value: new DateTime(2025, 4, 2, 17, 35, 8, 757, DateTimeKind.Local).AddTicks(9967));

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 4, 1, 17, 35, 8, 758, DateTimeKind.Local).AddTicks(467));

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 4, 2, 17, 35, 8, 758, DateTimeKind.Local).AddTicks(474));

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Appointments_AppointmentId",
                table: "Prescriptions",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
