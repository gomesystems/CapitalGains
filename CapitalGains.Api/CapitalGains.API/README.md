### Capital Gains - Resumo da Documentação
Descrição:

Aplicação .NET Core que calcula o imposto sobre operações de compra e venda de ações conforme regras pré-definidas. 
Utiliza a entidade TradeOperation tanto para deserialização dos dados de entrada quanto para a lógica de negócio, mantendo a implementação simples e fácil de manter.

### Estrutura do Projeto:

Domain:
Entities: TradeOperation representa cada operação de compra ou venda.
Enums: OperationType define os tipos de operação (compra ou venda).
Services: CapitalGainsCalculator implementa a lógica de cálculo dos impostos, gerenciando o portfólio (quantidade de ações, preço médio e prejuízo acumulado).

Presentation:
Programa principal que lê os dados de entrada, processa as operações e retorna os resultados.

### Regras de Negócio:

Compra (buy):
Atualiza o portfólio (quantidade e preço médio) sem gerar imposto.
Venda (sell):
Se o valor total da operação for ≤ R$20.000, nenhum imposto é cobrado.
Se for > R$20.000:
Calcula o lucro: (unit-cost - preço médio) × quantity.
Deduz prejuízos acumulados do lucro.
Aplica 20% de imposto sobre o lucro líquido.
Prejuízos são acumulados para compensar lucros futuros e o preço médio é reiniciado quando todas as ações são vendidas.

Compilação e Execução:

### Pré-requisito: .NET 6 SDK
Compilação: Executar dotnet build na raiz do projeto.

### Decisões Técnicas e Arquiteturais:

Simplicidade: Uso direto da entidade TradeOperation, eliminando DTOs e camadas intermediárias.
Domain-Driven Design (DDD): A lógica de negócio fica isolada na camada de domínio, facilitando futuras extensões.
Clean Code: Código organizado, legível e com responsabilidades bem definidas.
STDIN/STDOUT: Facilita integração com pipelines e testes automatizados.
Arquitetura em Camadas: Separa a lógica de negócio da interface, permitindo 
evolução independente de cada camada.


### Exemplo de Teste de Entrada:

[{"operation":"buy", "unit-cost":10.00, "quantity": 10000},
 {"operation":"sell", "unit-cost":50.00, "quantity": 10000},
 {"operation":"buy", "unit-cost":20.00, "quantity": 10000},
 {"operation":"sell", "unit-cost":50.00, "quantity": 10000}]
