using Models;
using System;
using System.Linq;

namespace Services
{
    public class BankService : IBankService
    {
        public Bank Bank { get; set; }

        public BankService(Bank bank)
        {
            this.Bank = bank;
        }

        public int SearchEmployee(string id)
        {
            var index = this.Bank.Employees.FindIndex(i => i.Id == id);
            if (index != -1)
            {
                return index;
            }

            return -1;
        }

        public int SearchAccount(string id)
        {
            var index = this.Bank.Accounts.FindIndex(i => i.Id == id);
            if (index != -1)
            {
                return index;
            }

            return -1;
        }

        public Employee CreateEmployee(Employee employee)
        {
            DateTime now = DateTime.Now;
            employee.Id = employee.Name.Substring(0, 2) + now.Day + now.Month + now.Year;
            employee.UserId = employee.Name.Substring(0, 3) + this.Bank.Id;
            this.Bank.Employees.Add(employee);
            return employee;
        }

        public Account CreateAccount(Account account)
        {
            DateTime now = DateTime.Now;
            account.Id = account.Holder.Name.Substring(0, 4) + now.Day + now.Month + now.Year;
            account.Holder.UserId = account.Holder.Name.Substring(0, 3) + this.Bank.Id;
            this.Bank.Accounts.Add(account);
            return account;
        }

        public bool RemoveEmployee(int index)
        {
            this.Bank.Employees.RemoveAt(index);
            return true;
        }

        public bool RemoveAccount(int index)
        {
            this.Bank.Accounts.RemoveAt(index);
            return true;
        }

        public bool UpdateAccount(Account account, string accountId)
        {
            var oldAccount = this.Bank.Accounts.FirstOrDefault(a => a.Id == accountId);
            if (oldAccount != null)
            {
                account.Id = oldAccount.Id;
                account.Holder.BankId = oldAccount.Holder.BankId;
                oldAccount = account;
                return true;
            }

            return false;
        }

        public bool UpdateEmployee(Employee employee, string employeeId)
        {
            var index = Bank.Employees.FindIndex(_ => _.Id == employeeId);
            if (index != -1)
            {
                Bank.Employees[index] = employee;
                return true;
            }

            return false;
        }
    }
}
