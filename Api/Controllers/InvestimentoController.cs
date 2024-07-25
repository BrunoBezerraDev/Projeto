using Microsoft.AspNetCore.Mvc;
using API.Model;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestimentoController : ControllerBase
    {
        [HttpPost("calcular")]
        public IActionResult CalcularInvestimento([FromBody] InvestimentoModel model)
        {
            if (model.ValorInicial <= 0 || model.Prazo <= 1)
                return BadRequest("Valores inválidos");

            const double CDI = 0.009;
            const double TB = 1.08;



            double valorBruto = model.ValorInicial;

            for (int i = 0; i < model.Prazo; i++)
            {
                valorBruto *= (1 + (CDI * TB));
            }

            double imposto = CalcularImposto(model.Prazo, valorBruto - model.ValorInicial);
            double valorLiquido = valorBruto - imposto;

            var resultado = new
            {
                valorBruto = valorBruto.ToString("F2"),
                valorLiquido = valorLiquido.ToString("F2")
            };

            return Ok(resultado);
        }

        private double CalcularImposto(int prazo, double rendimento)
        {
            double aliquota;

            if (prazo <= 6)
                aliquota = 0.225;
            else if (prazo <= 12)
                aliquota = 0.20;
            else if (prazo <= 24)
                aliquota = 0.175;
            else
                aliquota = 0.15;

            return rendimento * aliquota;
        }
    }
}
