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
        /// <response code="201">Sucesso</response>
        /// <response code="400">Falha</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public JogoResponse IniciarJogo(IniciarJogoRequest request)
        {
            return jogosAppServico.IniciarJogo(request.NomeJogador);
        }

        /// <summary>
        /// Continua ou Finaliza o jogo
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Falha</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public JogoResponse ContinuarJogo([FromBody] ContinuarJogoRequest request)
        {
            return jogosAppServico.ContinuarJogo(request.IdJogo, request.Continua);
        }

    }
}
