using System.Text.Json;

public class DebtService
{
    // Path for the debt transaction file
    private readonly string _filePath = Path.Combine(
            @"C:\Users\Ayush\Desktop\C#\ayushChaudhary\ayushChaudhary\bin\Debug\net8.0-windows10.0.19041.0\win10-x64",
            "debt_transaction.json");

    public List<DebtTransaction> DebtTransactions { get; set; }

    public DebtService()
    {
        DebtTransactions = LoadDebtTransactions();
    }

    // Add a new debt transaction
    public string AddDebtTransaction(DebtTransaction transaction)
    {
        if (transaction.Amount <= 0)
        {
            return "Amount must be greater than zero.";
        }

        DebtTransactions.Add(transaction);
        SaveDebtTransactions(); // Save updated list to JSON file
        return "Debt transaction added successfully.";
    }

    // Load all debt transactions from the JSON file
    private List<DebtTransaction> LoadDebtTransactions()
    {
        if (!File.Exists(_filePath))
        {
            return new List<DebtTransaction>(); // Return an empty list if the file doesn't exist
        }

        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<DebtTransaction>>(json) ?? new List<DebtTransaction>();
    }

    // Save all debt transactions to the JSON file
    public void SaveDebtTransactions()
    {
        string json = JsonSerializer.Serialize(DebtTransactions, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    // Get all debt transactions
    public List<DebtTransaction> GetAllDebtTransactions()
    {
        return DebtTransactions;
    }

    // Mark debt as cleared
    public string MarkDebtAsCleared(DebtTransaction transaction)
    {
        // Update the status of the debt to cleared
        var debt = DebtTransactions.FirstOrDefault(d => d == transaction);
        if (debt != null)
        {
            debt.Status = "Cleared";
            SaveDebtTransactions(); // Save updated list to file
            return "Debt cleared successfully.";
        }
        return "Debt not found.";
    }

    // Filter debt transactions by a certain criteria (e.g., due date, source, etc.)
    public List<DebtTransaction> FilterTransactions(Func<DebtTransaction, bool> predicate)
    {
        return DebtTransactions.Where(predicate).ToList();
    }
}