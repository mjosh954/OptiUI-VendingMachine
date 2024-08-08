using System.Collections.ObjectModel;

namespace VendingMachineOOO.Domain;

public class Ledger
{
    private List<Transaction> _transactions = [];

    public ReadOnlyCollection<Transaction> Transactions
        => _transactions.AsReadOnly();

    public Ledger()
    {

    }

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

}
