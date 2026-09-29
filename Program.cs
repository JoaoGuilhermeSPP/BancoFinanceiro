// See https://aka.ms/new-console-template for more information
using BancoFinanceiro.Models;
using BancoFinanceiro.Service;
using BancoFinanceiro.Data;

Console.WriteLine("Sistema Bancario");

BancoService bancoService = new BancoService();

Console.WriteLine("Carregando contas do arquivo JSON...");
SalvarDados salvarDados = new SalvarDados();


   List<ContaCorrente> todasContas = salvarDados.CarregarContas();
  
while (true)
{
    Console.WriteLine("Digite 1 para criar uma conta");
    Console.WriteLine("Digite 2 para acessar uma conta");
    Console.WriteLine("Digite 3 para listar contas");
    Console.WriteLine("Digite 4 para sair");
    int opcao = int.Parse(Console.ReadLine());

    switch (opcao)
    {
        case 1:
           Console.WriteLine("Digite o seu nome: ");
           string clienteNome = Console.ReadLine();
           clienteNome = new CadastraCliente().CadastrarCliente(clienteNome, "").Nome;
            Console.WriteLine("Digite o seu CPF: ");
            string clienteCpf = Console.ReadLine();
        Console.WriteLine("Digite o numero da conta: ");
            int numeroConta = int.Parse(Console.ReadLine());
       Console.WriteLine("Digite o saldo inicial da conta: ");
            decimal saldoInicial = decimal.Parse(Console.ReadLine());
            
            Console.WriteLine("Conta criada com sucesso!");
            Console.WriteLine($"Conta: {clienteNome}, CPF: {clienteCpf}, Numero da conta: {numeroConta}, Saldo inicial: {saldoInicial}");
            ContaCorrente Novaconta = new BancoService().CriarContaCorrente(numeroConta, saldoInicial, new CadastraCliente().CadastrarCliente(clienteNome, clienteCpf), 1000, 0.01m);
            Novaconta.Saldo = saldoInicial;
            todasContas.Add(Novaconta);
            break;
     

        case 2:
            Console.WriteLine("Digite o numero da conta: ");
            int numeroContaAcessar = int.Parse(Console.ReadLine());
            ContaCorrente contaAcessada = todasContas.FirstOrDefault(c => c.Numero == numeroContaAcessar);
            if(contaAcessada == null)
            {
                Console.WriteLine("Conta não encontrada.");
                break;
            }
            Console.WriteLine($"Conta: {contaAcessada.Numero}, Titular: {contaAcessada.Titular.Nome}, Saldo: {contaAcessada.Saldo}");
            Console.WriteLine("Digite 1 para depositar");
            Console.WriteLine("Digite 2 para sacar");  
            Console.WriteLine("Digite 3 para ver o saldo");
            int opcaoConta = int.Parse(Console.ReadLine());

            if (opcaoConta == 1)
            {
                Console.WriteLine("Digite o valor do deposito: ");
                decimal valorDeposito = decimal.Parse(Console.ReadLine());
                decimal saldoAtualizado = new BancoService().Depositar(0, valorDeposito, new BancoService().CriarContaCorrente(numeroContaAcessar, 0, new CadastraCliente().CadastrarCliente("", ""), 1000, 0.01m));
                Console.WriteLine($"Deposito realizado com sucesso! Saldo atualizado: {saldoAtualizado}");
            }
            else if (opcaoConta == 2)
            {
                Console.WriteLine("Digite o valor do saque: ");
                decimal valorSaque = decimal.Parse(Console.ReadLine());
                try
                {
                    decimal saldoAtualizado = new BancoService().Sacar(0, valorSaque, new BancoService().CriarContaCorrente(numeroContaAcessar, 0, new CadastraCliente().CadastrarCliente("", ""), 1000, 0.01m));
                    Console.WriteLine($"Saque realizado com sucesso! Saldo atualizado: {saldoAtualizado}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (opcaoConta == 3)
            {
                decimal saldoAtual = new BancoService().VerificarSaldo(0, new BancoService().CriarContaCorrente(numeroContaAcessar, 0, new CadastraCliente().CadastrarCliente("", ""), 1000, 0.01m));
                Console.WriteLine($"Saldo atual: {saldoAtual}");
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
           
        break;

        case 3:
        
           BancoService.ListarContas(todasContas.ToArray());
     
        break;   
             case 4:
            Console.WriteLine("Saindo do sistema...");
            salvarDados.SalvarContas(todasContas.ToArray());
        return;

        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }
}

