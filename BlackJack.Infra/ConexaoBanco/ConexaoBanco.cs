using BlackJack.Infra.Uteis.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BlackJack.Infra.Uteis
{
    public class ConexaoBanco : IConexaoBanco
    {
        IConfiguration _configuration;
        public ConexaoBanco(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("Connection").Value;
            return connection;
        }
    }
}
