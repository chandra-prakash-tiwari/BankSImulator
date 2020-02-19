using System;

namespace Models
{
    public class Transaction
    {
        public string Id { get; set; }

        public double Amount { get; set; }

        public TransactionType Mode { get; set; }

        public DateTime Date { get; set; }

        public TransactionStatus Status { get; set; }

        public string AccountNumber { get; set; }

        public string SrcBankId { get; set; }

        public string DescAccountNumber { get; set; }

        public string DestBankId { get; set; }
    }
}
