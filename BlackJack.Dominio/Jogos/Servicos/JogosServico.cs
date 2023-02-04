using BlackJack.Dominio.Jogos.Entidades;
using BlackJack.Dominio.Jogos.Repositorios;
using BlackJack.Dominio.Jogos.Servicos.Interfaces;
using System.Collections.Generic;

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

            Jogo jogo = DistribuirCartasRodada(2, 2, idJogo);

            return jogo;
        }

        public Jogo SeguirRodadaJogo(int jogo)
        {
            return null;
        }

        public Jogo FinalizarJogo(Jogo jogo)
        {
            return null;
        }

        public Jogo DistribuirCartasRodada(int quantidadeDealer, int quantidadeJogador, int idJogo)
        {
            IList<Carta> cartasDealer = AtribuirCartasPersonagem(idJogo, quantidadeDealer);
            cartasDealer = VerificarEsconderCartaDealer(cartasDealer);

            IList<Carta> cartasJogador = AtribuirCartasPersonagem(idJogo, quantidadeJogador);

            GravarJogada(cartasDealer, cartasJogador, idJogo);

            return new Jogo(cartasDealer, cartasJogador);
        }

        private IList<Carta> AtribuirCartasPersonagem(int idJogo, int quantidade)
        {
            IList<Carta> cartasDisponiveis = cartasRepositorio.RecuperarDisponiveis(idJogo);
            IList<Carta> cartas = new List<Carta> { };

            for (int i = 0; i < quantidade; i++)
            {
                var rnd = new Random();
                Carta proximaCarta = cartasDisponiveis[rnd.Next(cartasDisponiveis.Count)];
                cartasDisponiveis.Remove(proximaCarta);
                cartas.Add(proximaCarta);
            }
            return cartas;
        }

        private IList<Carta> VerificarEsconderCartaDealer(IList<Carta> cartasDealer)
        {
            if (cartasDealer.Any(x => x.Valor is 10 or 11)) 
                cartasDealer[0] = new Carta("Escondida","Escondido",0);

            return cartasDealer;
        }

        private void GravarJogada(IList<Carta> cartasDealer, IList<Carta> cartasJogador, int idJogo) 
        {

        }
    }
}
