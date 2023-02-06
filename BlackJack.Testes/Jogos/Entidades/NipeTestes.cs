using BlackJack.Dominio.Jogos.Entidades;
using FizzWare.NBuilder;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace BlackJack.Testes.Jogos.Entidades
{
    public class NipeTestes
    {
        public Nipe sut;

        public NipeTestes()
        {
            sut = Builder<Nipe>.CreateNew().Build();
        }
        public class ConstrutorMetodo : NipeTestes
        {
            [Fact]
            public void Quando_ContrutorForValido_Espero_ObjetoInstanciado()
            {
                Nipe jogo = new("Descricao");
                jogo.Descricao.Should().Be("Descricao");
            }
        }
    }
}
