using System.Text.Json.Serialization;
using CapitalGains.Domain.Enums;

namespace CapitalGains.Domain.Entities
{
   
    public class TradeOperation
    {
      
        [JsonPropertyName("operation")]
        public string OperationText { get; set; }

      
        [JsonIgnore]
        public OperationType Operation => OperationText.ToLower() == "buy" ? OperationType.Buy : OperationType.Sell;

        
        [JsonPropertyName("unit-cost")]
        public decimal UnitCost { get; set; }

        
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
