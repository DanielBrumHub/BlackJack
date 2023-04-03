using AutoMapper;
using BlackJack.Aplicacao.Jogos.Servicos.Interfaces;
using BlackJack.DataTransfer.Jogos.Responses;
using BlackJack.Dominio.Jogos.Entidades;
using BlackJack.Dominio.Jogos.Servicos.Interfaces;

namespace BlackJack.Aplicacao.Jogos.Servicos
{
    public class JogosAppServico : IJogosAppServico
    {
        private readonly IMapper mapper;
        private readonly IJogosServico jogosServico;

        public JogosAppServico(IMapper mapper,
                               IJogosServico jogosServico)
        {
            this.mapper = mapper;
            this.jogosServico = jogosServico;
        }

        public JogoResponse IniciarJogo(string nomeJogador)
        {
            Jogo jogo = jogosServico.IniciarJogo(nomeJogador);

            return mapper.Map<JogoResponse>(jogo);
        }

        public JogoResponse ContinuarJogo(int idJogo, bool continua)
        {
            Jogo jogo = jogosServico.ContinuarJogo(idJogo, continua);

            return mapper.Map<JogoResponse>(jogo);
        }
    }
}
