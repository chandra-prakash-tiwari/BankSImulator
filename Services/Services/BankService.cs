using Models;
using System.Linq;

namespace Services
{
    public class BankService : IBankService
    {
        public Bank Bank { get; set; }

        public BankService(Bank bank)
        {
            Bank = bank;
        }

        public bool CreateEmployee(Employee employee)
        {
            this.Bank.Employees.Add(employee);
            return true;
        }

        public bool CreateAccount(Account account)
        {
            this.Bank.Accounts.Add(account);
            return true;
        }

        public bool RemoveEmployee(string Id)
        {
            var record = Bank.Employees.Where(a => a.Id == Id)?.Select(a => a).ToList();
            foreach (var employee in record)
            {
                this.Bank.Employees.Remove(employee);
                return true;
            }

            return false;
        }

        public bool RemoveAccount(string accountId)
        {
            var record = Bank.Accounts.Where(a => a.Id == accountId).Select(a => a).ToList();
            foreach (var account in record)
            {
                this.Bank.Accounts.Remove(account);
                return true;
            }

            return false;
        }

        public bool UpdateAccount(Account account, string accountId)
        {
            var index = Bank.Accounts.FindIndex(_ => _.Id == accountId);
            if(index != -1)
            {
                Bank.Accounts[index] = account;
                return true;
            }

            return false;
        }

        public bool UpdateEmployee(Employee updateEmployee, string employeeId)
        {
            var record = Bank.Employees.Where(a => a.Id == employeeId).Select(a => a).ToList();
            foreach (var employee in record)
            {
                this.Bank.Employees.Remove(employee);
                this.Bank.Employees.Add(updateEmployee);
                return true;
            }

            return false;
        }
    }
}
