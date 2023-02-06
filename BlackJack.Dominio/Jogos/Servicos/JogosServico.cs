using BlackJack.Dominio.Jogos.Entidades;
using BlackJack.Dominio.Jogos.Enumeradores;
using BlackJack.Dominio.Jogos.Repositorios;
using BlackJack.Dominio.Jogos.Servicos.Interfaces;
using BlackJack.Infra.Jogos.Repositorios.Consultas;
using NPOI.SS.Formula.Functions;

namespace BlackJack.Dominio.Jogos.Servicos
{
    public class JogosServico : IJogosServico
    {
        private readonly IJogosRepositorio jogosRepositorio;
        private readonly ICartasRepositorio cartasRepositorio;

        public JogosServico(IJogosRepositorio jogosRepositorio,
                            ICartasRepositorio cartasRepositorio)
        {
            this.jogosRepositorio = jogosRepositorio;
            this.cartasRepositorio = cartasRepositorio;
        }

        public Jogo IniciarJogo(string nomeJogador)
        {
            int idJogo = jogosRepositorio.Inserir(nomeJogador);

            DistribuirCartasRodada(2, idJogo, true);
            DistribuirCartasRodada(2, idJogo, false);

            IList<JogadasConsulta> jogadas = jogosRepositorio.RecuperarJogadas(idJogo);

            return VerificarResultado(jogadas);
        }

        public Jogo ContinuarJogo(int idJogo, bool continua)
        {
            IList<JogadasConsulta> jogadas = jogosRepositorio.RecuperarJogadas(idJogo);

            bool jogoEncerrado = jogadas.Any(x => x.Encerrado);
            if (jogoEncerrado)
                return new(null, null, 0, 0, "Esse jogo já acabou!");

            if (continua)
                DistribuirCartasRodada(1, idJogo, false);
            else
                FinalizarJogo(idJogo, jogadas);

            jogadas = jogosRepositorio.RecuperarJogadas(idJogo);

            return VerificarResultado(jogadas);
        }
        private void FinalizarJogo(int idJogo, IList<JogadasConsulta> jogadas) 
        {
            jogosRepositorio.EncerrarJogo(idJogo);
            int pontuacaoDealer = RecuperarCartas(jogadas, true).Sum(x => x.Valor);
            int quantidadeDealer = RecuperarCartas(jogadas, true).Count();

            bool limiteDealer = pontuacaoDealer < 17 && quantidadeDealer < 5;
            while (limiteDealer)
            {
                DistribuirCartasRodada(1, idJogo, true);

                pontuacaoDealer = RecuperarCartas(jogadas, true).Sum(x => x.Valor);
                quantidadeDealer = RecuperarCartas(jogadas, true).Count();
                limiteDealer = pontuacaoDealer < 17 && quantidadeDealer < 5;
            }
        }

        public IList<Carta> DistribuirCartasRodada(int quantidade, int idJogo, bool dealer)
        {
            IList<Carta> cartas = AtribuirCartasPersonagem(idJogo, quantidade);
            cartas = VerificarValorAs(cartas);

            GravarJogada(cartas, idJogo, dealer);

            return cartas;
        }

        private IList<Carta> AtribuirCartasPersonagem(int idJogo, int quantidade)
        {
            IList<Carta> cartas = new List<Carta> { };

            if (quantidade > 0)
            {
                IList<Carta> cartasDisponiveis = cartasRepositorio.RecuperarDisponiveis(idJogo);
                for (int i = 0; i < quantidade; i++)
                {
                    var rnd = new Random();
                    Carta proximaCarta = cartasDisponiveis[rnd.Next(cartasDisponiveis.Count)];
                    cartasDisponiveis.Remove(proximaCarta);
                    cartas.Add(proximaCarta);
                }
            }
            return cartas;
        }

        private void GravarJogada(IList<Carta> cartas, int idJogo, bool dealer)
        {
            foreach (Carta carta in cartas)
            {
                jogosRepositorio.InserirJogada(carta, idJogo, dealer);
            }
        }

