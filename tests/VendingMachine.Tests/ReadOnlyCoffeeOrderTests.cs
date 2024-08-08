using FluentAssertions;
using VendingMachineOOO.Domain;

namespace VendingMachineTests;

public class ReadOnlyCoffeeOrderTests
{
    [Fact]
    public void ThrowInvalidOperationException_When_CallSetSugarCount()
    {
        ReadOnlyCoffeeOrder order = new ReadOnlyCoffeeOrder(new CoffeeOrder());
        var exception = Record.Exception(() => order.SugarCount = 1);
        exception.Should().BeOfType<InvalidOperationException>();
    }

    [Fact]
    public void ThrowInvalidOperationException_When_CallSetCreamCount()
    {
        ReadOnlyCoffeeOrder order = new ReadOnlyCoffeeOrder(new CoffeeOrder());

        var exception = Record.Exception(() => order.CreamCount = 1);
        exception.Should().BeOfType<InvalidOperationException>();
    }

    [Fact]
    public void ThrowInvalidOperationException_When_CallSetSize()
    {
        ReadOnlyCoffeeOrder order = new ReadOnlyCoffeeOrder(new CoffeeOrder());
        var exception = Record.Exception(() => order.Size = CoffeeSize.Medium);
        exception.Should().BeOfType<InvalidOperationException>();
    }

    [Fact]
    public void HaveValuesFromCoffeeOrder()
    {
        ReadOnlyCoffeeOrder order = new ReadOnlyCoffeeOrder(new CoffeeOrder
        {
            Size = CoffeeSize.Medium,
            SugarCount = 1,
            CreamCount = 3
        });

        order.Size.Should().Be(CoffeeSize.Medium);
        order.SugarCount.Should().Be(1);
        order.CreamCount.Should().Be(3);
    }
}
