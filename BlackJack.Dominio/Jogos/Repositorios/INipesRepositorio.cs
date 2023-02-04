using BlackJack.Dominio.Jogos.Entidades;

namespace BlackJack.Dominio.Jogos.Repositorios
{
    public interface INipesRepositorio
    {
        IEnumerable<Nipe> Listar();
    }
}
