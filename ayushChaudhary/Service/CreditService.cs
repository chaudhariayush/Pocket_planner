using System.Text.Json;

namespace ayushChaudhary.Service
{
    public class CreditService
    {
        private readonly string _filePath = Path.Combine(
            @"C:\Users\Ayush\Desktop\C#\ayushChaudhary\ayushChaudhary\bin\Debug\net8.0-windows10.0.19041.0\win10-x64",
            "credit_transaction.json");

        public List<CreditTransaction> GetCredits()
        {
            if (!File.Exists(_filePath))
            {
                return new List<CreditTransaction>(); // Return empty list if the file doesn't exist
            }

            var jsonString = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<CreditTransaction>>(jsonString) ?? new List<CreditTransaction>();
        }

        // Save Credit Transactions to JSON file
        public void SaveCredits(List<CreditTransaction> credits)
        {
            var jsonString = JsonSerializer.Serialize(credits, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, jsonString);
        }

        // Read Total Inflow (Modify this to no longer use _inflowFilePath)
        public decimal GetTotalInflow()
        {
            // Instead of reading from a file, you can calculate or store the inflow in another way
            return 0m; // Return 0 or modify as necessary
        }

        // Save Total Inflow (Modify this to no longer use _inflowFilePath)
        public void SaveTotalInflow(decimal totalInflow)
        {
            // You can update total inflow in another way, such as in a database or in-memory storage
        }

        // New method to add to total inflow
        public void AddToTotalInflow(decimal amount)
        {
            decimal currentTotalInflow = GetTotalInflow();
            decimal newTotalInflow = currentTotalInflow + amount;

            // Save updated total inflow
            SaveTotalInflow(newTotalInflow);

            // Debugging: Print the updated total inflow
            Console.WriteLine($"Total Inflow updated: {newTotalInflow}");
        }

        // Clear debt by marking the credit transaction as "Cleared"
        public void ClearDebt(int creditId)
        {
            var credits = GetCredits();
            var creditToClear = credits.FirstOrDefault(c => c.Id == creditId);

            if (creditToClear != null && creditToClear.Status != "Cleared")
            {
                creditToClear.Status = "Cleared"; // Mark as cleared

                // Debugging: Print the amount added to total inflow
                Console.WriteLine($"Clearing debt: Adding {creditToClear.Amount} to total inflow.");

                // Update total inflow when debt is cleared
                AddToTotalInflow(creditToClear.Amount);

                SaveCredits(credits); // Save the updated list of credits
            }
        }

        // Optional: If you want to implement a Delete method as well
        public void DeleteCreditTransaction(int creditId)
        {
            var credits = GetCredits();
            var creditToDelete = credits.FirstOrDefault(c => c.Id == creditId);

            if (creditToDelete != null)
            {
                credits.Remove(creditToDelete);
                SaveCredits(credits); // Save the updated list after deletion
            }
        }
    }
}
