namespace BlackJack.Dominio.Jogos.Entidades
{
    public class Jogo
    {
        public virtual IList<Carta> CartasDealer { get; protected set; }
        public virtual IList<Carta> CartasJogador { get; protected set; }

        protected Jogo() { }

        public Jogo(IList<Carta> cartasDealer, IList<Carta> cartasJogador)
        {
            SetCartasDealer(cartasDealer);
            SetCartasJogador(cartasJogador);
        }

        public virtual void SetCartasDealer(IList<Carta> cartasDealer)
        {
            CartasDealer = cartasDealer;
        }
        public virtual void SetCartasJogador(IList<Carta> cartasJogador)
        {
            CartasJogador = cartasJogador;
        }
    }
}
