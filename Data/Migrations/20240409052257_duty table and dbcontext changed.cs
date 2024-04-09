using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class dutytableanddbcontextchanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Duty_Nurses_NurseId",
                table: "Duty");

            migrationBuilder.DropColumn(
                name: "IsDutyAssigned",
                table: "Duty");

            migrationBuilder.AddColumn<DateTime>(
                name: "AdmittedTime",
                table: "Duty",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Duty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Duty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Duty_DoctorId",
                table: "Duty",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Duty_PatientId",
                table: "Duty",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Duty_Doctors_DoctorId",
                table: "Duty",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Duty_Nurses_NurseId",
                table: "Duty",
                column: "NurseId",
                principalTable: "Nurses",
                principalColumn: "NurseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Duty_Patients_PatientId",
                table: "Duty",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Duty_Doctors_DoctorId",
                table: "Duty");

            migrationBuilder.DropForeignKey(
                name: "FK_Duty_Nurses_NurseId",
                table: "Duty");

            migrationBuilder.DropForeignKey(
                name: "FK_Duty_Patients_PatientId",
                table: "Duty");

            migrationBuilder.DropIndex(
                name: "IX_Duty_DoctorId",
                table: "Duty");

            migrationBuilder.DropIndex(
                name: "IX_Duty_PatientId",
                table: "Duty");

            migrationBuilder.DropColumn(
                name: "AdmittedTime",
                table: "Duty");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Duty");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Duty");

            migrationBuilder.AddColumn<bool>(
                name: "IsDutyAssigned",
                table: "Duty",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Duty_Nurses_NurseId",
                table: "Duty",
                column: "NurseId",
                principalTable: "Nurses",
                principalColumn: "NurseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
