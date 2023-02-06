using BlackJack.Dominio.Jogos.Entidades;

namespace BlackJack.Infra.Jogos.Repositorios.Consultas
{
    public class JogadasConsulta
    {
        public int IdCarta { get; set; }
        public string DescricaoCarta { get; set; }
        public int ValorCarta { get; set; }
        public int IdNipe { get; set; }
        public string DescricaoNipe { get; set; }
        public bool IdtDealer { get; set; }
        public bool Encerrado { get; set; }
    }
}
