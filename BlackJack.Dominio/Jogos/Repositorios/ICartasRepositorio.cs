using BlackJack.Dominio.Jogos.Entidades;

namespace BlackJack.Dominio.Jogos.Repositorios
{
    public interface ICartasRepositorio
    {
        IList<Carta> RecuperarDisponiveis(int idJogo);
    }
}
