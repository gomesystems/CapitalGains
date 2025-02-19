using CapitalGains.Domain.Entities;
using CapitalGains.Domain.Services;
using CapitalGains.Domain.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CapitalGains.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TradeOperationsController : ControllerBase
    {
        private readonly ITradeOperationValidator _tradeOperationValidator;

        public TradeOperationsController(ITradeOperationValidator tradeOperationValidator)
        {
            _tradeOperationValidator = tradeOperationValidator;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] List<TradeOperation> operations)
        {

            var validationResult = _tradeOperationValidator.Validate(operations);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { error = validationResult.ErrorMessage });
            }

            try
            {
                var calculator = new CapitalGainsCalculator();
                var taxes = calculator.Calculate(operations);

                var result = taxes.Select(t => new { tax = Math.Round(t, 2, MidpointRounding.AwayFromZero) });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
