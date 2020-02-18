using System.Collections.Generic;

namespace Models
{
    public class Bank
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public float RtgsSame { get; set; }

        public float RtgsOther { get; set; }

        public float ItgsSame { get; set; }

        public float ItgsOther { get; set; }

        public long PhoneNumber { get; set; }

        public string Currency { get; set; }

        public Admin Admin { get; set; }

        public List<Employee> Employees { get; set; }

        public List<Account> Accounts { get; set; }

        public Bank()
        {
            Admin = new Admin();
            Employees = new List<Employee>();
            Accounts = new List<Account>();
        }
    }
}