        private static IList<Carta> VerificarEsconderCartaDealer(IList<Carta> cartasDealer)
        {
            bool primeiraRodada = cartasDealer.Count == 2;
            bool dealerTemAsOu10 = !cartasDealer.Any(x => x.Valor is 10 or 11);

            if (primeiraRodada && dealerTemAsOu10)
                cartasDealer[0] = new Carta("Escondida", new Nipe("Escondido"), 0);

            return cartasDealer;
        }

        private static Jogo VerificarResultado(IList<JogadasConsulta> jogadas)
        {
            IQueryable<Carta> queryDealer = RecuperarCartas(jogadas, true);
            IQueryable<Carta> queryJogador = RecuperarCartas(jogadas, false);

            return new(VerificarEsconderCartaDealer(queryDealer.ToList()),
                        queryJogador.ToList(),
                        queryDealer.Sum(x => x.Valor),
                        queryJogador.Sum(x => x.Valor),
                        RecuperarTextoResultado(jogadas));
        }

        private static string RecuperarTextoResultado(IList<JogadasConsulta> jogadas)
        {
            string[] textoResultado = { "Você Ganhou!", "Você Perdeu!", "Você pode parar ou continuar!" };
            if (VerificarGanhou(jogadas))
                return textoResultado[0];

            if (VerificarPerdeu(jogadas))
                return textoResultado[1];

            return textoResultado[2];
        }

        private static bool VerificarGanhou(IList<JogadasConsulta> jogadas)
        {
            int pontuacaoDealer = RecuperarCartas(jogadas, true).Sum(x => x.Valor);
            int pontuacaoJogador = RecuperarCartas(jogadas, false).Sum(x => x.Valor);
            int quantidadeDealer = RecuperarCartas(jogadas, true).Count();

            bool dealerExcedeu21Pontos = (pontuacaoDealer > 21 &&
                                          pontuacaoJogador <= 21);

            bool jogadorCom21DealerNao = (pontuacaoJogador == 21 &&
                                                 pontuacaoDealer < 21);

            bool jogadorMaisPertoDe21DealerEsgotado = (pontuacaoJogador < 21 &&
                                                       (quantidadeDealer >= 5 || pontuacaoDealer >= 17) &&
                                                       pontuacaoJogador > pontuacaoDealer);

            return dealerExcedeu21Pontos || jogadorCom21DealerNao || jogadorMaisPertoDe21DealerEsgotado;
        }

        private static bool VerificarPerdeu(IList<JogadasConsulta> jogadas)
        {
            int pontuacaoDealer = RecuperarCartas(jogadas, true).Sum(x => x.Valor);
            int pontuacaoJogador = RecuperarCartas(jogadas, false).Sum(x => x.Valor);

            bool dealerCom21JogadorNao = (pontuacaoDealer == 21 &&
                                          pontuacaoJogador != 21);

            bool jogadorPassouDe21 = (pontuacaoJogador > 21);

            return dealerCom21JogadorNao || jogadorPassouDe21;
        }

        private static IQueryable<Carta> RecuperarCartas(IList<JogadasConsulta> jogadas, bool dealer)
        {
            IQueryable<JogadasConsulta> query = jogadas.Where(x => x.IdtDealer == dealer).AsQueryable();

            IQueryable<Carta> carta = query.Select(x => new Carta(x.DescricaoCarta, new(x.DescricaoNipe), x.ValorCarta))
                                           .AsQueryable();

            return carta;
        }

        private static IList<Carta> VerificarValorAs(IList<Carta> cartas)
        {
            bool primeiraRodada = cartas.Count == 2;
            bool existeAs = cartas.Any(x => x.Id == 1);
            bool existeCartaValor10 = cartas.Any(x => x.Id != 1 && x.Valor == 10);

            if (primeiraRodada && existeAs && existeCartaValor10)
                cartas = cartas.Select(x => new Carta(x.Descricao, x.Nipe, x.Id == 1 ? x.Valor : 11))
                               .ToList();

            return cartas;
        }
    }
}
