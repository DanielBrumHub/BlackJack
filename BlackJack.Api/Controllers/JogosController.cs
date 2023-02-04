using BlackJack.Aplicacao.Jogos.Servicos.Interfaces;
using BlackJack.DataTransfer.Jogos.Requests;
using BlackJack.DataTransfer.Jogos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BlackJack.Api.Controllers
{
    [ApiController]
    [Route("jogo")]
    public class JogosController : ControllerBase
    {
        private readonly IJogosAppServico jogosAppServico;

        public JogosController(IJogosAppServico jogosAppServico)
        {
            this.jogosAppServico = jogosAppServico;
        }

        /// <summary>
        /// Inicia Jogo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JogoResponse IniciarJogo(IniciarJogoRequest request)
        {
            return jogosAppServico.IniciarJogo(request.NomeJogador);
        }

        /// <summary>
        /// Segue para a próxima jogada
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public JogoResponse SeguirRodadaJogo([FromBody] ContinuarJogoRequest request)
        {
            return jogosAppServico.SeguirRodadaJogo(request.IdJogo);
        }

    }
}
