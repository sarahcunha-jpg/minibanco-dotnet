# 💰 MiniBanco .NET — Motor de Gestão de Contas Bancárias

> **Mini-Projeto Avaliativo — Módulo 2 — Semana 07**
> Desenvolvedor Back-End .NET · Console Application em C#

---

## 🎯 Objetivo

Construir uma **engine de gestão de contas bancárias** via Console Application em C# que gerencie contas de 3 tipos (`ContaCorrente`, `ContaPoupanca`, `ContaSalario`), com regras próprias de saque e fechamento mensal, registre transações, calcule o índice de saúde financeira (0% a 100%), classifique a conta e emita um relatório detalhado com recomendações para a gerência.

---

## 📹 Vídeo de apresentação

> 🔗 **[Inserir aqui o link do vídeo — Google Drive (leitor) ou YouTube não listado]**

---

## 🧠 Regras de Negócio

### Tipos de conta

| Tipo | Atributos específicos | Regra de saque | Fechamento mensal |
|---|---|---|---|
| **ContaCorrente** | Limite do cheque especial | Pode sacar até `Saldo + LimiteChequeEspecial` | Registra **tarifa de manutenção de R$ 10,00** |
| **ContaPoupanca** | Taxa de rendimento mensal (%) e dia de aniversário | Saque limitado ao saldo | Registra **depósito de rendimento** = `Saldo × Taxa / 100` |
| **ContaSalario** | Empregador e limite de saques mensais | Saque limitado ao saldo; ao exceder o limite mensal cobra **tarifa de R$ 5,00** | **Zera a contagem de saques** do mês |

### Pontuação por transação

| Tipo | Pontos |
|---|---|
| `Deposito` | **10** |
| `Saque` | **5** |
| `Tarifa` | **0** |

### Cálculo do índice de saúde
> ⚠️ Divisão com **cast para `double`** e proteção contra **divisão por zero**.

### Classificação final

| Faixa | Classificação | Ação corporativa |
|---|---|---|
| **90% – 100%** | 🟢 Conta Saudável | Oferecer investimentos e avaliar aumento de limite. |
| **60% – 89%** | 🟡 Conta em Atenção | Contato da gerência para orientação financeira e revisão de tarifas. |
| **0% – 59%** | 🔴 Conta em Risco | Bloquear aumento de limite e abrir análise de crédito. |

---

## 🗂 Estrutura do projeto
minibanco-dotnet/
├── src/
│ └── MiniBanco.ConsoleApp/
│ ├── Exceptions
│ ├── Models/
│ ├──Services/
│ ├── MiniBanco.ConsoleApp.csproj
│ └── Program.cs
├── .gitignore
└── README.md

---

## ▶️ Como executar

### Pré-requisitos
- [.NET SDK 9.0+](https://dotnet.microsoft.com/download)

### Passos

```bash
git clone https://github.com/sarahcunha-jpg/minibanco-dotnet.git
cd minibanco-dotnet

''Rodar no Terminal :cd src/MiniBanco.ConsoleApp              
                     dotnet run

Ator : Sara Hellen