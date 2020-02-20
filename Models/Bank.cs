using System.Collections.Generic;

namespace BankSimulator.Models
{
    public class Bank
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public float RTGSSame { get; set; }

        public float RTGSOther { get; set; }

        public float ITGSSame { get; set; }

        public float ITGSOther { get; set; }

        public long PhoneNumber { get; set; }

        public string Currency { get; set; }

        public User Admin { get; set; }

        public List<Employee> Employees { get; set; }

        public List<Account> Accounts { get; set; }

        public Bank()
        {
            Admin = new User();
            Employees = new List<Employee>();
            Accounts = new List<Account>();
        }
    }
}
