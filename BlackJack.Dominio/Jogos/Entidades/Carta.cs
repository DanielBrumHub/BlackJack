namespace BlackJack.Dominio.Jogos.Entidades
{
    public class Carta
    {
        public virtual int Id { get; protected set; }
        public virtual string Descricao { get; protected set; }
        public virtual string Nipe { get; protected set; }
        public virtual int Valor { get; protected set; }

        protected Carta() { }

        public Carta(string descricao, string nipe, int valor)
        {
            SetDescricao(descricao);
            SetNipe(nipe);
            SetValores(valor);
        }

        public virtual void SetDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public virtual void SetNipe(string nipe)
        {
            Nipe = nipe;
        }
        public virtual void SetValores(int valor)
        {
            Valor = valor;
        }
    }
}
