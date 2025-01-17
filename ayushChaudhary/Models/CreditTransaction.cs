namespace ayushChaudhary.Service
{
    public class CreditTransaction
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Tags { get; set; }
        public DateTime CreditDate { get; set; }
        public string Status { get; set; } // New property to track Pending or Cleared status
    }
}
