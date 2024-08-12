using VendingMachineApp.Domain;

namespace VendingMachineApp.Application;

public class CoffeeFactory : ICoffeeFactory
{
    public Coffee BuildCoffee(CoffeeOrder order)
    {
        var size = order.Size;

        Coffee coffee = size switch
        {
            CoffeeSize.Large => new LargeCoffee(),
            CoffeeSize.Medium => new MediumCoffee(),
            CoffeeSize.Small => new SmallCoffee(),
            _ => throw new ArgumentOutOfRangeException(nameof(order), "Size is not valid")
        };
        coffee.AddSugar(order.SugarCount);
        coffee.AddCream(order.CreamCount);
        return coffee!;
    }
}
