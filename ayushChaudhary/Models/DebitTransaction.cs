namespace ayushChaudhary.Models
{
    public class DebitTransaction
    {
        public int Id { get; set; }
        public string Title { get; set; } // Title of the debit transaction
        public decimal DeductedAmount { get; set; } // Amount deducted
        public DateTime DebitDate { get; set; } // Date of the debit transaction
        public string Tags { get; set; } // Tags associated with the debit transaction
        public string Notes { get; set; } // Additional notes for the transaction
    }
}
