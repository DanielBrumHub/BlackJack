using BlackJack.Dominio.Jogos.Entidades;
using FizzWare.NBuilder;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace BlackJack.Testes.Jogos.Entidades
{
    public class BaralhoTestes
    {
        public Baralho sut;

        public BaralhoTestes()
        {
            sut = Builder<Baralho>.CreateNew().Build();
        }
        public class ConstrutorMetodo : BaralhoTestes
        {
            [Fact]
            public void Quando_ContrutorForInvalido_Espero_ObjetoNaoInstanciado()
            {
                IList<Carta> cartas = new List<Carta>{ };
                Baralho baralho = new(cartas);

                baralho.Cartas.Should().BeNull();
            }

            [Fact]
            public void Quando_ContrutorForValido_Espero_ObjetoInstanciado()
            {
                IList<Carta> cartas = Builder<Carta>.CreateListOfSize(5).Build();
                Baralho baralho = new(cartas);

                baralho.Cartas.Should().BeEquivalentTo(cartas);
            }
        }
    }
}
