using System.Collections.ObjectModel;
using VendingMachineApp.Domain;

namespace VendingMachineApp.Application;

public class Ledger : ILedger
{
    private readonly List<Transaction> _transactions = [];

    public ReadOnlyCollection<Transaction> Transactions
        => _transactions.AsReadOnly();

    public void RecordCompletedTransaction(Transaction transaction)
    {
        if (!transaction.IsComplete)
        {
            throw new InvalidOperationException("Transaction is not complete to record to the ledger");
        }
        _transactions.Add(transaction);
    }
}
