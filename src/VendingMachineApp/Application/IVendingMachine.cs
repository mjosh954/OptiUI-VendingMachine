using System.Collections.ObjectModel;
using VendingMachineApp.Domain;

namespace VendingMachineApp.Application
{
    public interface IVendingMachine
    {
        decimal RemainingBalance { get; }

        decimal TotalInsertedMoneyValue { get; }

        ReadOnlyCollection<CoffeeOrder> CurrentOrders { get; }
        decimal TotalCost { get; }

        void SelectCoffee(CoffeeSize size, int creamCount = 0, int sugarCount = 0);

        // Marks transaction as being paid, can't edit order
        void InsertMoney(Money money);

        List<Money> Cancel();

        (List<Coffee>, List<Money>) CompleteOrder();
    }
}