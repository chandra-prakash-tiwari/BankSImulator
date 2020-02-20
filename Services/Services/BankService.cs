using System;
using System.Linq;
using BankSimulator.Models;
using BankSimulator.Services.Interfaces;
using Services.Services;

namespace Services
{
    public class BankService : IBankService
    {
        public Bank CurrentBank { get; set; }

        public BankService(string bankId)
        {
            this.CurrentBank = MasterBankService.GetBank(bankId);
        }

        public bool HasEmployee(string id)
        {
            return this.CurrentBank != null ? this.CurrentBank.Employees.FirstOrDefault(a => a.Id == id) !=null ? true : false : false;
        }

        public bool HasAccount(string id)
        {
            return this.CurrentBank != null ? this.CurrentBank.Accounts.FirstOrDefault(a => a.Id == id) != null ? true : false : false;
        }

        public static bool CreateAdmin(User user,string bankId)
        {
            MasterBankService.GetBank(bankId).Admin.UserId = user.Name.Substring(0, 3) + bankId;
            MasterBankService.GetBank(bankId).Admin = user;
            return true;
        }

        public Employee GetEmployee(string id)
        {
            Employee employee = this.CurrentBank.Employees.FirstOrDefault(a => a.Id == id);
            if (employee != null)
            {
                return employee;
            }

            return null;
        }

        public Account GetAccount(string id)
        {
            Account account = this.CurrentBank.Accounts.FirstOrDefault(i => i.Id == id);
            if (account != null)
            {
                return account;
            }

            return null;
        }

        public string CreateEmployee(Employee employee)
        {
            employee.Id = employee.Name.Substring(0, 2) + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year;
            employee.UserId = employee.Name.Substring(0, 3) + this.CurrentBank.Id;
            this.CurrentBank.Employees.Add(employee);
            return employee.Id;
        }

        public string CreateAccount(Account account)
        {
            DateTime now = DateTime.Now;
            account.Id = account.Holder.Name.Substring(0, 4) + now.Day + now.Month + now.Year;
            account.Holder.UserId = account.Holder.Name.Substring(0, 3) + this.CurrentBank.Id;
            this.CurrentBank.Accounts.Add(account);
            return account.Id;
        }

        public bool RemoveEmployee(string id)
        {
            Employee employee = this.CurrentBank.Employees.FirstOrDefault(a => a.Id == id);
            if (employee != null)
            {
                this.CurrentBank.Employees.Remove(employee);
                return true;
            }

            return false;
        }

        public bool RemoveAccount(string id)
        {
            Account account = this.CurrentBank.Accounts.FirstOrDefault(i => i.Id == id);
            if (account != null)
            {
                this.CurrentBank.Accounts.Remove(account);
                return true;
            }

            return false;
        }

        public bool UpdateAccount(Account account, string id)
        {
            Account oldAccountDetail = this.CurrentBank.Accounts.FirstOrDefault(a => a.Id == id);
            if (oldAccountDetail != null)
            {
                account.Id = oldAccountDetail.Id;
                account.Holder.BankId = oldAccountDetail.Holder.BankId;
                oldAccountDetail = account;
                return true;
            }

            return false;
        }

        public bool UpdateEmployee(Employee employee, string id)
        {
            Employee oldEmployeeDetail = this.CurrentBank.Employees.FirstOrDefault(a => a.Id == id);
            if (oldEmployeeDetail != null)
            {
                employee.Id = oldEmployeeDetail.Id;
                employee.UserId = oldEmployeeDetail.UserId;
                oldEmployeeDetail = employee;
                return true;
            }


            return true;
        }
    }
}
