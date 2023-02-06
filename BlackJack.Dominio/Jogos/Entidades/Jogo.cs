namespace BlackJack.Dominio.Jogos.Entidades
{
    public class Jogo
    {
        public virtual IList<Carta> CartasDealer { get; protected set; }
        public virtual IList<Carta> CartasJogador { get; protected set; }
        public virtual int PontuacaoDealer { get; protected set; }
        public virtual int PontuacaoJogador { get; protected set; }
        public virtual string Resultado { get; protected set; }

        protected Jogo() { }

        public Jogo(IList<Carta> cartasDealer, IList<Carta> cartasJogador, int pontuacaoDealer, int pontuacaoJogador, string resultado)
        {
            SetCartasDealer(cartasDealer);
            SetCartasJogador(cartasJogador);
            SetPontuacaoDealer(pontuacaoDealer);
            SetPontuacaoJogador(pontuacaoJogador);
            SetResultado(resultado);
        }

        public virtual void SetCartasDealer(IList<Carta> cartasDealer)
        {
            CartasDealer = cartasDealer;
        }
        public virtual void SetCartasJogador(IList<Carta> cartasJogador)
        {
            CartasJogador = cartasJogador;
        }
        public virtual void SetPontuacaoDealer(int pontuacaoDealer)
        {
            PontuacaoDealer = pontuacaoDealer;
        }
        public virtual void SetPontuacaoJogador(int pontuacaoJogador)
        {
            PontuacaoJogador = pontuacaoJogador;
        }
        public virtual void SetResultado(string resultado)
        {
            Resultado = resultado;
        }
    }
}
