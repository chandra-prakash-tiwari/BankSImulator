using System.Collections.Generic;

namespace BankSimulator.Models
{
    public class Account
    {
        public string Id { get; set; }

        public double FundBalance { get; set; }

        public User Holder { get; set; }

        public List<Transaction> Transactions { get; set; }

        public Account()
        {
            Transactions = new List<Transaction>();

            Holder = new User();
        }
    }
}
