using System.Collections.Generic;
using Xunit;
using CapitalGains.Domain.Entities;
using CapitalGains.Domain.Enums;
using CapitalGains.Domain.Services;

namespace CapitalGains.Tests
{
    public class CapitalGainsCalculatorTests
    {
        [Fact]
        public void Test_Case1_NoTaxOnSmallSales()
        {
            // Caso #1:
            // buy: unit cost 10.00, quantity 100
            // sell: unit cost 15.00, quantity 50 (valor total abaixo de 20000)
            // sell: unit cost 15.00, quantity 50 (idem)
            var operations = new List<TradeOperation>
            {
                new TradeOperation { OperationText = "buy", UnitCost = 10.00m, Quantity = 100 },
                new TradeOperation { OperationText = "sell", UnitCost = 15.00m, Quantity = 50 },
                new TradeOperation { OperationText = "sell", UnitCost = 15.00m, Quantity = 50 }
            };

            var calculator = new CapitalGainsCalculator();
            var taxes = calculator.Calculate(operations);

            Assert.Equal(3, taxes.Count);
            Assert.Equal(0.00m, taxes[0]);
            Assert.Equal(0.00m, taxes[1]);
            Assert.Equal(0.00m, taxes[2]);
        }

        // Outros testes podem ser implementados conforme os casos de uso do desafio...
    }
}
