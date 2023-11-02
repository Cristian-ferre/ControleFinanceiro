using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ControleFinanceiro.API.Controllers.Extensions
{
    public static class SwaggerSetup
    {

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
