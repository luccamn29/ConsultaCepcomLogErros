using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ConsultaViaCep.Controllers
{
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        [HttpGet(@"v1/BuscaCep")]
        public IActionResult BuscarEndereco(string cep)
        {
            ConsumoAPI.Contratos.APIViacep.GET.Response retorno;
            try
            {
                retorno = ConsumoAPI.ExecutarApi.ConsultaVerboGet<ConsumoAPI.Contratos.APIViacep.GET.Response>("https://viacep.com.br/ws/" + cep + "/json/");
            }
            catch (Exception ex)
            {
                var erro = new ConsumoAPI.Contratos.APILogErro.POST.Request();
                erro.DataHora = DateTime.Now;
                erro.MensagemErro = ex.Message;
                erro.RastreioErro = ex.StackTrace;
                erro.NomeMaquina = Environment.MachineName.ToString();
                erro.NomeAplicacao = ex.Source;
                erro.Usuario = Environment.UserName;
                ConsumoAPI.ExecutarApi.ConsultaVerboPost<ConsumoAPI.Contratos.APILogErro.POST.Request>("https://logaplicacao.aiur.com.br/v1/Logs", erro);
                return StatusCode(400);
            }
            return StatusCode(200,retorno);
        }
    }
}