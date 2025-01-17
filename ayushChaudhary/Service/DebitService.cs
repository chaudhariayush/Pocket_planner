using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ayushChaudhary.Models;
using ayushChaudhary.Service;

public class DebitService
{
    private readonly string _filePath = Path.Combine(
        @"C:\Users\Ayush\Desktop\C#\ayushChaudhary\ayushChaudhary\bin\Debug\net8.0-windows10.0.19041.0\win10-x64",
        "debit_transaction.json");

    private decimal totalBalance = 1000; // Initial balance for demonstration

    // Inject CreditService to access credits
    private readonly CreditService _creditService;

    public DebitService(CreditService creditService)
    {
        _creditService = creditService;
    }

    // Read Debit Transactions from JSON file
    public List<DebitTransaction> GetDebits()
    {
        if (!File.Exists(_filePath))
        {
            return new List<DebitTransaction>(); // Return empty list if the file doesn't exist
        }

        var jsonString = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<DebitTransaction>>(jsonString) ?? new List<DebitTransaction>();
    }

    // Save Debit Transactions to JSON file
    public void SaveDebits(List<DebitTransaction> debits)
    {
        var jsonString = JsonSerializer.Serialize(debits, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, jsonString);
    }

    // Get total balance
    public decimal GetTotalBalance()
    {
        return totalBalance;
    }

    // Add a new Debit Transaction and update the balance
    public string AddDebitTransaction(DebitTransaction debit)
    {
        var credits = _creditService.GetCredits();
        var totalCredits = credits.Sum(c => c.Amount); // Get total credits from CreditService

        if (debit.DeductedAmount > totalCredits)
        {
            return "Insufficient credits to perform this debit!"; // If credits are insufficient
        }

        // Proceed with the debit transaction
        totalBalance -= debit.DeductedAmount; // Subtract the debit amount from total balance
        var debits = GetDebits();
        debits.Add(debit);
        SaveDebits(debits); // Save the updated list of debit transactions

        // Deduct the debit amount from total credits
        var creditToUpdate = credits.FirstOrDefault(); // Assuming only one credit entry for simplicity
        if (creditToUpdate != null)
        {
            creditToUpdate.Amount -= debit.DeductedAmount; // Subtract the debit amount from credit
            _creditService.SaveCredits(credits); // Save the updated credits back to the file
        }

        return "Debit transaction successful.";
    }

    // Delete a Debit Transaction
    public string DeleteDebitTransaction(int id)
    {
        var debits = GetDebits();
        var debitToDelete = debits.FirstOrDefault(d => d.Id == id);

        if (debitToDelete != null)
        {
            debits.Remove(debitToDelete);
            SaveDebits(debits); // Save the updated list
            totalBalance += debitToDelete.DeductedAmount; // Restore the balance
            return "Debit transaction deleted successfully.";
        }

        return "Debit transaction not found.";
    }
}
