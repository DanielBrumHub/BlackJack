using BlackJack.Dominio.Jogos.Entidades;
using BlackJack.Dominio.Jogos.Repositorios;
using BlackJack.Dominio.Jogos.Servicos;
using BlackJack.Infra.Jogos.Repositorios.Consultas;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Autoglass.Servicos.Estatistica.Dominio.Testes.LimitesMonetariosVersoes.Servicos
{
    public class JogosServicoTestes
    {
        private readonly JogosServico sut;
        private readonly IJogosRepositorio jogosRepositorio;
        private readonly ICartasRepositorio cartasRepositorio;

        public JogosServicoTestes()
        {
            jogosRepositorio = Substitute.For<IJogosRepositorio>();
            cartasRepositorio = Substitute.For<ICartasRepositorio>();

            sut = new JogosServico(jogosRepositorio, cartasRepositorio);
        }

        public class IniciarJogoMetodo : JogosServicoTestes
        {
            [Theory]
            [InlineData("")]
            [InlineData(null)]
            public void Quando_NomeDoJogadorInvalido_Espero_Exception(string nomeJogador)
            {
                sut.Invoking(x => x.IniciarJogo(nomeJogador)).Should().Throw<Exception>();
            }
            [Fact]
            public void Quando_NomeDoJogadorValido_Espero_JogoIniciado()
            {
                IList<JogadasConsulta> jogadas = Builder<JogadasConsulta>.CreateListOfSize(5).Build();
                string nomeJogador = "Nome";
                int idJogo = 5;

                jogosRepositorio.Inserir(nomeJogador).Returns(x => idJogo);
                jogosRepositorio.RecuperarJogadas(idJogo).Returns(x => jogadas);
                Jogo jogo = sut.VerificarResultado(jogadas, idJogo, true);

                sut.IniciarJogo(nomeJogador).Should().BeEquivalentTo(jogo);
            }
        }

        public class ContinuarJogoMetodo : JogosServicoTestes
        {
            [Fact]
            public void Quando_JogoEncerrado_Espero_Exception()
            {
                int idJogo = 5;
                IList<JogadasConsulta> jogadas = Builder<JogadasConsulta>.CreateListOfSize(5).All()
                                                                         .With(x => x.Encerrado, true).Build();

                jogosRepositorio.RecuperarJogadas(idJogo).Returns(x => jogadas);

                sut.Invoking(x => x.ContinuarJogo(idJogo, true)).Should().Throw<Exception>();
            }

            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Quando_JogoNaoEncerrado_Espero_Resultado(bool continua)
            {
                int idJogo = 5;
                IList<JogadasConsulta> jogadas = Builder<JogadasConsulta>.CreateListOfSize(5).All()
                                                                         .With(x => x.Encerrado, false).Build();

                jogosRepositorio.RecuperarJogadas(idJogo).Returns(x => jogadas);
                Jogo jogo = sut.VerificarResultado(jogadas, idJogo, continua);

                sut.ContinuarJogo(idJogo, true).Should().BeEquivalentTo(jogo);
            }
        }

        public class FinalizarJogoMetodo : JogosServicoTestes
        {
            [Fact]
            public void Quando_ParametrosValidos_Espero_PassagemLimpa()
            {
                int idJogo = 5;
                IList<JogadasConsulta> jogadas = Builder<JogadasConsulta>.CreateListOfSize(5).All()
                                                                         .With(x => x.IdtDealer, true).Build();

                sut.Invoking(x => x.FinalizarJogo(idJogo, jogadas)).Should().NotThrow<Exception>();
            }
        }

        public class DistribuirCartasRodadaMetodo : JogosServicoTestes
        {
            [Fact]
            public void Quando_ParametrosValidos_Espero_PassagemLimpa()
            {
                int idJogo = 5;
                int quantidade = 3;

                sut.Invoking(x => x.DistribuirCartasRodada(quantidade, idJogo, true)).Should().NotThrow<Exception>();
            }
        }

        public class AtribuirCartasPersonagemMetodo : JogosServicoTestes
        {
            [Fact]
            public void Quando_QuantidadeForZero_Espero_RetornoVazio()
            {
                int idJogo = 5;
                int quantidade = 0;
                IList<Carta> cartas = new List<Carta> { };

                sut.AtribuirCartasPersonagem(idJogo, quantidade).Should().BeEquivalentTo(cartas);
            }

            [Fact]
            public void Quando_ParametrosValidos_Espero_RetornoPreenchido()
            {
                int idJogo = 5;
                int quantidade = 0;
                IList<Carta> cartas = Builder<Carta>.CreateListOfSize(5).Build();
                cartasRepositorio.RecuperarDisponiveis(idJogo).Returns(cartas);

                sut.AtribuirCartasPersonagem(idJogo, quantidade).Should().NotBeNull();
            }
        }
    }
}
