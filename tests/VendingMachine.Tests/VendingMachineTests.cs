using FluentAssertions;
using VendingMachineOOO.Domain;

namespace VendingMachineTests;

public class VendingMachineTests
{
    [Fact]
    public void GetCurrentTotal_Returns_0_When_No_Order()
    {
        IVendingMachine machine = new VendingMachine();

        decimal total = machine.TotalCost;

        total.Should().Be(0.0m);
    }

    [Fact]
    public void Can_Create_Order_With_Two_Coffees_ShowTotal()
    {
        IVendingMachine machine = new VendingMachine();
        // 2.00 + 0.50 + 0.50 = 3.00
        machine.SelectCoffee(CoffeeSize.Medium,
            creamCount: 2,
            sugarCount: 0);

        // 1.75 + 0.25 = 2
        machine.SelectCoffee(CoffeeSize.Small, creamCount: 0, sugarCount: 1);

        decimal total = machine.TotalCost;

        total.Should().Be(5.0m);
    }


    [Fact]
    public void Should_Return0RemainingBalance_WhenMoneyGreaterThanCostOfOrder()
    {
        var medCoffeeResult = new MediumCoffee();
        IVendingMachine machine = new VendingMachine();
        machine.InsertMoney(new Five());
        machine.SelectCoffee(CoffeeSize.Medium);

        var remainingBalance = machine.RemainingBalance;

        remainingBalance.Should().Be(0.0m);
    }


    [Fact]
    public void Should_ReturnRemainingBalance1_75_WhenMoneyInsertedLessThanCost()
    {
        var medCoffeeResult = new MediumCoffee();
        IVendingMachine machine = new VendingMachine();
        machine.SelectCoffee(CoffeeSize.Medium);

        var remainingBalance = machine.RemainingBalance;

        remainingBalance.Should().Be(2.00m);
    }

    [Fact]
    public void InsertMoneyAddsMoneyToSpend()
    {
        IVendingMachine vendingMachine = new VendingMachine();
        vendingMachine.InsertMoney(new Twenty());
        vendingMachine.InsertMoney(new Dollar());

        var totalInserted = vendingMachine.TotalInsertedMoneyValue;

        totalInserted.Should().Be(21.0m);
    }

    [Fact]
    public void CurrentSpendableMoneyValue_0_When_NoMoneyInserted()
    {
        IVendingMachine vendingMachine = new VendingMachine();

        var totalInserted = vendingMachine.TotalInsertedMoneyValue;

        totalInserted.Should().Be(0.0m);
    }


    [Fact]
    public void CurrentOrder_Returns_ReadonlyCoffeeOrderWithSingleMediumCoffeeWithCreamAndSugarAddons()
    {
        IVendingMachine vendingMachine = new VendingMachine();
        vendingMachine.SelectCoffee(CoffeeSize.Medium, 1, 2);

        var currentOrders = vendingMachine.CurrentOrders;

        currentOrders.Should().HaveCount(1);
        currentOrders.Should().BeOfType<List<ReadOnlyCoffeeOrder>>();
    }

    [Fact]
    public void CompleteOrder_Returns_CoffeeAndChange_When_MoneyGreaterThanTotal()
    {

        IVendingMachine vendingMachine = new VendingMachine();
        vendingMachine.SelectCoffee(CoffeeSize.Medium, 1, 2);
        vendingMachine.InsertMoney(new Twenty());

        var (coffee, change) = vendingMachine.CompleteOrder();

        coffee.Should().NotBeEmpty().And.ContainSingle();
        change.Should().NotBeEmpty();

    }
}