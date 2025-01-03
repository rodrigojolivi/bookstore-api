using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assunto",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "VARCHAR(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assunto", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(40)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "VARCHAR(40)", nullable: true),
                    Editora = table.Column<string>(type: "VARCHAR(40)", nullable: true),
                    Edicao = table.Column<int>(type: "int", nullable: false),
                    AnoPublicacao = table.Column<string>(type: "VARCHAR(4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Livro_Assunto",
                columns: table => new
                {
                    CodigoLivro = table.Column<int>(type: "int", nullable: false),
                    CodigoAssunto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro_Assunto", x => new { x.CodigoLivro, x.CodigoAssunto });
                    table.ForeignKey(
                        name: "FK_Livro_Assunto_Assunto_CodigoAssunto",
                        column: x => x.CodigoAssunto,
                        principalTable: "Assunto",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Assunto_Livro_CodigoLivro",
                        column: x => x.CodigoLivro,
                        principalTable: "Livro",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Livro_Autor",
                columns: table => new
                {
                    CodigoLivro = table.Column<int>(type: "int", nullable: false),
                    CodigoAutor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro_Autor", x => new { x.CodigoLivro, x.CodigoAutor });
                    table.ForeignKey(
                        name: "FK_Livro_Autor_Autor_CodigoAutor",
                        column: x => x.CodigoAutor,
                        principalTable: "Autor",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Autor_Livro_CodigoLivro",
                        column: x => x.CodigoLivro,
                        principalTable: "Livro",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Assunto_CodigoAssunto",
                table: "Livro_Assunto",
                column: "CodigoAssunto");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Autor_CodigoAutor",
                table: "Livro_Autor",
                column: "CodigoAutor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livro_Assunto");

            migrationBuilder.DropTable(
                name: "Livro_Autor");

            migrationBuilder.DropTable(
                name: "Assunto");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Livro");
        }
    }
}
