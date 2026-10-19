using System;

namespace MiniBanco.ConsoleApp.Models
{
    public class ContaPoupanca : Conta
    {
        public double TaxaRendimentoMensal { get; private set; }
        public int DiaAniversario { get; private set; }

        public ContaPoupanca(string titular, int numero, decimal saldoInicial, double taxaRendimentoMensal, int diaAniversario)
            : base(titular, numero, saldoInicial)
        {
            if (taxaRendimentoMensal < 0)
                throw new ArgumentException("A taxa de rendimento não pode ser negativa.");
            if (diaAniversario < 1 || diaAniversario > 31)
                throw new ArgumentException("Dia de aniversário inválido (1 a 31).");
            this.TaxaRendimentoMensal = taxaRendimentoMensal;
            this.DiaAniversario = diaAniversario;
        }

        public override void AplicarFechamentoMensal()
        {
            decimal rendimento = this.Saldo * (decimal)this.TaxaRendimentoMensal / 100m;
            if (rendimento > 0)
            {
                this.Saldo += rendimento;
                RegistrarTransacao("Rendimento mensal", "Deposito", rendimento);
            }
        }

        public override string ObterTipoConta() => "Conta Poupança";
        public override string ObterAtributoEspecifico() => $"Rendimento: {this.TaxaRendimentoMensal:F1}% ao mês | Aniversário: dia {this.DiaAniversario}";
    }
}
