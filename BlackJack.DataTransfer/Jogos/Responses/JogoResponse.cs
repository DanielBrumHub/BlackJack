namespace BlackJack.DataTransfer.Jogos.Responses
{
    public class JogoResponse
    {
        public IList<CartaResponse> CartasDealer { get; set; }
        public IList<CartaResponse> CartasJogador { get; set; }
        public string Resultado { get; set; }
        public int IdJogo { get; set; }
    }
}
