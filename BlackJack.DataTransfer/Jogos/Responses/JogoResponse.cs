namespace BlackJack.DataTransfer.Jogos.Responses
{
    public class JogoResponse
    {
        public IList<CartaResponse> CartasDealer { get; set; }
        public IList<CartaResponse> CartasJogador { get; set; }
        public int PontuacaoDealer { get; set; }
        public int PontuacaoJogador { get; set; }
        public string Resultado { get; set; }
    }
}
