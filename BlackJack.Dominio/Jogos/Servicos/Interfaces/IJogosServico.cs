using BlackJack.Dominio.Jogos.Entidades;

namespace BlackJack.Dominio.Jogos.Servicos.Interfaces
{
    public interface IJogosServico
    {
        Jogo IniciarJogo(string nomeJogador);
        Jogo ContinuarJogo(int idJogo, bool continua);
        IList<Carta> DistribuirCartasRodada(int quantidade, int idJogo, bool dealer);
    }
}
