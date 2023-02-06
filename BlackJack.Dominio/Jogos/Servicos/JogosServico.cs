using BlackJack.Dominio.Jogos.Entidades;
using BlackJack.Dominio.Jogos.Enumeradores;
using BlackJack.Dominio.Jogos.Repositorios;
using BlackJack.Dominio.Jogos.Servicos.Interfaces;
using BlackJack.Infra.Jogos.Repositorios.Consultas;
using NPOI.Util;
using Org.BouncyCastle.Math.EC.Rfc7748;

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

            IList<Carta> cartasDealer = DistribuirCartasRodada(2, idJogo, true);
            IList<Carta> cartasJogador = DistribuirCartasRodada(2, idJogo, false);

            IList<JogadasConsulta> jogadas = jogosRepositorio.RecuperarJogadas(idJogo);

            cartasDealer = VerificarEsconderCartaDealer(cartasDealer);

            ResultadoEnum resultado = VerificarResultado(jogadas);

            return new (cartasDealer, cartasJogador, 
                        RecuperarCartas(jogadas, true).Sum(x => x.Valor), 
                        RecuperarCartas(jogadas, false).Sum(x => x.Valor),
                        RecuperarTextoResultado(resultado));
        }

        public Jogo ContinuarJogo(int idJogo)
        {
            IList<JogadasConsulta> jogadas = jogosRepositorio.RecuperarJogadas(idJogo);
            IQueryable<Carta> queryDealer = RecuperarCartas(jogadas, true);
            IQueryable<Carta> queryJogador = RecuperarCartas(jogadas, false);

            if (VerificarGanhou(jogadas) || VerificarPerdeu(jogadas))
                return new (null, null, queryDealer.Sum(x => x.Valor), queryJogador.Sum(x => x.Valor), "Esse jogo já acabou!");

            DistribuirCartasRodada(1, idJogo, false);
            jogadas = jogosRepositorio.RecuperarJogadas(idJogo);
            queryDealer = RecuperarCartas(jogadas, true);
            queryJogador = RecuperarCartas(jogadas, false);

            ResultadoEnum resultado = VerificarResultado(jogadas);

            return new (VerificarEsconderCartaDealer(queryDealer.ToList()),
                        queryJogador.ToList(),
                        queryDealer.Sum(x => x.Valor),
                        queryJogador.Sum(x => x.Valor),
                        RecuperarTextoResultado(resultado));
        }

        public Jogo FinalizarJogo(int idJogo)
        {
            IList<JogadasConsulta> jogadas = jogosRepositorio.RecuperarJogadas(idJogo);
            return null;
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
            if (!cartasDealer.Any(x => x.Valor is 10 or 11))
                cartasDealer[0] = new Carta("Escondida", new Nipe("Escondido"), 0);

            return cartasDealer;
        }

        private static ResultadoEnum VerificarResultado(IList<JogadasConsulta> jogadas)
        {
            if (VerificarGanhou(jogadas))
                return ResultadoEnum.Ganhou;

            if (VerificarPerdeu(jogadas))
                return ResultadoEnum.Perdeu;

            return ResultadoEnum.Continua;
        }
        private static string RecuperarTextoResultado(ResultadoEnum resultado) 
        {
            string[] textoResultado = { "Você Ganhou!", "Você Perdeu!", "Você pode parar ou continuar!" };
            switch (resultado)
            {
                case ResultadoEnum.Ganhou:
                    return textoResultado[0];
                case ResultadoEnum.Perdeu:
                    return textoResultado[1];
                default:
                    return textoResultado[2];
            }
        } 

        private static bool VerificarGanhou(IList<JogadasConsulta> jogadas)
        {
            int pontuacaoDealer = RecuperarCartas(jogadas, true).Sum(x => x.Valor);
            int pontuacaoJogador = RecuperarCartas(jogadas, false).Sum(x => x.Valor);

            bool dealerExcedeu21Pontos = (pontuacaoDealer > 21);

            bool jogadorComecouCom21DealerNao = (pontuacaoJogador == 21 &&
                                                 pontuacaoDealer < 21);

            return dealerExcedeu21Pontos || jogadorComecouCom21DealerNao;
        }

        private static bool VerificarPerdeu(IList<JogadasConsulta> jogadas)
        {
            int pontuacaoDealer = RecuperarCartas(jogadas, true).Sum(x => x.Valor);
            int pontuacaoJogador = RecuperarCartas(jogadas, false).Sum(x => x.Valor);

            bool dealerComecouCom21JogadorNao = (pontuacaoDealer == 21 &&
                                                 pontuacaoJogador != 21);

            bool jogadorPassouDe21 = (pontuacaoJogador > 21);

            return dealerComecouCom21JogadorNao || jogadorPassouDe21;
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
