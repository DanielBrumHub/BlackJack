using BlackJack.Dominio.Jogos.Entidades;

namespace BlackJack.Dominio.Jogos.Servicos.Interfaces
{
    public interface IJogosServico
    {
        Jogo IniciarJogo(string nomeJogador);
        Jogo ContinuarJogo(int idJogo);
        Jogo FinalizarJogo(int idJogo);
        IList<Carta> DistribuirCartasRodada(int quantidade, int idJogo, bool dealer);
    }
}
