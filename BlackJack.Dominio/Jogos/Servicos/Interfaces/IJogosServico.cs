using BlackJack.Dominio.Jogos.Entidades;

namespace BlackJack.Dominio.Jogos.Servicos.Interfaces
{
    public interface IJogosServico
    {
        Jogo IniciarJogo(string nomeJogador);
        Jogo SeguirRodadaJogo(int idJogo);
        Jogo FinalizarJogo(Jogo jogo);
        Jogo DistribuirCartasRodada(int quantidadeDealer, int quantidadeJogador, int idJogo);
    }
}
