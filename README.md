# BankSystem (C# / .NET)

A simple console-based banking system implemented in C#.

## Requirements

- .NET SDK 9.0+

Check your SDK:

```bash
dotnet --version
```

## Build

From the project folder:

```bash
dotnet build
```

## Run

```bash
dotnet run
```

If your workspace contains multiple projects, you can run this one explicitly:

```bash
dotnet run --project BankSystem.csproj
```

## Project Structure

- `BankSystem.cs` — Program entry / console UI
- `Bank.cs` — Bank-level operations and account management
- `Account.cs` — Account model and balance operations
- `Transaction.cs` — Base transaction type
- `DepositTransaction.cs` — Deposit implementation
- `WithdrawTransaction.cs` — Withdraw implementation
- `TransferTransaction.cs` — Transfer implementation
