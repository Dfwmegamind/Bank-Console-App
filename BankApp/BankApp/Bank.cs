using Newtonsoft.Json;

public class Bank
{
    private List<Account> accounts;
    private readonly string dataFile = "accounts.json";

    public Bank()
    {
        LoadAccounts();
    }

    public void CreateAccount(string accountType, string accountNumber, string accountHolderName, string pin)
    {
        if (accounts.Any(a => a.AccountNumber == accountNumber))
        {
            throw new InvalidOperationException("Account number already exists.");
        }

        Account account;
        if (accountType.ToLower() == "current")
        {
            account = new CurrentAccount(accountNumber, accountHolderName, pin);
        }
        else if (accountType.ToLower() == "savings")
        {
            account = new SavingsAccount(accountNumber, accountHolderName, pin);
        }
        else
        {
            throw new InvalidOperationException("Invalid account type.");
        }

        accounts.Add(account);
        SaveAccounts();
    }

    public Account Login(string accountNumber, string pin)
    {
        var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber && a.Pin == pin);
        if (account == null)
        {
            throw new InvalidOperationException("Invalid account number or PIN.");
        }
        return account;
    }

    public void SaveAccounts()
    {
        try
        {
            var json = JsonConvert.SerializeObject(accounts, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            File.WriteAllText(dataFile, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving accounts: {ex.Message}");
        }
    }

    public void LoadAccounts()
    {
        try
        {
            if (File.Exists(dataFile))
            {
                var json = File.ReadAllText(dataFile);
                accounts = JsonConvert.DeserializeObject<List<Account>>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
            }
            else
            {
                accounts = new List<Account>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading accounts: {ex.Message}");
            accounts = new List<Account>();
        }
    }
}
