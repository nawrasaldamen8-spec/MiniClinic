using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniClinicManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditEntitiesToCreatePersonFirstNameLastName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorProfiles_AspNetUsers_ApplicationUserId",
                table: "DoctorProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientProfiles_AspNetUsers_ApplicationUserId",
                table: "PatientProfiles");

            migrationBuilder.DropIndex(
                name: "IX_PatientProfiles_ApplicationUserId",
                table: "PatientProfiles");

            migrationBuilder.DropIndex(
                name: "IX_DoctorProfiles_ApplicationUserId",
                table: "DoctorProfiles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "PatientProfiles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "DoctorProfiles");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "PatientProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "DoctorProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientProfiles_PersonId",
                table: "PatientProfiles",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_PersonId",
                table: "DoctorProfiles",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ApplicationUserId",
                table: "Persons",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorProfiles_Persons_PersonId",
                table: "DoctorProfiles",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientProfiles_Persons_PersonId",
                table: "PatientProfiles",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorProfiles_Persons_PersonId",
                table: "DoctorProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientProfiles_Persons_PersonId",
                table: "PatientProfiles");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_PatientProfiles_PersonId",
                table: "PatientProfiles");

            migrationBuilder.DropIndex(
                name: "IX_DoctorProfiles_PersonId",
                table: "DoctorProfiles");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "PatientProfiles");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "DoctorProfiles");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "PatientProfiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "DoctorProfiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProfiles_ApplicationUserId",
                table: "PatientProfiles",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_ApplicationUserId",
                table: "DoctorProfiles",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorProfiles_AspNetUsers_ApplicationUserId",
                table: "DoctorProfiles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientProfiles_AspNetUsers_ApplicationUserId",
                table: "PatientProfiles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
