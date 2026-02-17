using System;

namespace BankSystem
{
    public class BankSystem
    {
        public enum MenuOption
        {
            AddAccount,
            Withdraw,
            Deposit,
            Transfer,
            Print,
            PrintTrnsactionsHistory,
            Quit
        }

        public static void Main(string[] args)
        {
            GetTranscationHistory Bank = new GetTranscationHistory();
            MenuOption option;

            do
            {
                option = ReadUserOption();
                switch (option)
                {
                    case MenuOption.AddAccount:
                        DoAddAccount(Bank);
                        break;
                    case MenuOption.Withdraw:
                        DoWithdraw(Bank);
                        break;
                    case MenuOption.Deposit:
                        DoDeposit(Bank);
                        break;
                    case MenuOption.Transfer:
                        DoTransfer(Bank);
                        break;
                    case MenuOption.Print:
                        DoPrint(Bank);
                        break;
                    case MenuOption.PrintTrnsactionsHistory:
                        DoPrintTransactionsHistory(Bank);
                        break;
                    case MenuOption.Quit:
                        Console.WriteLine("Goodbye");
                        break;
                }
            } while (option != MenuOption.Quit);
        }

        private static Account FindAccount(GetTranscationHistory bank)
        {
            Console.Write("Enter account name: ");
            string name = Console.ReadLine().ToUpper();
            Account account = bank.GetAccount(name);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
            }
            return account;
        }

        public static void DoPrintTransactionsHistory(GetTranscationHistory bank)
        {
            bank.PrintTransactionsHistory();
            Console.Write("Do you want to rollback a transaction? (y/n): ");
            try{
            string input = Console.ReadLine().Trim().ToLower();
            if (input == "y")
            {
                Console.Write("Enter transaction number to rollback: ");
                int index = Convert.ToInt32(Console.ReadLine());
                if (index > 0 && index <= bank.GetTransactionCount())
                {
                    Transaction transaction = bank.GetTransactionByIndex(index - 1);
                    if (transaction == null)
                    {
                        Console.WriteLine("Invalid transaction number.");
                        return;
                    }

                    DoRollback(bank, transaction);
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            }catch(Exception e){
                Console.WriteLine("Invalid input. " + e.Message);

            }
        }

        public static void DoAddAccount(GetTranscationHistory bank)
        {
            Console.Write("Enter account name: ");
            string name = Console.ReadLine().ToUpper();
            Console.Write("Enter starting balance: ");
            decimal balance = Convert.ToDecimal(Console.ReadLine());
            Account newAccount = new Account(name, balance);
            bank.AddAccount(newAccount);
            Console.WriteLine($"Account '{name}' added with balance {balance}.");
        }

        public static MenuOption ReadUserOption()
        {
            while (true)
            {
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. Add account");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Print");
                Console.WriteLine("6. Print Transactions History");
                Console.WriteLine("7. Quit");
                Console.Write("Enter your choice (1-7): ");

                try
                {
                    int input = Convert.ToInt32(Console.ReadLine());
                    if (input >= 1 && input <= 7)
                    {
                        return (MenuOption)(input - 1);
                    }

                    Console.WriteLine("Invalid option. Please try again.\n");
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 7.\n");
                }
            }
        }

        public static void DoTransfer(GetTranscationHistory bank)
        {
            Console.WriteLine("Select account to transfer from:");
            Account account = FindAccount(bank);
            if (account == null) return;
            Console.WriteLine("Select account to transfer to:");
            Account account2 = FindAccount(bank);
            if (account2 == null) return;
            Console.Write("Enter amount to transfer: ");
            try
            {
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                TransferTransaction transaction = new TransferTransaction(account, account2, amount);
                bank.ExecuteTransaction(transaction);
                if (transaction.Success())
                {
                    transaction.Print();
                }
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public static void DoDeposit(GetTranscationHistory bank)
        {
            Account account = FindAccount(bank);
            if (account == null) return;
            Console.Write("Enter amount to deposit: ");
            try
            {
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                DepositTransaction transaction = new DepositTransaction(account, amount);
                bank.ExecuteTransaction(transaction);
                if (transaction.Success())
                {
                    transaction.Print();
                }
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public static void DoWithdraw(GetTranscationHistory bank)
        {
            Account account = FindAccount(bank);
            if (account == null) return;
            Console.Write("Enter amount to withdraw: ");

            try
            {
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                WithdrawTransaction transaction = new WithdrawTransaction(account, amount);
                bank.ExecuteTransaction(transaction);
                if (transaction.Success())
                {
                    transaction.Print();
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void DoPrint(GetTranscationHistory bank)
        {
            Account account = FindAccount(bank);
            if (account != null)
            {
                account.print();
            }
        }

        public static void DoRollback(GetTranscationHistory bank, Transaction transaction)
        {
            try
            {
                bank.RollbackTransaction(transaction);
                Console.WriteLine("Transaction rolled back successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during rollback: {ex.Message}");
            }
        }
    }
}
