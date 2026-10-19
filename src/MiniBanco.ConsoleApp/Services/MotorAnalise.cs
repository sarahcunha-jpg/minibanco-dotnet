using System;
using System.Collections.Generic;
using MiniBanco.ConsoleApp.Models;

namespace MiniBanco.ConsoleApp.Services
{
    public class MotorAnalise
    {
        public int CalcularPontuacaoObtida(Conta conta)
        {
            int pontos = 0;
            foreach (var t in conta.Extrato)
            {
                if (t.Tipo == "Deposito") pontos += 10;
                else if (t.Tipo == "Saque") pontos += 5;
                else if (t.Tipo == "Tarifa") pontos += 0;
            }
            return pontos;
        }

        public int CalcularPontuacaoMaxima(Conta conta)
        {
            return conta.Extrato.Count * 10;
        }

        public double CalcularIndiceSaude(Conta conta)
        {
            int obtida = CalcularPontuacaoObtida(conta);
            int maxima = CalcularPontuacaoMaxima(conta);
            if (maxima == 0) return 0d;
            return ((double)obtida / (double)maxima) * 100.0;
        }

        public string ClassificarConta(double percentual)
        {
            if (percentual >= 90.0) return "CONTA SAUDÁVEL";
            if (percentual >= 60.0) return "CONTA EM ATENÇÃO";
            return "CONTA EM RISCO";
        }

        public string ObterAcaoCorporativa(double percentual)
        {
            if (percentual >= 90.0)
                return "Oferecer investimentos e avaliar aumento de limite.";
            if (percentual >= 60.0)
                return "Contato da gerência para orientação financeira e revisão de tarifas.";
            return "Bloquear aumento de limite e abrir análise de crédito.";
        }

        public List<Transacao> ObterTarifas(Conta conta)
        {
            var lista = new List<Transacao>();
            foreach (var t in conta.Extrato)
                if (t.Tipo == "Tarifa") lista.Add(t);
            return lista;
        }

        public List<Transacao> ObterSaques(Conta conta)
        {
            var lista = new List<Transacao>();
            foreach (var t in conta.Extrato)
                if (t.Tipo == "Saque") lista.Add(t);
            return lista;
        }

        public void ExibirRelatorio(Conta conta)
        {
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("> DADOS DA CONTA:");
            Console.WriteLine($"  - Tipo: {conta.ObterTipoConta()}");
            Console.WriteLine($"  - Titular: {conta.Titular} | Conta: {conta.Numero}");
            Console.WriteLine($"  - Atributo Específico: {conta.ObterAtributoEspecifico()}");
            Console.WriteLine($"  - Saldo Atual: R$ {conta.Saldo:F2}");
            Console.WriteLine();

            Console.WriteLine($"> MOVIMENTAÇÕES REGISTRADAS ({conta.Extrato.Count} TRANSAÇÕES):");
            foreach (var t in conta.Extrato)
            {
                int pts = 0;
                string marcador = "[OK]";

                if (t.Tipo == "Deposito") { pts = 10; marcador = "[OK]"; }
                else if (t.Tipo == "Saque") { pts = 5; marcador = "[ ! ]"; }
                else if (t.Tipo == "Tarifa") { pts = 0; marcador = "[ X ]"; }

                Console.WriteLine($"  {marcador} {t.Descricao,-26} {t.Tipo}: R$ {t.Valor:F2} ({pts} pts)");
            }
            Console.WriteLine();

            int obtida = CalcularPontuacaoObtida(conta);
            int maxima = CalcularPontuacaoMaxima(conta);
            double percentual = CalcularIndiceSaude(conta);
            string classificacao = ClassificarConta(percentual);

            Console.WriteLine("> RESUMO DA PONTUAÇÃO:");
            Console.WriteLine($"  - Pontuação Atingida: {obtida} de {maxima} pontos possíveis");
            Console.WriteLine($"  - Índice de Saúde: {percentual:F1}%");
            Console.WriteLine($"  - Classificação Final: [ {classificacao} ]");
            Console.WriteLine();

            Console.WriteLine("> RELATÓRIO DE PENDÊNCIAS E RECOMENDAÇÕES DA GERÊNCIA:");

            var tarifas = ObterTarifas(conta);
            var saques = ObterSaques(conta);

            if (tarifas.Count == 0 && saques.Count == 0)
            {
                Console.WriteLine("  🟢 Nenhuma pendência identificada. Oferecer produtos de investimento ao cliente.");
            }
            else
            {
                if (tarifas.Count > 0)
                {
                    Console.WriteLine("  🔴 ITENS CRÍTICOS (TARIFAS):");
                    foreach (var t in tarifas)
                        Console.WriteLine($"    - {t.Descricao}: Avaliar isenção ou migração para pacote de serviços.");
                }

                if (saques.Count > 0)
                {
                    Console.WriteLine("  🟡 ITENS DE ATENÇÃO (SAQUES):");
                    foreach (var t in saques)
                        Console.WriteLine($"    - Saque de R$ {t.Valor:F2}: Acompanhar movimentação e contatar o cliente.");
                }
            }

            Console.WriteLine($"\n  ➜ Ação corporativa: {ObterAcaoCorporativa(percentual)}");
            Console.WriteLine("--------------------------------------------------------------------");
        }
    }
}