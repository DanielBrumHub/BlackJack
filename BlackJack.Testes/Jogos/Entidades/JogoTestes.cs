using BlackJack.Dominio.Jogos.Entidades;
using FizzWare.NBuilder;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace BlackJack.Testes.Jogos.Entidades
{
    public class JogoTestes
    {
        public Jogo sut;

        public JogoTestes()
        {
            sut = Builder<Jogo>.CreateNew().Build();
        }
        public class ConstrutorMetodo : JogoTestes
        {
            [Fact]
            public void Quando_ContrutorForValido_Espero_ObjetoInstanciado()
            {
                IList<Carta> cartas = Builder<Carta>.CreateListOfSize(5).Build();

                Jogo jogo = new(cartas, cartas, 20, 30, "Resultado");

                jogo.CartasDealer.Should().BeEquivalentTo(cartas);
                jogo.CartasJogador.Should().BeEquivalentTo(cartas);
                jogo.PontuacaoDealer.Should().Be(20);
                jogo.PontuacaoJogador.Should().Be(30);
                jogo.Resultado.Should().Be("Resultado");
            }
        }
    }
}
