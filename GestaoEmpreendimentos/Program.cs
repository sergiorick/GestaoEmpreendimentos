using GestaoEmpreendimentos.Domain.Interfaces;
using GestaoEmpreendimentos.Infrastructure.Data;
using GestaoEmpreendimentos.Infrastructure.Repositories;
using GestaoEmpreendimentos.Application.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura o Kestrel para escutar na porta 8080 (padrão usado dentro de containers Linux)
/*builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);cd
});
*/
const string AngularOrigin = "AngularOrigin";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AngularOrigin,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEmpreendimentoRepository, EmpreendimentoRepository>();
builder.Services.AddScoped<IEmpreendimentoService, EmpreendimentoService>();

var app = builder.Build();

// Aplica migrations automaticamente ao iniciar (útil em containers, onde não há "dotnet ef" manual)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(AngularOrigin);

app.UseAuthorization();

app.MapControllers();

app.Run();