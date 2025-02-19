using CapitalGains.Domain.Entities;

namespace CapitalGains.Domain.Services.Interface
{
    public  interface ITradeOperationValidator
    {
        public (bool IsValid, string ErrorMessage) Validate(List<TradeOperation> operations);
    }
}