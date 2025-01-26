using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using VozAtiva.Domain.Enums;
using VozAtiva.Infrastructure.Context;

#nullable disable

namespace VozAtiva.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PopulateDBWithAlerts : Migration
    {
        /// <inheritdoc />
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            
                migrationBuilder.InsertData(
                    table: "alert",
                    columns: ["Id","Title", "Description", "Date","UserId", "PublicAgentId", "AlertTypeId", "Status", "Latitude", "Longitude"],
                    values: new object[,]
                    {
                        {Guid.NewGuid(), "Buraco na Via", "Atenção: buraco no meio da faixa", new DateTime(2025, 1, 13), Guid.Parse("11111111-1111-1111-1111-111111111111") ,1 , 1 , 1, -23.231618, -45.885205},
                        {Guid.NewGuid(), "Carro Abandonado", "Veículo sem placa estacionado há dias", new DateTime(2024, 2, 2), Guid.Parse("55555555-5555-5555-5555-555555555555") ,1 , 2 ,1, -23.237842, -45.925440 },
                        {Guid.NewGuid(), "Deslizamento de Terra", "Pequenos desmoronamentos na encosta", new DateTime(2024, 1, 20),Guid.Parse("11111111-1111-1111-1111-111111111111"), 1 , 3 ,1, -23.289224, -45.895970 },
                        {Guid.NewGuid(), "Sinal Quebrado", "Semáforo inoperante", new DateTime(2024, 12, 19), Guid.Parse("55555555-5555-5555-5555-555555555555"), 4 , 3 ,1, -23.206851, -45.854230 },
                        {Guid.NewGuid(), "Rua Alagada", "Transbordamento do córrego próximo", new DateTime(2024, 12, 22),Guid.Parse("44444444-4444-4444-4444-444444444444"), 4 , 3 ,1, -23.130757, -45.767213 },
                        {Guid.NewGuid(), "Furto a Residência", "Moradores reportaram invasão na madrugada", new DateTime(2024, 12, 21),Guid.Parse("11111111-1111-1111-1111-111111111111"), 5 , 5 ,1, -23.187616, -45.791950 },
                        {Guid.NewGuid(), "Congestionamento", "Engarrafamento devido a evento na região", new DateTime(2024, 12, 18), Guid.Parse("33333333-3333-3333-3333-333333333333") ,5 , 5 ,1, -23.213113, -45.808559 },
                        {Guid.NewGuid(), "Árvore Caída", "Bloqueio parcial da rua", new DateTime(2024, 12, 18), Guid.Parse("33333333-3333-3333-3333-333333333333") ,1 , 6 ,1, -23.186411, -45.884732 },
                        {Guid.NewGuid(), "Vandalismo", "Pichações em muros e postes", new DateTime(2024, 12, 19), Guid.Parse("33333333-3333-3333-3333-333333333333"), 1 , 7 ,1, -23.192214, -45.879301 },
                        {Guid.NewGuid(), "Carro Quebrado", "Veículo parado em faixa central", new DateTime(2024, 12, 23),Guid.Parse("11111111-1111-1111-1111-111111111111"), 3 , 4 ,1, -23.189632, -45.890222 },
                        {Guid.NewGuid(), "Lixo Acumulado", "Entulho descartado irregularmente", new DateTime(2024, 12, 24), Guid.Parse("11111111-1111-1111-1111-111111111111") ,1 , 7 ,1, -23.194332, -45.882100 },
                        {Guid.NewGuid(), "Buraco Emergencial", "Afundamento de asfalto", new DateTime(2024, 12, 24), Guid.Parse("11111111-1111-1111-1111-111111111111") ,10 , 1 ,1, -23.234808, -45.849580 },
                        {Guid.NewGuid(), "Roubo de Veículo", "Carro levado próximo a mercado", new DateTime(2024, 12, 20), Guid.Parse("33333333-3333-3333-3333-333333333333") ,10 , 7 ,1, -23.191212, -45.887312 },
                        {Guid.NewGuid(), "Pequeno Alagamento", "Pontos de água acumulada na via", new DateTime(2024, 12, 20), Guid.Parse("33333333-3333-3333-3333-333333333333") ,7 , 10 ,1, -23.260123, -45.949444},
                        {Guid.NewGuid(), "Batida entre Motos", "Acidente sem feridos", new DateTime(2024, 12, 18), Guid.Parse("11111111-1111-1111-1111-111111111111"), 1 , 1 ,1, -23.192801, -45.885132 },
                        {Guid.NewGuid(), "Luz Intermitente", "Poste com luz piscando", new DateTime(2024, 12, 21), Guid.Parse("55555555-5555-5555-5555-555555555555") ,2 , 3 ,1, -23.195420, -45.890990 },
                        {Guid.NewGuid(), "Tentativa de Assalto", "Suspeito fugiu após abordagem", new DateTime(2024, 12, 22), Guid.Parse("22222222-2222-2222-2222-222222222222"), 8 , 9 ,1, -23.256665, -45.922619 },
                        {Guid.NewGuid(), "Trânsito Intenso", "Devido a obras na pista", new DateTime(2024, 12, 22), Guid.Parse("55555555-5555-5555-5555-555555555555"), 1 , 1 ,1, -23.188941, -45.894102 },
                        {Guid.NewGuid(), "Desmoronamento", "Área interditada pela Defesa Civil", new DateTime(2024, 12, 23), Guid.Parse("22222222-2222-2222-2222-222222222222"), 6 , 10 ,1, -23.242167, -45.901741 },
                        {Guid.NewGuid(), "Furto a Comércio", "Loja invadida durante a madrugada", new DateTime(2024, 12, 25), Guid.Parse("44444444-4444-4444-4444-444444444444") ,5 , 7 ,1, -23.237929, -45.880984 },
                        {Guid.NewGuid(), "Congestionamento", "Sinalização prejudicada", new DateTime(2024, 12, 24), Guid.Parse("55555555-5555-5555-5555-555555555555") ,5 , 8 ,1, -23.191123, -45.883900 },
                        {Guid.NewGuid(), "Queda de Energia", "Bairro sem luz desde cedo", new DateTime(2024, 12, 23), Guid.Parse("22222222-2222-2222-2222-222222222222") ,7 , 8 ,1, -23.198000, -45.879900 },
                        {Guid.NewGuid(), "Furto de Celular", "Crime registrado em praça pública", new DateTime(2024, 12, 22), Guid.Parse("33333333-3333-3333-3333-333333333333") ,1 , 1 ,1, -23.223205, -45.893851 },
                        {Guid.NewGuid(), "Trânsito Bloqueado", "Caminhão quebrado ocupa faixa", new DateTime(2024, 12, 20), Guid.Parse("33333333-3333-3333-3333-333333333333") ,1 , 1 ,1, -23.256769, -45.889465 },
                    }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
