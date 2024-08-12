using System.Collections.ObjectModel;
using VendingMachineApp.Domain;

namespace VendingMachineApp.Application;

public class PriceCalculator : IPriceCalculator
{
    private readonly Prices _prices;

    public PriceCalculator(Prices prices)
    {
        _prices = prices; // This should define prices
    }

    public decimal CalculateTotal(CoffeeOrder order)
    {
        decimal total = order.Size switch
        {
            CoffeeSize.Small => Prices.SmallCoffee,
            CoffeeSize.Medium => Prices.MediumCoffee,
            CoffeeSize.Large => Prices.LargeCoffee,
            _ => throw new ArgumentOutOfRangeException(nameof(order))
        };

        total += order.SugarCount * Prices.Sugar;
        total += order.CreamCount * Prices.Cream;
        return total;
    }

    public decimal CalculateTotal(ReadOnlyCollection<CoffeeOrder> orders) => orders.Sum(CalculateTotal);

}
