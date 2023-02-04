namespace BlackJack.Dominio.Jogos.Entidades
{
    public class Baralho
    {
        public virtual IList<Carta> Cartas { get; protected set; }


        public Baralho() 
        {
            IList<Carta> cartas = new List<Carta> { };

            SetCartas(cartas);
        }

        public Baralho(IList<Carta> cartas)
        {
            SetCartas(cartas);
        }

        public virtual void SetCartas(IList<Carta> cartas)
        {
            Cartas = cartas;
        }
    }
}
