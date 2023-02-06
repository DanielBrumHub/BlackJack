using BlackJack.Dominio.Jogos.Entidades;
using BlackJack.Infra.Jogos.Repositorios.Consultas;

namespace BlackJack.Dominio.Jogos.Repositorios
{
    public interface IJogosRepositorio
    {
        int Inserir(string nomeJogador);
        void InserirJogada(Carta carta, int idJogo, bool dealer);
        void EncerrarJogo(int idJogo);
        IList<JogadasConsulta> RecuperarJogadas(int idJogo);
    }
}
