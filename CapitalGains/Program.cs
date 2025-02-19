using System.Text.Json;


namespace CapitalGains.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Leitura das entradas via STDIN.
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                // Encerra a leitura se encontrar uma linha vazia.
                if (string.IsNullOrWhiteSpace(line))
                    break;

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    // Desserializa o JSON de entrada diretamente em uma lista de TradeOperation.
                    List<TradeOperation> operations = JsonSerializer.Deserialize<List<TradeOperation>>(line, options);

                    // Calcula os impostos utilizando o serviço de domínio.
                    var calculator = new CapitalGainsCalculator();
                    var taxes = calculator.Calculate(operations);

                    // Prepara o resultado: uma lista de objetos com a propriedade "tax".
                    var taxResults = new List<object>();
                    foreach (var tax in taxes)
                    {
                        taxResults.Add(new { tax = Math.Round(tax, 2, MidpointRounding.AwayFromZero) });
                    }

                    // Serializa o resultado em JSON e escreve na saída padrão.
                    string output = JsonSerializer.Serialize(taxResults);
                    Console.WriteLine(output);
                }
                catch (Exception ex)
                {
                    // Em caso de erro, escreve a mensagem na saída de erro.
                    Console.Error.WriteLine($"Erro ao processar a entrada: {ex.Message}");
                }
            }
        }
    }
}
