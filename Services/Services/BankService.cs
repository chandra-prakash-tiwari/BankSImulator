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
            Bank = bank;
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

        public bool RemoveEmployee(string EmployeeId)
        {
            var employee = Bank.Employees.SingleOrDefault(a => a.Id == EmployeeId);
            if (employee != null)
            {
                this.Bank.Employees.Remove(employee);
                return true;
            }
            return false;
        }

        public bool RemoveAccount(string accountId)
        {
            var account = Bank.Accounts.SingleOrDefault(a => a.Id == accountId);
            if (account != null)
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
