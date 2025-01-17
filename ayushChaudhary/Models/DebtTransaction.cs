public class DebtTransaction
{
    public decimal Amount { get; set; }      // Amount of the debt
    public DateTime DueDate { get; set; }    // Due date of the debt
    public string Source { get; set; }       // Source of the debt (e.g., Loan, Credit Card)
    public string Notes { get; set; }        // Optional notes about the debt
    public string Tags { get; set; }         // Optional tags (e.g., "Loan", "Mortgage")
    public string Status { get; set; } = "Pending";  // Status: Pending or Cleared
}