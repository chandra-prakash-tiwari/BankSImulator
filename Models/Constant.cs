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

        public static readonly string EmployeeMenu = " 1. Add new account\n 2. Remove account\n 3 .Update account\n 4. Transaction\n 5. Currency exchange\n 6. SignOut\n 7. Exit";

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

        public static readonly string SourceAccountNumber = "Source account number : ";

        public static readonly string DestinationAccountNumber = "Destination account number : ";

        public static readonly string DestinationBankId = "Destination bank id";

        public static readonly string UpdateField = "1. Name\n2. Address\n3. Phone number\n4. Password";

        public static readonly string AvailbleBalance = "You available balance is ";

        public static readonly string UserNotFound = "User not found";

        public static readonly string DepositDecline = "Deposit operation will not perform right now try another time";

        public static readonly string CashWithDrawDecline = "CashWithDraw not perform right now try another time once check you available balance";

        public static readonly string FundTranserDecline = "Fund transer will be not perform right now once check all detail and availabe balance";

        public static readonly string EmployeeId = "Employee Id : ";

        public static readonly string EmployeeUserName = "Employee username : ";

        public static readonly string EmployeePassword = "Employee password : ";

        public static readonly string RemoveEmployee = "Employee has successfully";

        public static readonly string UpdateEmployee = "Employee detail has successfully updated";

        public static readonly string RemoveAccount= "Account has successfully";

        public static readonly string UpdateAccount = "Account detail has successfully updated";

        public static readonly string CurrencyRate = "Currency rate : ";

        public static readonly string PayableAmount = "Amount will be pay";

        public static readonly string AcoountNotFound = "Sorry this account not found";

        public static readonly string RevertDecline = "Revert transaction not perform right now";

        public static readonly string RevertDone = "Revert has been successfully done";

        public static readonly string AdminDetail = "Enter admin detail";

        public static readonly string EmployeeDetail = "Enter employee detail";

        public static readonly string AccountHolderDetail = "Enter account holder Detail";

        public static readonly string BankDetail = "Enter bank detail";

        public static readonly string TransactionId = "TRansaction Id :";

        public static readonly string NewBalance = "Your new balance";

        public static readonly string AdminCredentials = "Admin Credentials";
    }
}
