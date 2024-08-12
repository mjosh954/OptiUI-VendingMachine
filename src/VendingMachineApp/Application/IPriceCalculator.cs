using System.Collections.ObjectModel;
using VendingMachineApp.Domain;

namespace VendingMachineApp.Application
{
    public interface IPriceCalculator
    {
        public decimal CalculateTotal(CoffeeOrder order);
        public decimal CalculateTotal(ReadOnlyCollection<CoffeeOrder> orders);
    }
}
