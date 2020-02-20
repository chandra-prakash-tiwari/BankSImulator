using System;
using System.Linq;
using BankSimulator.Models;
using BankSimulator.Services.Interfaces;
using Services.Services;

namespace Services
{
    public class BankService : IBankService
    {
        public string BankId { get; set; }

        public BankService(string bankId)
        {
            this.BankId = bankId;
        }

        public bool SearchEmployee(string id)
        {
            Employee employee = MasterBankService.GetBank(this.BankId).Employees.FirstOrDefault(a => a.Id == id);
            if (employee != null)
            {
                return true;
            }

            return false;
        }

        public bool SearchAccount(string id)
        {
            Account account = MasterBankService.GetBank(this.BankId).Accounts.FirstOrDefault(i => i.Id == id);
            if (account != null)
            {
                return true;
            }

            return false;
        }

        public Employee GetEmployee(string id)
        {
            Employee employee = MasterBankService.GetBank(this.BankId).Employees.FirstOrDefault(a => a.Id == id);
            if (employee != null)
            {
                return employee;
            }

            return null;
        }

        public Account GetAccount(string id)
        {
            Account account = MasterBankService.GetBank(this.BankId).Accounts.FirstOrDefault(i => i.Id == id);
            if (account != null)
            {
                return account;
            }

            return null;
        }

        public string CreateEmployee(Employee employee)
        {
            DateTime now = DateTime.Now;
            employee.Id = employee.Name.Substring(0, 2) + now.Day + now.Month + now.Year;
            employee.UserId = employee.Name.Substring(0, 3) + this.BankId;
            MasterBankService.GetBank(this.BankId).Employees.Add(employee);
            return employee.Id;
        }

        public string CreateAccount(Account account)
        {
            DateTime now = DateTime.Now;
            account.Id = account.Holder.Name.Substring(0, 4) + now.Day + now.Month + now.Year;
            account.Holder.UserId = account.Holder.Name.Substring(0, 3) + this.BankId;
            MasterBankService.GetBank(this.BankId).Accounts.Add(account);
            return account.Id;
        }

        public bool RemoveEmployee(Employee employee)
        {
            MasterBankService.GetBank(this.BankId).Employees.Remove(employee);
            return true;
        }

        public bool RemoveAccount(Account account)
        {
            MasterBankService.GetBank(this.BankId).Accounts.Remove(account);
            return true;
        }

        public bool UpdateAccount(Account account, Account oldAccount)
        {
            account.Id = oldAccount.Id;
            account.Holder.BankId = oldAccount.Holder.BankId;
            oldAccount = account;

            return true;
        }

        public bool UpdateEmployee(Employee employee, Employee oldemployee)
        {
            employee.Id = oldemployee.Id;
            employee.UserId = oldemployee.UserId;
            oldemployee = employee;

            return true;
        }
    }
}
