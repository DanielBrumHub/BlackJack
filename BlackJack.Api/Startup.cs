using BlackJack.Aplicacao.Jogos.Servicos;
using BlackJack.Aplicacao.Jogos.Servicos.Interfaces;
using BlackJack.Dominio.Jogos.Repositorios;
using BlackJack.Dominio.Jogos.Servicos;
using BlackJack.Dominio.Jogos.Servicos.Interfaces;
using BlackJack.Infra.Jogos.Repositorios;
using BlackJack.Infra.Uteis;
using BlackJack.Infra.Uteis.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BlackJack.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            // Add services to the container.

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
             {
                 options.SwaggerDoc("v1", new OpenApiInfo{});

                 var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                 options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
             });
            services.AddScoped<IJogosAppServico, JogosAppServico>();
            services.AddScoped<IJogosServico, JogosServico>();
            services.AddScoped<IJogosRepositorio, JogosRepositorio>();
            services.AddScoped<ICartasRepositorio, CartasRepositorio>();
            services.AddScoped<IConexaoBanco, ConexaoBanco>();
        }
        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
