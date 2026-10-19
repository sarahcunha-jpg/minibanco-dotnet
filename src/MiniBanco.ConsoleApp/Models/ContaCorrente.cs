using System;
using MiniBanco.ConsoleApp.Exceptions;

namespace MiniBanco.ConsoleApp.Models
{
    public class ContaCorrente : Conta
    {
        public decimal LimiteChequeEspecial { get; private set; }

        public ContaCorrente(string titular, int numero, decimal saldoInicial, decimal limiteChequeEspecial)
            : base(titular, numero, saldoInicial)
        {
            if (limiteChequeEspecial < 0)
                throw new ArgumentException("O limite do cheque especial não pode ser negativo.");

            this.LimiteChequeEspecial = limiteChequeEspecial;
        }

        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor do saque deve ser maior que zero.");

            decimal limiteDisponivel = this.Saldo + this.LimiteChequeEspecial;

            if (valor > limiteDisponivel)
                throw new SaldoInsuficienteException(
                    $"Saldo + limite insuficiente. Disponível: R$ {limiteDisponivel:F2}.");

            this.Saldo -= valor;
            RegistrarTransacao("Saque", "Saque", valor);
        }

        public override void AplicarFechamentoMensal()
        {
            this.Saldo -= 10m;
            RegistrarTransacao("Tarifa de manutenção", "Tarifa", 10m);
        }

        public override string ObterTipoConta() => "Conta Corrente";

        public override string ObterAtributoEspecifico() =>
            $"Limite Cheque Especial: R$ {this.LimiteChequeEspecial:F2}";
    }
}