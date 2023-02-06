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
                IList<JogadasConsulta> jogadas = Builder<JogadasConsulta>.CreateListOfSize(5).All()
                                                                         .With(x => x.Encerrado, true).Build();
                int idJogo = 5;

                jogosRepositorio.RecuperarJogadas(idJogo).Returns(x => jogadas);

                sut.Invoking(x => x.ContinuarJogo(idJogo, true)).Should().Throw<Exception>();
            }
        }
    }
}
