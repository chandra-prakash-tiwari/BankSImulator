
using BankSimulator.Models;

namespace BankSimulator.Services.Interfaces
{
    public interface IBankService
    {
        string CreateEmployee(Employee employee);

        string CreateAccount(Account account);

        bool RemoveEmployee(string employeeId);

        bool RemoveAccount(string accountId);

        bool UpdateAccount(Account updateAccount, string accountId);

        bool UpdateEmployee(Employee updateEmployee, string employeeId);
    }
}
