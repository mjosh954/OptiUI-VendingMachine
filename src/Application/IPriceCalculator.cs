using System.Collections.ObjectModel;
using VendingMachineOOO.Domain;

namespace VendingMachineOOO.Application
{
    public interface IPriceCalculator
    {
        public decimal CalculateTotal(CoffeeOrder order);
        public decimal CalculateTotal(ReadOnlyCollection<CoffeeOrder> orders);
    }
}
