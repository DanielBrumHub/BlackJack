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
        [Route("iniciar")]
        public JogoResponse IniciarJogo(IniciarJogoRequest request)
        {
            return jogosAppServico.IniciarJogo(request.NomeJogador);
        }

        /// <summary>
        /// Pede nova carta
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("continuar")]
        public JogoResponse ContinuarJogo([FromBody] ContinuarJogoRequest request)
        {
            return jogosAppServico.ContinuarJogo(request.IdJogo);
        }

        /// <summary>
        /// Finaliza o Jogo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("parar")]
        public JogoResponse FinalizarJogo([FromBody] ContinuarJogoRequest request)
        {
            return jogosAppServico.ContinuarJogo(request.IdJogo);
        }

    }
}
