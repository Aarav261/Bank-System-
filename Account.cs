namespace BankSystem;

public class Account
{
    private string _name;
    private decimal _balance;

    public Account(string name, decimal balance)
    {
        this._name = name;
        this._balance = balance;
    }
    
    public String GetName()
    {
        return _name;
    }
    
    public decimal GetBalance()
    {
        return _balance;
    }
    public bool Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Deposit amount must be greater than zero.");
            return false;
        }
        _balance += amount;
        Console.WriteLine("Deposited " + amount + " to " + _name + " account. New balance: " + _balance);
        return true;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Withdrawal amount must be greater than zero.");
            return false;
        }
        if (amount > _balance)
        {
            Console.WriteLine("Insufficient funds for " + _name);
            return false;
        } 
        _balance -= amount;
        Console.WriteLine("Withdrew " + amount + " from "+ _name + " account .New balance: " + _balance);
        return true; 
    }
    public void print()
    {
        Console.WriteLine("Account name: " + _name);
        Console.WriteLine("Account balance: " + _balance);
    }
    
}
