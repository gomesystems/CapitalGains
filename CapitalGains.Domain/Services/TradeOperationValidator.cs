using CapitalGains.Domain.Entities;
using CapitalGains.Domain.Services.Interface;

public class TradeOperationValidator : ITradeOperationValidator
{
    public (bool IsValid, string ErrorMessage) Validate(List<TradeOperation> operations)
    {
        if (operations == null || !operations.Any())
        {
            return (false, "Invalid input: operations list is empty or null.");
        }

        foreach (var operation in operations)
        {
            if (string.IsNullOrEmpty(operation.OperationText) || operation.UnitCost <= 0 || operation.Quantity <= 0)
            {
                return (false, "Invalid input: one or more operations have invalid data.");
            }
        }

        return (true, string.Empty);
    }
}
