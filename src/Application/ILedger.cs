using VendingMachineOOO.Domain;

namespace VendingMachineOOO.Application;

public interface ILedger
{
    void RecordCompletedTransaction(Transaction transaction);
}
