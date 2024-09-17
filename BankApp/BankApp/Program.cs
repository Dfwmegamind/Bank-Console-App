public class Program
{
    private static Bank bank = new Bank();

    public static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Console Bank Application!");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    Login();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private static void CreateAccount()
    {
        Console.Clear();
        Console.WriteLine("Create Account:");
        Console.Write("Enter Account Type (Current/Savings): ");
        string accountType = Console.ReadLine();
        Console.Write("Enter Account Number: ");
        string accountNumber = Console.ReadLine();
        Console.Write("Enter Account Holder Name: ");
        string accountHolderName = Console.ReadLine();
        Console.Write("Enter PIN: ");
        string pin = Console.ReadLine();

        try
        {
            bank.CreateAccount(accountType, accountNumber, accountHolderName, pin);
            Console.WriteLine("Account created successfully! Press Enter to continue.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.ReadLine();
    }

    private static void Login()
    {
        Console.Clear();
        Console.WriteLine("Login:");
        Console.Write("Enter Account Number: ");
        string accountNumber = Console.ReadLine();
        Console.Write("Enter PIN: ");
        string pin = Console.ReadLine();

        try
        {
            var account = bank.Login(accountNumber, pin);
            AccountMenu(account);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.ReadLine();
    }

    private static void AccountMenu(Account account)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Welcome, {account.AccountHolderName}!");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Transfer");
            Console.WriteLine("4. View Transaction History");
            Console.WriteLine("5. View Balance");
            Console.WriteLine("6. Logout");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Deposit(account);
                    break;
                case "2":
                    Withdraw(account);
                    break;
                case "3":
                    Transfer(account);
                    break;
                case "4":
                    account.ViewTransactionHistory();
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                    break;
                case "5":
                    account.ViewBalance();
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private static void Deposit(Account account)
    {
        Console.Clear();
        Console.Write("Enter amount to deposit: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            try
            {
                account.Deposit(amount);
                bank.SaveAccounts();
                Console.WriteLine("Deposit successful! Press Enter to continue.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount. Press Enter to try again.");
        }

        Console.ReadLine();
    }

    private static void Withdraw(Account account)
    {
        Console.Clear();
        Console.Write("Enter amount to withdraw: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            try
            {
                account.Withdraw(amount);
                bank.SaveAccounts();
                Console.WriteLine("Withdrawal successful! Press Enter to continue.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount. Press Enter to try again.");
        }

        Console.ReadLine();
    }

    private static void Transfer(Account account)
    {
        Console.Clear();
        Console.Write("Enter target account number: ");
        string targetAccountNumber = Console.ReadLine();
        Console.Write("Enter amount to transfer: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            try
            {
                var targetAccount = bank.Login(targetAccountNumber, account.Pin); // Simplified for this example
                account.Transfer(targetAccount, amount);
                bank.SaveAccounts();
                Console.WriteLine("Transfer successful! Press Enter to continue.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount. Press Enter to try again.");
        }

        Console.ReadLine();
    }
}
