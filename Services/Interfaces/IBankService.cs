
using Models;

namespace Services
{
    public interface IBankService
    {
        string CreateEmployee(Employee employee);

        string CreateAccount(Account account);

        bool RemoveEmployee(string Id);

        bool RemoveAccount(string AccountId);

        bool UpdateAccount(Account updateAccount, string AccountId);

        bool UpdateEmployee(Employee updateEmployee, string employeeI);
    }
}
