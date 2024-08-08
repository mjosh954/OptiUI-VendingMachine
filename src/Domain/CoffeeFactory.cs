namespace VendingMachineOOO.Domain;

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
            _ => throw new ArgumentOutOfRangeException()
        };
        coffee.AddSugar(order.SugarCount);
        coffee.AddCream(order.CreamCount);
        return coffee!;
    }
}
