# Capital Gains (Ganhos de Capital)

## Descrição

Esta aplicação calcula o imposto devido sobre operações de compra e venda de ações, conforme as regras definidas no desafio. 
A solução foi desenvolvida em .NET Core utilizando uma abordagem simples e direta, sem camadas intermediárias (como DTOs). A entidade `TradeOperation` é utilizada tanto para a deserialização dos dados de entrada quanto para a lógica de negócio, garantindo uma implementação enxuta e fácil de manter.

## Estrutura do Projeto

O projeto é dividido em duas camadas principais:

- **Domain**: Contém a lógica de negócio, as entidades e os serviços necessários para o cálculo dos ganhos de capital.
  - **Entities**: Define a entidade `TradeOperation`, que representa uma operação de compra ou venda de ações.
  - Essa entidade é diretamente utilizada para a deserialização do JSON de entrada e para o processamento das regras de negócio.
  - **Enums**: Define o enum `OperationType`, que representa os tipos de operação (compra ou venda).
  - **Services**: Implementa a lógica de cálculo dos impostos através do serviço `CapitalGainsCalculator`, que gerencia o estado do portfólio (quantidade de ações, preço médio ponderado e prejuízo acumulado) e processa cada operação na ordem em que ocorre.

- **Presentation**: Contém o programa principal que lê a entrada padrão (STDIN), processa as operações utilizando o serviço de domínio e escreve o resultado na saída padrão (STDOUT).

## Regras de Negócio

- **Operação de Compra (buy)**:
  - Atualiza o portfólio, recalculando a quantidade total de ações e o preço médio ponderado.
  - Não gera imposto.

- **Operação de Venda (sell)**:
  - Se o valor total da operação (unit-cost × quantity) for **menor ou igual a R$20.000,00**, nenhum imposto é cobrado.
  - Se o valor total for **maior que R$20.000,00**:
    - Calcula-se o lucro:
      ```
      lucro = (unit-cost - preço médio ponderado) × quantity
      ```
    - O prejuízo acumulado de vendas anteriores é deduzido dos lucros.
    - O imposto é de **20% sobre o lucro positivo** (após dedução dos prejuízos).
  - Caso a operação resulte em prejuízo, o valor negativo é acumulado para compensar lucros futuros.
  - Quando todas as ações são vendidas, o preço médio é reiniciado.

## Compilação e Execução

### Pré-requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

### Compilação

Na pasta raiz do projeto, execute:

```bash
dotnet build


### Decisões Técnicas e Arquiteturais

- Simplicidade e Direto ao Ponto:

- A solução utiliza a entidade TradeOperation diretamente para representar as operações, eliminando a 
necessidade de conversão ou camadas de DTO. Isso simplifica a arquitetura e reduz o código, mantendo o 
foco na lógica de negócio.

Domain-Driven Design (DDD):

- Mesmo com uma abordagem simplificada, a lógica de negócio é isolada na camada de domínio. 
A entidade e o serviço encapsulam as regras de cálculo de impostos, facilitando futuras extensões 
sem afetar a camada de apresentação.

Clean Code:

- O código foi estruturado para ser legível e de fácil manutenção, com comentários explicativos e responsabilida
des bem definidas em cada componente.


Arquitetura em Camadas:

- A separação entre a camada de domínio e a camada de apresentação garante que a 
lógica de negócio esteja isolada dos detalhes de interface, permitindo uma evolução 
independente de cada camada.

### Exemplo de Teste de Entrada:

[{"operation":"buy", "unit-cost":10.00, "quantity": 10000},
 {"operation":"sell", "unit-cost":50.00, "quantity": 10000},
 {"operation":"buy", "unit-cost":20.00, "quantity": 10000},
 {"operation":"sell", "unit-cost":50.00, "quantity": 10000}]
