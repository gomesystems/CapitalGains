using CapitalGains.Domain.Entities;
using CapitalGains.Domain.Enums;

namespace CapitalGains.Domain.Services
{
    
    public class CapitalGainsCalculator
    {
      
        public IList<decimal> Calculate(IList<TradeOperation> operations)
        {
            var taxes = new List<decimal>();

            int totalShares = 0;
            decimal weightedAverage = 0m;
            decimal accumulatedLoss = 0m;

            foreach (var op in operations)
            {
                if (op.Operation == OperationType.Buy)
                {
                    // Atualiza o portfólio: soma as ações e recalcula o preço médio ponderado.
                    decimal totalCost = weightedAverage * totalShares + op.UnitCost * op.Quantity;
                    totalShares += op.Quantity;
                    weightedAverage = totalShares > 0 ? totalCost / totalShares : 0;
                    weightedAverage = Math.Round(weightedAverage, 2, MidpointRounding.AwayFromZero);

                    // Operações de compra não geram imposto.
                    taxes.Add(0m);
                }
                else if (op.Operation == OperationType.Sell)
                {
                    decimal tax = 0m;
                    decimal saleTotal = op.UnitCost * op.Quantity;
                    decimal profit = (op.UnitCost - weightedAverage) * op.Quantity;

                    // Se o valor total da venda for menor ou igual a R$20.000, nenhum imposto é cobrado.
                    if (saleTotal <= 20000m)
                    {
                        if (profit < 0)
                        {
                            accumulatedLoss += Math.Abs(profit);
                        }
                    }
                    else
                    {
                        if (profit > 0)
                        {
                            // Deduz o prejuízo acumulado do lucro.
                            if (accumulatedLoss > 0)
                            {
                                if (accumulatedLoss >= profit)
                                {
                                    accumulatedLoss -= profit;
                                    profit = 0;
                                }
                                else
                                {
                                    profit -= accumulatedLoss;
                                    accumulatedLoss = 0;
                                }
                            }
                            if (profit > 0)
                            {
                                tax = Math.Round(profit * 0.2m, 2, MidpointRounding.AwayFromZero);
                            }
                        }
                        else
                        {
                            // Em caso de prejuízo, acumula o valor.
                            accumulatedLoss += Math.Abs(profit);
                        }
                    }

                    // Atualiza o portfólio: subtrai as ações vendidas.
                    totalShares -= op.Quantity;
                    if (totalShares == 0)
                    {
                        weightedAverage = 0;
                    }
                    taxes.Add(tax);
                }
            }

            return taxes;
        }
    }
}
