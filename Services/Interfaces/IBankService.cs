
using BankSimulator.Models;

namespace BankSimulator.Services.Interfaces
{
    public interface IBankService
    {
        string CreateEmployee(Employee employee);

        string CreateAccount(Account account);

        bool RemoveEmployee(Employee employee);

        bool RemoveAccount(Account account);

        bool UpdateAccount(Account updateAccount, Account account);

        bool UpdateEmployee(Employee updateEmployee, Employee employee);
    }
}
