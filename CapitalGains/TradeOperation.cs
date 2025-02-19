using System.Text.Json.Serialization;

namespace CapitalGains.Presentation
{
    public class TradeOperation
    {
        /// <summary>
        /// Tipo da operação representado como texto (ex.: "buy" ou "sell").
        /// </summary>
        [JsonPropertyName("operation")]
        public string OperationText { get; set; }

        /// <summary>
        /// Propriedade calculada que converte o valor textual em um enum OperationType.
        /// </summary>
        [JsonIgnore]
        public OperationType Operation => OperationText.ToLower() == "buy" ? OperationType.Buy : OperationType.Sell;

        /// <summary>
        /// Custo unitário da ação.
        /// </summary>
        [JsonPropertyName("unit-cost")]
        public decimal UnitCost { get; set; }

        /// <summary>
        /// Quantidade de ações na operação.
        /// </summary>
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }

}