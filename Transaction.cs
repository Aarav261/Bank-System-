namespace BankSystem;

public abstract class Transaction
{
    protected decimal _amount;
    protected bool _success;
    protected bool _executed;
    protected bool _reversed;
    protected  DateTime _dateStamp;

    public Transaction(decimal amount)
    {
        this._amount = amount;

    }

    public abstract bool Executed();

    public abstract bool Success();

    public abstract bool Reversed();

    public virtual DateTime DateStamp()
    {
        return _dateStamp;
    }
    
    public abstract void Print();
    
    
        
    public virtual void Execute()
    {
        _executed = true;
        _dateStamp = DateTime.Now;
    }

    public virtual void Rollback()
    {
        _reversed = true;
        _dateStamp = DateTime.Now;
    }
    
    
    
}