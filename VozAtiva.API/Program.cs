using Microsoft.EntityFrameworkCore;
using VozAtiva.CrossCutting.IoC;
using VozAtiva.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowExpoClient", policy =>
    {
        policy.WithOrigins("http://localhost:8081") // Replace with your Expo frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureAPI(builder.Configuration);



// Registrando servico Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
//}

app.UseHttpsRedirection();

app.UseCors("AllowExpoClient");

app.UseAuthorization();

app.MapControllers();

app.Run();
