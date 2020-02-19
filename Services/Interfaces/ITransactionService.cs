
using Models;

namespace Services.Interfaces
{
    interface ITransactionService
    {
        double? ViewBalence(string AccountId);

        bool RevertTransaction(string TransactionId);
    }
}
