public abstract class Account
{
    public string AccountNumber { get; private set; }
    public string AccountHolderName { get; private set; }
    protected decimal Balance { get; set; }
    public string Pin { get; private set; }
    public List<string> TransactionHistory { get; private set; }

    protected Account(string accountNumber, string accountHolderName, string pin)
    {
        AccountNumber = accountNumber;
        AccountHolderName = accountHolderName;
        Pin = pin;
        Balance = 0;
        TransactionHistory = new List<string>();
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new InvalidOperationException("Deposit amount must be positive.");
        }

        Balance += amount;
        TransactionHistory.Add($"Deposited: {amount}. New Balance: {Balance}");
    }

    public abstract void Withdraw(decimal amount);

    public void Transfer(Account targetAccount, decimal amount)
    {
        Withdraw(amount);
        targetAccount.Deposit(amount);
        TransactionHistory.Add($"Transferred: {amount} to {targetAccount.AccountNumber}. New Balance: {Balance}");
    }

    public void ViewTransactionHistory()
    {
        Console.WriteLine($"Transaction History for {AccountHolderName} (Account: {AccountNumber}):");
        foreach (var transaction in TransactionHistory)
        {
            Console.WriteLine(transaction);
        }
    }

    public void ViewBalance()
    {
        Console.WriteLine($"Account Balance for {AccountHolderName} (Account: {AccountNumber}): {Balance}");
    }
}
public class CurrentAccount : Account
{
    private const decimal MinimumBalance = 5000;

    public CurrentAccount(string accountNumber, string accountHolderName, string pin)
        : base(accountNumber, accountHolderName, pin) { }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new InvalidOperationException("Withdrawal amount must be positive.");
        }

        if (Balance - amount < MinimumBalance)
        {
            throw new InvalidOperationException($"Insufficient balance. Current account must maintain a minimum balance of {MinimumBalance}.");
        }

        Balance -= amount;
        TransactionHistory.Add($"Withdrew: {amount}. New Balance: {Balance}");
    }
}

public class SavingsAccount : Account
{
    public SavingsAccount(string accountNumber, string accountHolderName, string pin)
        : base(accountNumber, accountHolderName, pin) { }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new InvalidOperationException("Withdrawal amount must be positive.");
        }

        if (Balance < amount)
        {
            throw new InvalidOperationException("Insufficient balance.");
        }

        Balance -= amount;
        TransactionHistory.Add($"Withdrew: {amount}. New Balance: {Balance}");
    }
}

