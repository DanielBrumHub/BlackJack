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

                Jogo jogo = new(cartas, cartas, "Resultado", 30);

                jogo.CartasDealer.Should().BeEquivalentTo(cartas);
                jogo.CartasJogador.Should().BeEquivalentTo(cartas);
                jogo.Resultado.Should().Be("Resultado");
                jogo.IdJogo.Should().Be(30);
            }
        }
    }
}
