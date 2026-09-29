# Banco Financeiro

Aplicação bancária de linha de comando desenvolvida em C# e .NET 9. O projeto permite cadastrar contas correntes, localizá-las pelo número e armazenar os registros em um arquivo JSON.

> **Estado do projeto:** depósito, saque e consulta de saldo ainda precisam de correção para operar sobre a conta acessada. Veja [Limitações conhecidas](#limitações-conhecidas).

## Requisitos

- .NET SDK 9.0 ou superior
- Terminal ou console compatível com a execução de aplicações .NET

O projeto não utiliza pacotes externos; emprega recursos incluídos no .NET, como `System.Text.Json`.

## Como executar

Na pasta do projeto, execute:

```bash
dotnet restore
dotnet run --project BancoFinanceiro.csproj
```

## Menu da aplicação

Ao iniciar, a aplicação carrega as contas de `contas.json`, caso o arquivo exista, e apresenta estas opções:

1. **Criar uma conta:** solicita nome, CPF, número da conta e saldo inicial.
2. **Acessar uma conta:** localiza uma conta pelo número e oferece opções de depósito, saque e consulta de saldo.
3. **Listar contas:** exibe o número da conta e o nome do titular.
4. **Sair:** grava a lista de contas em `contas.json` e encerra a aplicação.

A conta recém-criada fica em memória até a opção 4 ser selecionada. Encerrar o processo sem usar essa opção pode descartar alterações não salvas.

## Persistência dos dados

Os dados são serializados em formato JSON indentado. O arquivo é lido e gravado no diretório de trabalho da aplicação, usando o nome `contas.json`.

Exemplo da estrutura de uma conta:

```json
{
	"Limite": 1000,
	"Taxa": 0.01,
	"Numero": 1001,
	"Saldo": 500.00,
	"Titular": {
		"Nome": "Nome de exemplo",
		"Cpf": "00000000000"
	}
}
```

O arquivo contém uma lista de contas. Cada registro inclui número, saldo, limite, taxa e dados do titular. O saldo é um valor decimal armazenado individualmente em cada conta.

## Organização do projeto

```text
BancoFinanceiro/
├── Data/
│   └── SalvarDados.cs
├── Models/
│   ├── Cliente.cs
│   ├── Conta.cs
│   └── ContaCorrente.cs
├── Service/
│   └── BancoService.cs
├── BancoFinanceiro.csproj
├── Program.cs
└── contas.json
```

- **`Program.cs`**: ponto de entrada; apresenta o menu e coordena as operações.
- **`Models/Cliente.cs`**: representa o titular da conta, com nome e CPF.
- **`Models/Conta.cs`**: classe base abstrata com número, saldo e titular.
- **`Models/ContaCorrente.cs`**: especializa a conta com limite e taxa.
- **`Service/BancoService.cs`**: contém operações de criação, depósito, saque, consulta e listagem, além do cadastro de clientes.
- **`Data/SalvarDados.cs`**: carrega e grava a lista de contas em JSON.
- **`contas.json`**: arquivo de dados utilizado pela aplicação.

## Limitações conhecidas

- No fluxo atual, depósito, saque e consulta criam uma conta temporária com saldo zero, em vez de operar sobre a conta localizada. Por isso, as operações não atualizam corretamente o saldo da conta cadastrada; saques também podem informar saldo insuficiente indevidamente.
- A listagem mostra o número da conta e o nome do titular, mas não exibe o saldo.
- As entradas numéricas usam `int.Parse` e `decimal.Parse`. Valores inválidos ou vazios podem encerrar a aplicação com uma exceção.
- As contas novas são salvas ao selecionar a opção 4, não imediatamente após o cadastro.
- A validação de CPF e a prevenção de números de conta duplicados não estão implementadas.
- `ExcluirCliente` é apenas um placeholder e não realiza exclusão.

## Privacidade dos dados

O arquivo `contas.json` contém nomes, CPFs e saldos. Antes de publicar o projeto em um repositório público, substitua esses registros por dados fictícios ou remova o arquivo. A aplicação armazena os dados sem criptografia.
