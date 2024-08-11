using VendingMachineOOO.Domain;

namespace VendingMachineOOO.Application;

public interface ICoffeeFactory
{
    Coffee BuildCoffee(CoffeeOrder order);
}