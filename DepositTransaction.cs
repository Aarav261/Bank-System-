namespace BankSystem;

public class DepositTransaction : Transaction
{
    Account _account;
    
    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        this._account = account;
        this._amount = amount;
    }
    
    public override void Execute()
    {
        if (_executed)
            throw new InvalidOperationException("Transaction has already been executed.");
        base.Execute();
        _success = _account.Deposit(_amount);
        _executed = true;
    }

    public override void Print()
    {
        Console.WriteLine("Deposit Transaction:");
        Console.WriteLine("Account: " + _account.GetName());
        Console.WriteLine("Amount: " + _amount);
        Console.WriteLine($"Executed: {_executed} | Success: {_success} | Reversed: {_reversed}");
        Console.WriteLine();
    }
    
    public override void Rollback()
    {
        if (!_executed)
            throw new InvalidOperationException("Transaction has not been executed yet.");

        if (_reversed)
            throw new InvalidOperationException("Transaction has already been reversed.");
        base.Rollback();
        _account.Withdraw(_amount);
        _reversed = true;
    }
    
    public override bool Executed()
    {
        return _executed;
    }
    
    public override bool Success()
    {
        return _success;
    }
    
    public override bool Reversed()
    {
        return _reversed;
    }
}