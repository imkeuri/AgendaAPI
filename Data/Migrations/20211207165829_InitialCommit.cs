using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactoCorreosElectronico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Correo = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: false),
                    Id_Contacto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactoCorreosElectronico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactoCorreosElectronico_Contactos_Id_Contacto",
                        column: x => x.Id_Contacto,
                        principalTable: "Contactos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactoTelefonos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroTelefono = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Id_Contacto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactoTelefonos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactoTelefonos_Contactos_Id_Contacto",
                        column: x => x.Id_Contacto,
                        principalTable: "Contactos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactoCorreosElectronico_Id_Contacto",
                table: "ContactoCorreosElectronico",
                column: "Id_Contacto");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_Cedula",
                table: "Contactos",
                column: "Cedula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactoTelefonos_Id_Contacto",
                table: "ContactoTelefonos",
                column: "Id_Contacto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactoCorreosElectronico");

            migrationBuilder.DropTable(
                name: "ContactoTelefonos");

            migrationBuilder.DropTable(
                name: "Contactos");
        }
    }
}
