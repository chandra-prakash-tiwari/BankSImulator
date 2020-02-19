
using Models;

namespace Services
{
    public interface IBankService
    {
        Employee CreateEmployee(Employee employee);

        Account CreateAccount(Account account);

        bool RemoveEmployee(int index);

        bool RemoveAccount(int index);

        bool UpdateAccount(Account updateAccount, string AccountId);

        bool UpdateEmployee(Employee updateEmployee, string employeeI);
    }
}
