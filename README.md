# 💰 MiniBanco .NET — Motor de Gestão de Contas Bancárias

> **Mini-Projeto Avaliativo — Módulo 2 — Semana 07**  
> Desenvolvedor Back-End .NET · Console Application em C#

---

## 🎯 Objetivo

Construir uma **engine de gestão de contas bancárias** via Console Application em C# que:

- Gerencie contas de 3 tipos (`ContaCorrente`, `ContaPoupanca`, `ContaSalario`), cada uma com regras próprias de saque e fechamento mensal.
- Registre cada movimentação como uma **Transacao** (`Deposito`, `Saque` ou `Tarifa`).
- Calcule o **índice de saúde financeira** de cada conta (0% a 100%).
- Classifique a conta em **Saudável**, **Em Atenção** ou **Em Risco**.
- Emita um relatório detalhado com **pendências** e **recomendações** para a gerência.

---

## 📹 Vídeo de apresentação

> 🔗 ** https://drive.google.com/drive/folders/16cuKfO-uZ3og_aIv_2sSLjAIITDfCvG3 **

---

## ▶️ Como Executar (Início Rápido)

### Pré-requisitos
- Ter o [.NET SDK 9.0+](https://dotnet.microsoft.com/download) instalado na máquina.
- Ter o Git instalado para clonar o repositório.

### Passos

**1. Clone o repositório:**
```bash
git clone https://github.com/sarahcunha-jpg/minibanco-dotnet.git
```

**2. Acesse a pasta do projeto:**
```bash
cd minibanco-dotnet/src/MiniBanco.ConsoleApp
```

**3. Execute a aplicação:**
```bash
dotnet run
```

💡 *O menu interativo aparecerá no terminal. Navegue pelas opções digitando o número correspondente e pressionando `Enter`.*

---

## 🧠 Regras de Negócio

### 🏦 Tipos de conta

| Tipo | Atributos Específicos | Regra de Saque | Fechamento Mensal |
| :--- | :--- | :--- | :--- |
| **ContaCorrente** | Limite do cheque especial | Pode sacar até `Saldo + LimiteChequeEspecial` | Registra **tarifa de manutenção de R$ 10,00** |
| **ContaPoupanca** | Taxa de rendimento mensal (%) e dia de aniversário | Saque limitado ao saldo | Registra **depósito de rendimento** = `Saldo × Taxa / 100` |
| **ContaSalario** | Empregador e limite de saques mensais | Saque limitado ao saldo; ao exceder o limite mensal cobra **tarifa de R$ 5,00** | **Zera a contagem de saques** do mês |

### 🎯 Pontuação por transação

| Tipo | Pontos |
| :--- | :---: |
| `Deposito` | **+10** |
| `Saque` | **+5** |
| `Tarifa` | **0** |

### 📊 Cálculo do Índice de Saúde

```text
Percentual (%) = (PontuaçãoObtida / (TotalTransações × 10)) × 100
```
> ⚠️ *Divisão com **cast para `double`** e proteção contra **divisão por zero**.*

### 🚦 Classificação Final

| Faixa | Classificação | Ação Corporativa |
| :---: | :--- | :--- |
| **90% – 100%** | 🟢 Conta Saudável | Oferecer investimentos e avaliar aumento de limite. |
| **60% – 89%** | 🟡 Conta em Atenção | Contato da gerência para orientação financeira e revisão de tarifas. |
| **0% – 59%** | 🔴 Conta em Risco | Bloquear aumento de limite e abrir análise de crédito. |

---

## 🗂 Estrutura do Projeto

```text
minibanco-dotnet/
├── src/
│   └── MiniBanco.ConsoleApp/
│       ├── Exceptions/
│       │   └── SaldoInsuficienteException.cs
│       ├── Models/
│       │   ├── Conta.cs
│       │   ├── ContaCorrente.cs
│       │   ├── ContaPoupanca.cs
│       │   ├── ContaSalario.cs
│       │   └── Transacao.cs
│       ├── Services/
│       │   └── MotorAnalise.cs
│       ├── Program.cs
│       └── MiniBanco.ConsoleApp.csproj
├── .gitignore
└── README.md
```

---

## 🧩 Conceitos de POO Aplicados

- 🧬 **Herança** — `ContaCorrente`, `ContaPoupanca` e `ContaSalario` herdam de `Conta`.
- 🎭 **Polimorfismo** — `Sacar()` e `AplicarFechamentoMensal()` são `virtual` e sobrescritos (`override`).
- 🔒 **Encapsulamento** — `Saldo` tem `protected set`.
- 🧱 **Composição** — `Conta` contém `List<Transacao>`.
- 🚨 **Exceção customizada** — `SaldoInsuficienteException`.
- ⚙️ **Construtor com `this`** — atribuição explícita das propriedades.

---

## 🚫 Restrições Respeitadas

- ❌ **Sem LINQ** — toda varredura usa `foreach` + `if`.
- ✅ **Cast `(double)`** na divisão do índice.
- ✅ **Tratamento de divisão por zero**.
- ✅ **Exceções tratadas** com mensagem amigável ao usuário.

---

## 📈 Melhorias Futuras (Limitações)

- 💾 Persistência em banco de dados (SQLite / SQL Server).
- 📄 Exportação de relatório para arquivo `.txt`/`.csv`.
- 🔐 Autenticação de gerente.

---

Autora: Sara Hellen 
