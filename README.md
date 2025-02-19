# Capital Gains - Code Challenge

## Descri��o

Esta aplica��o de linha de comando (CLI) calcula o imposto devido sobre opera��es de compra e venda de a��es, conforme as regras definidas no desafio. A solu��o foi desenvolvida em .NET Core utilizando uma abordagem simples e direta, sem camadas intermedi�rias (como DTOs). A entidade `TradeOperation` � utilizada tanto para a deserializa��o dos dados de entrada quanto para a l�gica de neg�cio, garantindo uma implementa��o enxuta e f�cil de manter.

## Estrutura do Projeto

O projeto � dividido em duas camadas principais:

- **Domain**: Cont�m a l�gica de neg�cio, as entidades e os servi�os necess�rios para o c�lculo dos ganhos de capital.
  - **Entities**: Define a entidade `TradeOperation`, que representa uma opera��o de compra ou venda de a��es. Essa entidade � diretamente utilizada para a deserializa��o do JSON de entrada e para o processamento das regras de neg�cio.
  - **Enums**: Define o enum `OperationType`, que representa os tipos de opera��o (compra ou venda).
  - **Services**: Implementa a l�gica de c�lculo dos impostos atrav�s do servi�o `CapitalGainsCalculator`, que gerencia o estado do portf�lio (quantidade de a��es, pre�o m�dio ponderado e preju�zo acumulado) e processa cada opera��o na ordem em que ocorre.

- **Presentation**: Cont�m o programa principal (CLI) que l� a entrada padr�o (STDIN), processa as opera��es utilizando o servi�o de dom�nio e escreve o resultado na sa�da padr�o (STDOUT).

## Regras de Neg�cio

- **Opera��o de Compra (buy)**:
  - Atualiza o portf�lio, recalculando a quantidade total de a��es e o pre�o m�dio ponderado.
  - N�o gera imposto.

- **Opera��o de Venda (sell)**:
  - Se o valor total da opera��o (unit-cost � quantity) for **menor ou igual a R$20.000,00**, nenhum imposto � cobrado.
  - Se o valor total for **maior que R$20.000,00**:
    - Calcula-se o lucro:
      ```
      lucro = (unit-cost - pre�o m�dio ponderado) � quantity
      ```
    - O preju�zo acumulado de vendas anteriores � deduzido dos lucros.
    - O imposto � de **20% sobre o lucro positivo** (ap�s dedu��o dos preju�zos).
  - Caso a opera��o resulte em preju�zo, o valor negativo � acumulado para compensar lucros futuros.
  - Quando todas as a��es s�o vendidas, o pre�o m�dio � reiniciado.

## Compila��o e Execu��o

### Pr�-requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

### Compila��o

Na pasta raiz do projeto, execute:

```bash
dotnet build


### Decis�es T�cnicas e Arquiteturais

- Simplicidade e Direto ao Ponto:

- A solu��o utiliza a entidade TradeOperation diretamente para representar as opera��es, eliminando a 
necessidade de convers�o ou camadas de DTO. Isso simplifica a arquitetura e reduz o c�digo, mantendo o 
foco na l�gica de neg�cio.

Domain-Driven Design (DDD):

- Mesmo com uma abordagem simplificada, a l�gica de neg�cio � isolada na camada de dom�nio. 
A entidade e o servi�o encapsulam as regras de c�lculo de impostos, facilitando futuras extens�es 
sem afetar a camada de apresenta��o.

Clean Code:

- O c�digo foi estruturado para ser leg�vel e de f�cil manuten��o, com coment�rios explicativos e responsabilida
des bem definidas em cada componente.

Uso de STDIN/STDOUT:

- A aplica��o foi projetada para operar em ambientes de linha de comando, permitindo 
redirecionamento de entrada e sa�da. Isso facilita a integra��o com pipelines e testes automatizados.

Arquitetura em Camadas:

- A separa��o entre a camada de dom�nio e a camada de apresenta��o garante que a 
l�gica de neg�cio esteja isolada dos detalhes de interface, permitindo uma evolu��o 
independente de cada camada.