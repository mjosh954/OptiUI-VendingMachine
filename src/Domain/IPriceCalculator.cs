using System.Collections.ObjectModel;
using VendingMachineOOO.Domain;

namespace VendingMachineOOO
{
    public interface IPriceCalculator
    {
        public decimal CalculateTotal(ReadOnlyCoffeeOrder order);
        public decimal CalculateTotal(ReadOnlyCollection<ReadOnlyCoffeeOrder> orders);
    }
}
