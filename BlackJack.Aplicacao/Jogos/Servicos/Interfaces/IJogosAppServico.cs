using BlackJack.DataTransfer.Jogos.Requests;
using BlackJack.DataTransfer.Jogos.Responses;

namespace BlackJack.Aplicacao.Jogos.Servicos.Interfaces
{
    public interface IJogosAppServico
    {
        JogoResponse IniciarJogo(string nomeJogador);
        JogoResponse SeguirRodadaJogo(int request);
    }
}
