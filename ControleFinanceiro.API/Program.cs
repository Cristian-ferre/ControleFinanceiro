using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.Dados.Context;
using Microsoft.OpenApi.Models;
using ControleFinanceiro.Dados;
using Microsoft.AspNetCore.Hosting;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ControleFinanceiroDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ControleFinanceiroDbConnection")));


//Utilizando um método de extensão para facilitar a organização
builder.Services.adicionarDependecias();


builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // Configurações do Kestrel vão aqui
    serverOptions.ListenAnyIP(5000);
    serverOptions.ListenAnyIP(5001);
    
});



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Adionando Comentarios nos metodos da api no swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ControleFinanceiro.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Cristian Ferreira",
            Email = "cristianferreiradeoliveira.ti@gmail.com",
            Url = new Uri("https://linktr.ee/cristian.ferreira?fbclid=PAAaaFsHJuRyieNtJB1P4UgeeULBhf051-10USj81D1h0HyWGqhppXDF2XnB8")
        }
    });
    var xmlFile = "ControleFinanceiro.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
}
    );

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
