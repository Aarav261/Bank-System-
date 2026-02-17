namespace BankSystem;

public class TransferTransaction : Transaction
{
    Account _fromAccount;
    Account _toAccount;

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        this._fromAccount = fromAccount;
        this._toAccount = toAccount;
        this._amount = amount;
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

    public override void Print()
    {
        Console.WriteLine(
            $"Transferred ${_amount} from {_fromAccount.GetName()}'s account to {_toAccount.GetName()}'s account."
        );
        Console.WriteLine();
      
    }
    
    public override void Execute()
    {
        if (_executed)
            throw new InvalidOperationException("Transaction has already been executed.");
    
        base.Execute();
        if (_fromAccount.Withdraw(_amount))
        {
            _toAccount.Deposit(_amount);
            _success = true;
        }
    }
    
    public override void Rollback()
    {
        if (!_executed)
            throw new InvalidOperationException("Transaction has not been executed yet.");
        if (_reversed)
            throw new InvalidOperationException("Transaction has already been reversed.");
        base.Rollback();
        if (_success)
        {
            _toAccount.Withdraw(_amount);
            _fromAccount.Deposit(_amount);
            _reversed = true;
        }
    }
    
    
    
    
    

}
