using System;
using System.Collections.Generic;
using MiniBanco.ConsoleApp.Exceptions;
using MiniBanco.ConsoleApp.Models;
using MiniBanco.ConsoleApp.Services;

namespace MiniBanco.ConsoleApp
{
    class Program
    {
        static List<Conta> contas = new List<Conta>();
        static MotorAnalise motor = new MotorAnalise();

        static void Main(string[] args)
        {
            bool executando = true;

            while (executando)
            {
                Console.WriteLine("\n====================================================================");
                Console.WriteLine("        MINIBANCO .NET - MOTOR DE GESTÃO DE CONTAS");
                Console.WriteLine("====================================================================");
                Console.WriteLine("1 - Abrir Nova Conta");
                Console.WriteLine("2 - Registrar Movimentação");
                Console.WriteLine("3 - Aplicar Fechamento Mensal");
                Console.WriteLine("4 - Exibir Relatório das Contas");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                try
                {
                    string opcao = Console.ReadLine() ?? "";

                    switch (opcao)
                    {
                        case "1": AbrirConta(); break;
                        case "2": RegistrarMovimentacao(); break;
                        case "3": AplicarFechamento(); break;
                        case "4": ExibirRelatorios(); break;
                        case "0":
                            executando = false;
                            Console.WriteLine("Encerrando o sistema...");
                            break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
                catch (SaldoInsuficienteException ex)
                {
                    Console.WriteLine($"[ERRO] {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[ERRO] {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("[ERRO] Entrada inválida. Digite um número válido.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERRO INESPERADO] {ex.Message}");
                }
            }
        }

        static void AbrirConta()
        {
            Console.Write("Tipo (1-Corrente, 2-Poupança, 3-Salário): ");
            string tipo = Console.ReadLine() ?? "";

            Console.Write("Titular: ");
            string titular = Console.ReadLine() ?? "";

            Console.Write("Número da conta: ");
            int numero = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Saldo inicial: ");
            decimal saldo = decimal.Parse(Console.ReadLine() ?? "0");

            Conta novaConta = null;

            switch (tipo)
            {
                case "1":
                    Console.Write("Limite do cheque especial: ");
                    decimal limite = decimal.Parse(Console.ReadLine() ?? "0");
                    novaConta = new ContaCorrente(titular, numero, saldo, limite);
                    break;
                case "2":
                    Console.Write("Taxa de rendimento mensal (%): ");
                    double taxa = double.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Dia de aniversário (1-31): ");
                    int dia = int.Parse(Console.ReadLine() ?? "1");
                    novaConta = new ContaPoupanca(titular, numero, saldo, taxa, dia);
                    break;
                case "3":
                    Console.Write("Empregador: ");
                    string empregador = Console.ReadLine() ?? "";
                    Console.Write("Limite de saques mensais: ");
                    int limSaques = int.Parse(Console.ReadLine() ?? "0");
                    novaConta = new ContaSalario(titular, numero, saldo, empregador, limSaques);
                    break;
                default:
                    Console.WriteLine("Tipo inválido.");
                    return;
            }

            contas.Add(novaConta);
            Console.WriteLine($"Conta {numero} criada com sucesso!");
        }

        static void RegistrarMovimentacao()
        {
            Console.Write("Número da conta: ");
            int numero = int.Parse(Console.ReadLine() ?? "0");

            Conta conta = null;
            foreach (var c in contas)
            {
                if (c.Numero == numero) { conta = c; break; }
            }

            if (conta == null)
            {
                Console.WriteLine("Conta não encontrada.");
                return;
            }

            Console.Write("Operação (1-Depósito, 2-Saque): ");
            string op = Console.ReadLine() ?? "";

            Console.Write("Valor: ");
            decimal valor = decimal.Parse(Console.ReadLine() ?? "0");

            if (op == "1")
            {
                Console.Write("Descrição do depósito: ");
                string desc = Console.ReadLine() ?? "Depósito";
                conta.Depositar(valor, desc);
                Console.WriteLine("Depósito realizado com sucesso.");
            }
            else if (op == "2")
            {
                conta.Sacar(valor);
                Console.WriteLine("Saque realizado com sucesso.");
            }
            else
            {
                Console.WriteLine("Operação inválida.");
            }
        }

        static void AplicarFechamento()
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta aberta até o momento.");
                return;
            }

            foreach (var c in contas)
            {
                c.AplicarFechamentoMensal();
            }
            Console.WriteLine("Fechamento mensal aplicado em todas as contas (polimorfismo).");
        }

        static void ExibirRelatorios()
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta aberta até o momento.");
                return;
            }

            int i = 1;
            foreach (var c in contas)
            {
                Console.WriteLine($"\n[{i}/{contas.Count}] PROCESSANDO CONTA");
                motor.ExibirRelatorio(c);
                i++;
            }

            Console.WriteLine("\n====================================================================");
            Console.WriteLine("        FIM DO PROCESSAMENTO DE CONTAS");
            Console.WriteLine("====================================================================");
        }
    }
}