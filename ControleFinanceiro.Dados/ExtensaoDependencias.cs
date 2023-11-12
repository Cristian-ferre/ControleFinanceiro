using ControleFinanceiro.Dominio.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ControleFinanceiro.Dados.Repositories;

namespace ControleFinanceiro.Dados
{
    public static class ExtensionsDependencias
    {
        //Organização na Injeção de depedência em uma classe separada, ou seja, fora da program
        //Metodo de extensão:
        public static IServiceCollection adicionarDependecias(this IServiceCollection services)
        {
            //configurando a injeção de dependência para associar a interface IRepositoryReceita à implementação concreta RepositoryReceita

            services.AddScoped<IRepositoryReceita, RepositoryReceita>();

            return services;
        }

    }
}
