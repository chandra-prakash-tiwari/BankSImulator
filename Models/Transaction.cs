using System;

namespace Models
{
    public class Transaction
    {
        public string Id { get; set; }

        public double Amount { get; set; }

        public TransactionType Mode { get; set; }

        public DateTime Date { get; set; }

        public bool Status { get; set; }

        public string Account { get; set; }

        public string BankId { get; set; }

        public string SourceAccount { get; set; }

        public string DestinationAccount { get; set; }

        public string SourceBank { get; set; }

        public string DestinationBank { get; set; }
    }
}
