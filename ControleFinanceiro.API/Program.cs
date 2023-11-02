using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.Dados.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ControleFinanceiroDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ControleFinanceiroDbConnection")));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
            Url = new Uri("https://www.linkedin.com/in/cristianferreiradeoliveira/")
        }
    });
    var xmlFile = "ControleFinanceiro.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
}

    );



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
