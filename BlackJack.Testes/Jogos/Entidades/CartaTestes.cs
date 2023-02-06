using BlackJack.Dominio.Jogos.Entidades;
using FizzWare.NBuilder;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace BlackJack.Testes.Jogos.Entidades
{
    public class CartaTestes
    {
        public Carta sut;

        public CartaTestes()
        {
            sut = Builder<Carta>.CreateNew().Build();
        }
        public class ConstrutorMetodo : CartaTestes
        {
            [Fact]
            public void Quando_ContrutorForValido_Espero_ObjetoInstanciado()
            {
                Nipe nipe = Builder<Nipe>.CreateNew().Build();

                Carta carta = new("Descricao", nipe, 11);

                carta.Descricao.Should().Be("Descricao");
                carta.Nipe.Should().Be(nipe);
                carta.Valor.Should().Be(11);
            }
        }
    }
}
