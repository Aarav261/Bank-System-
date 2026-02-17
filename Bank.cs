namespace BankSystem;

public class GetTranscationHistory
{
    private List<Account> _accounts = new List<Account>();
    private List<Transaction> _transactions = new List<Transaction>();
    public void AddAccount(Account account)
    {
        _accounts.Add(account);
        Console.WriteLine($"Account for {account.GetName()} added to the bank.");
    }
    
    public Account GetAccount(String name)
    {
        foreach (Account account in _accounts)
        {
            if (account.GetName() == name)
            {
                return account;
            }
        }return null;
    }

    
    public void ExecuteTransaction(Transaction transaction)
    {
        transaction.Execute();
        _transactions.Add(transaction);
    }

    public void RollbackTransaction(Transaction transaction)
    {
        transaction.Rollback();
    }
    
    public void PrintTransactionsHistory()
    {
        Console.WriteLine("Transaction History:");
        for (int i = 0; i < _transactions.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            _transactions[i].Print();
            Console.WriteLine($"Timestamp: {_transactions[i].DateStamp()}");
            Console.WriteLine($"Executed: {_transactions[i].Executed()} | Success: {_transactions[i].Success()} | Reversed: {_transactions[i].Reversed()}");
        }
    }
    
    public Transaction GetTransactionByIndex(int index)
    {
        if (index >= 0 && index < _transactions.Count)
            return _transactions[index];
        return null;
    }
  

        public int GetTransactionCount()
        {
            return _transactions.Count;
        }





}