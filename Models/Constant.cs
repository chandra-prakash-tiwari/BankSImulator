using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulator.Models
{
    public class Constant
    {
        public static readonly string Invalid = "Please Enter Correct Value";

        public static readonly string AdminMenu = " 1. Add new employee\n 2. Remove Employee\n 3. Update employee detail\n 4. Add new account\n 5. Remove account\n 6. Update account\n 7. Account transaction\n 8. Currency exchange\n 9. SignOut\n 10.Exit";

        public static readonly string EmployeeMenu = " 1. Add new Account\n 2. Remove Account\n 3 .Update Account\n 4. Transaction\n 5. Currency Exchange\n 6. SignOut\n 7. Exit";

        public static readonly string UserMenu = " 1. Deposit\n 2. Cash withdraw\n 3. Fund transfer\n 4. View balance\n 5. SignOut\n 6. Exit";

        public static readonly string TransactionMenu = " 1. Deposit\n 2. Cash withdraw\n 3. Fund transfer\n 4. View account balance\n 5. Revert transaction\n 6. SignOut\n 7. Exit";

        public static readonly string MainMenu = " 1. Bank setup\n 2. Login\n 3. Exit";

        public static readonly string BankId = "Bank Id : ";

        public static readonly string AccountNumber = "Account number : ";

        public static readonly string Name = "Name : ";

        public static readonly string Address = "Address : ";

        public static readonly string PhoneNumber = "Phone number : ";

        public static readonly string UserId = "User name : ";

        public static readonly string Password = "Password : ";

        public static readonly string TransactionAmount = "Amount : ";

        public static readonly string UpdateField = "1. Name\n2. Address\n3. Phone number\n4. Password";

        public static readonly string AvailbleBalance = "You available balance is ";

        public static readonly string UserNotFound = "User not found";

        public static readonly string AdminCredentials = "Admin Credentials";
    }
}
