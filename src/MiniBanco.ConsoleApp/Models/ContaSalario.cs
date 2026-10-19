using System;
using MiniBanco.ConsoleApp.Exceptions;

namespace MiniBanco.ConsoleApp.Models
{
    public class ContaSalario : Conta
    {
        public string Empregador { get; private set; }
        public int LimiteSaquesMensais { get; private set; }
        public int SaquesRealizados { get; private set; }

        public ContaSalario(string titular, int numero, decimal saldoInicial, string empregador, int limiteSaquesMensais)
            : base(titular, numero, saldoInicial)
        {
            if (string.IsNullOrWhiteSpace(empregador))
                throw new ArgumentException("O empregador não pode ser vazio.");
            if (limiteSaquesMensais < 0)
                throw new ArgumentException("O limite de saques não pode ser negativo.");
            this.Empregador = empregador;
            this.LimiteSaquesMensais = limiteSaquesMensais;
            this.SaquesRealizados = 0;
        }

        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor do saque deve ser maior que zero.");
            if (valor > this.Saldo)
                throw new SaldoInsuficienteException($"Saldo insuficiente. Saldo atual: R$ {this.Saldo:F2}.");

            this.Saldo -= valor;
            this.SaquesRealizados++;
            RegistrarTransacao("Saque", "Saque", valor);

            if (this.SaquesRealizados > this.LimiteSaquesMensais)
            {
                this.Saldo -= 5m;
                RegistrarTransacao("Tarifa por saque excedente", "Tarifa", 5m);
            }
        }

        public override void AplicarFechamentoMensal()
        {
            this.SaquesRealizados = 0;
        }

        public override string ObterTipoConta() => "Conta Salário";
        public override string ObterAtributoEspecifico() => $"Empregador: {this.Empregador} | Limite de saques: {this.LimiteSaquesMensais}/mês";
    }
}
