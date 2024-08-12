using VendingMachineApp.Domain;

namespace VendingMachineApp.Application;

public interface ILedger
{
    void RecordCompletedTransaction(Transaction transaction);
}
