using VendingMachineApp.Domain;

namespace VendingMachineApp.Application;

public interface ICoffeeFactory
{
    Coffee BuildCoffee(CoffeeOrder order);
}