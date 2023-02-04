namespace BlackJack.DataTransfer.Jogos.Responses
{
    public class JogoResponse
    {
        public IList<CartaResponse> CartasDealer { get; set; }
        public IList<CartaResponse> CartasJogador { get; set; }
    }
}
