namespace VendingMachineOOO.Domain;

public interface ICoffeeFactory
{
    Coffee BuildCoffee(CoffeeOrder order);
}