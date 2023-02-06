namespace BlackJack.Dominio.Jogos.Entidades
{
    public class Carta
    {
        public virtual int Id { get; protected set; }
        public virtual string Descricao { get; protected set; }
        public virtual Nipe Nipe { get; protected set; }
        public virtual int Valor { get; protected set; }

        protected Carta() { }

        public Carta(string descricao, Nipe nipe, int valor)
        {
            SetDescricao(descricao);
            SetNipe(nipe);
            SetValor(valor);
        }

        public virtual void SetDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public virtual void SetNipe(Nipe nipe)
        {
            Nipe = nipe;
        }
        public virtual void SetValor(int valor)
        {
            Valor = valor;
        }
    }
}
