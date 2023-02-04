namespace BlackJack.Dominio.Jogos.Entidades
{
    public class Nipe
    {
        public virtual int Id { get; protected set; }
        public virtual string Descricao { get; protected set; }

        protected Nipe() { }

        public Nipe(string descricao)
        {
            SetDescricao(descricao);
        }

        public virtual void SetDescricao(string descricao)
        {
            Descricao = descricao;
        }
    }
}
