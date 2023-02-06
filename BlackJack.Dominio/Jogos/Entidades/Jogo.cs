namespace BlackJack.Dominio.Jogos.Entidades
{
    public class Jogo
    {
        public virtual IList<Carta> CartasDealer { get; protected set; }
        public virtual IList<Carta> CartasJogador { get; protected set; }
        public virtual string Resultado { get; protected set; }
        public virtual int IdJogo { get; protected set; }

        protected Jogo() { }

        public Jogo(IList<Carta> cartasDealer, IList<Carta> cartasJogador, string resultado, int idJogo)
        {
            SetCartasDealer(cartasDealer);
            SetCartasJogador(cartasJogador);
            SetResultado(resultado);
            SetIdJogo(idJogo);
        }

        public virtual void SetCartasDealer(IList<Carta> cartasDealer)
        {
            CartasDealer = cartasDealer;
        }
        public virtual void SetCartasJogador(IList<Carta> cartasJogador)
        {
            CartasJogador = cartasJogador;
        }
        public virtual void SetResultado(string resultado)
        {
            Resultado = resultado;
        }
        public virtual void SetIdJogo(int idJogo)
        {
            IdJogo = idJogo;
        }
    }
}
