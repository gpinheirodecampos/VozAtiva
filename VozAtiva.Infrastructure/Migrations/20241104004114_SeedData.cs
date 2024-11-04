using Microsoft.EntityFrameworkCore.Migrations;
using VozAtiva.Domain.Enums;

#nullable disable

namespace VozAtiva.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "agent_type",
                columns: ["Id", "Description", "Name"],
                values: new object[,]
                {
                    { 1, "Prefeituras Municipais", "Prefeitura" },
                    { 2, "Governos Estaduais", "Estado" },
                    { 3, "Ministérios", "Ministério" },
                    { 4, "Tribunais", "Tribunal" },
                    { 5, "Polícia Federal e Polícia Civil", "Polícia" },
                    { 6, "Agências Reguladoras", "Agência" },
                    { 7, "Instituições Educacionais Públicas", "Educação Pública" },
                    { 8, "Instituições de Saúde Pública", "Saúde Pública" },
                    { 9, "Autarquias", "Autarquia" },
                    { 10, "Corpo de Bombeiros", "Bombeiros" }
                }
            );

            migrationBuilder.InsertData(
                table: "public_agent",
                columns: ["Id", "Name", "Email", "Phone", "Acronym", "AgentTypeId"],
                values: new object[,]
                {
                    { 1, "Prefeitura de São Paulo", "contato@prefeitura.sp.gov.br", "1131111234", "PMSP", 1 },
                    { 2, "Governo do Estado de São Paulo", "contato@estado.sp.gov.br", "1132221234", "GESP", 2 },
                    { 3, "Ministério da Educação", "contato@mec.gov.br", "6133445678", "MEC", 3 },
                    { 4, "Supremo Tribunal Federal", "contato@stf.jus.br", "6135556789", "STF", 4 },
                    { 5, "Polícia Federal", "contato@pf.gov.br", "6136667890", "PF", 5 },
                    { 6, "ANVISA", "contato@anvisa.gov.br", "6137778901", "ANVISA", 6 },
                    { 7, "Universidade Federal de São Paulo", "contato@unifesp.edu.br", "1138889012", null, 7 },
                    { 8, "Hospital das Clínicas de São Paulo", "contato@hcsp.gov.br", "1139990123", "HCSP", 8 },
                    { 9, "INSS - Instituto Nacional do Seguro Social", "contato@inss.gov.br", "6140001234", "INSS", 9 },
                    { 10, "Corpo de Bombeiros de Minas Gerais", "contato@cbmmg.mg.gov.br", "3131112345", "CBMMG", 10 }
                }
            );

            migrationBuilder.InsertData(
                table: "user",
                columns: ["Id", "Name", "Email", "FederalCodeClient", "Birthdate", "Phone", "UserType", "CreatedAt", "UpdatedAt", "Active", "Disabled"],
                values: new object[,]
                {
                    { Guid.NewGuid(), "Alice Santos", "alice.santos@example.com", "12345678901", new DateTime(1990, 5, 12), "11987654321", (int)UserTypeEnum.Admin, DateTime.Now, DateTime.Now, true, false },
                    { Guid.NewGuid(), "Bruno Silva", "bruno.silva@example.com", "10987654321", new DateTime(1985, 7, 23), "11976543210", (int)UserTypeEnum.User, DateTime.Now, DateTime.Now, true, false },
                    { Guid.NewGuid(), "Carla Almeida", "carla.almeida@example.com", "11223344556", new DateTime(1993, 3, 8), "11965432109", (int)UserTypeEnum.User, DateTime.Now, DateTime.Now, true, false },
                    { Guid.NewGuid(), "Diego Ferreira", "diego.ferreira@example.com", "22334455667", new DateTime(1982, 10, 17), "11954321098", (int)UserTypeEnum.Admin, DateTime.Now, DateTime.Now, true, false },
                    { Guid.NewGuid(), "Eva Lima", "eva.lima@example.com", "33445566778", new DateTime(1995, 12, 1), "11943210987", (int)UserTypeEnum.User, DateTime.Now, DateTime.Now, true, false }
                }
            );

            migrationBuilder.InsertData(
                table: "alert_type",
                columns: ["Id", "Name", "Description"],
                values: new object[,]
                {
                    { 1, "Iluminação Pública", "Relatos sobre problemas na iluminação de vias públicas" },
                    { 2, "Condições das Vias", "Relatos sobre problemas de pavimentação e manutenção das vias" },
                    { 3, "Obstrução de Vias", "Relatos sobre vias ou acessos públicos obstruídos" },
                    { 4, "Sinalização", "Relatos sobre problemas com a sinalização de trânsito" },
                    { 5, "Meio Ambiente", "Relatos sobre problemas ambientais em áreas públicas" },
                    { 6, "Infraestrutura Pública", "Relatos sobre danos em infraestruturas públicas, como calçadas" },
                    { 7, "Veículo Abandonado", "Relatos sobre veículos abandonados em áreas públicas" },
                    { 8, "Condições Sanitárias", "Relatos sobre condições sanitárias inadequadas em locais públicos" },
                    { 9, "Vandalismo", "Relatos sobre vandalismo em espaços públicos" },
                    { 10, "Outros", "Relatos sobre outros problemas que afetam áreas públicas" }
                }
            );

            migrationBuilder.InsertData(
                table: "image",
                columns: ["Id", "Url", "Description", "FileName", "CreatedAt", "UpdatedAt", "Active", "Disabled"],
                values: new object[,]
                {
                    { Guid.NewGuid(), "https://example.com/images/poste_sem_luz.jpg", "Imagem de um poste sem luz", "poste_sem_luz.jpg", DateTime.Now, DateTime.Now, true, false },
                    { Guid.NewGuid(), "https://example.com/images/rua_esburacada.jpg", "Imagem de uma rua com buracos", "rua_esburacada.jpg", DateTime.Now, DateTime.Now, true, false },
                    { Guid.NewGuid(), "https://example.com/images/entulho_via_publica.jpg", "Imagem de entulho descartado em via pública", "entulho_via_publica.jpg", DateTime.Now, DateTime.Now, true, false },
                    { Guid.NewGuid(), "https://example.com/images/sinalizacao_danificada.jpg", "Imagem de sinalização de trânsito danificada", "sinalizacao_danificada.jpg", DateTime.Now, DateTime.Now, true, false },
                    { Guid.NewGuid(), "https://example.com/images/calçada_danificada.jpg", "Imagem de uma calçada danificada", "calcada_danificada.jpg", DateTime.Now, DateTime.Now, true, false }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
