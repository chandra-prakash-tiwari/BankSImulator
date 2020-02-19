using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Constrants
    {
        public static readonly string Invalid = "Please Enter Correct Valid";

        public static readonly string AdminMenu = " 1. Add New Employee\n 2. Remove Employee\n 3. Update Employee Detail\n 4. Add new Account\n 5. Remove Account\n 6. Update Account\n 7. Account Transaction\n 8. Currency Exchange\n 9. SignOut\n 10.Exit";

        public static readonly string EmployeeMenu = " 1. Add New Account\n 2. Remove Account\n 3 .Update Account\n 4. Transaction\n 5. Currency Exchange\n 6. SignOut\n 7. Exit";

        public static readonly string UserMenu = " 1. Deposit\n 2. Cash Withdraw\n 3. Fund Transfer\n 4. View Balence\n 5. SignOut\n 6. Exit";

        public static readonly string TransactionMenu = " 1. Deposit\n 2. Cash Withdraw\n 3. Fund Transfer\n 4. View Account Balence\n 5. Revert Transaction\n 6. SignOut\n 7. Exit";

        public static readonly string MainMenu = " 1. Create a New Bank\n 2. Login\n 3. Exit";

        public static readonly string BankId = "Bank Id : ";

        public static readonly string AccountNumber = "Account Number : ";

        public static readonly string Name = "Name : ";

        public static readonly string Address = "Address : ";

        public static readonly string PhoneNumber = "Phone Number : ";

        public static readonly string UserId = "User Name : ";

        public static readonly string Password = "Password : ";

        public static readonly string TransactionAmount = "Amount : ";

        public static readonly string UpdateField = "1. Name\n2. Address\n3. PhoneNumber\n4. Password";

        public static readonly string AvailbleBalance = "You Available Balance is ";

        public static readonly string UserNotFound = "Please Check Id once";
    }
}
