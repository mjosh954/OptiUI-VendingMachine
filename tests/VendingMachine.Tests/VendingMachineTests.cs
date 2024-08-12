using FluentAssertions;
using System.Collections.ObjectModel;
using VendingMachineApp.Application;
using VendingMachineApp.Domain;
using VendingMachineApp.Infrastructure;

namespace VendingMachineTests;

public class VendingMachineTests
{
    private Clock _clock;
    private readonly PriceCalculator _priceCalculator;
    private readonly CoffeeFactory _coffeeFactory;

    public VendingMachineTests()
    {
        _clock = new Clock();
        _priceCalculator = new PriceCalculator(new Prices());
        _coffeeFactory = new CoffeeFactory();
    }

    private Func<Bank> getBank => () => new Bank(Bank.CreateDefaultTill());

    [Fact]
    public void GetCurrentTotal_Returns_0_When_No_Order()
    {
        VendingMachine machine = new VendingMachine(getBank(), _priceCalculator, _coffeeFactory, _clock);

        decimal total = machine.TotalCost;

        total.Should().Be(0.0m);
    }

    [Fact]
    public void Can_Create_Order_With_Two_Coffees_ShowTotal()
    {
        VendingMachine machine = new VendingMachine(getBank(), _priceCalculator, _coffeeFactory, _clock);
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
        VendingMachine machine = new VendingMachine(getBank(), _priceCalculator, _coffeeFactory, _clock);

        machine.InsertMoney(new Five());
        machine.SelectCoffee(CoffeeSize.Medium);

        var remainingBalance = machine.RemainingBalance;

        remainingBalance.Should().Be(0.0m);
    }


    [Fact]
    public void Should_ReturnRemainingBalance1_75_WhenMoneyInsertedLessThanCost()
    {
        VendingMachine machine = new VendingMachine(getBank(), _priceCalculator, _coffeeFactory, _clock);

        machine.SelectCoffee(CoffeeSize.Medium);

        var remainingBalance = machine.RemainingBalance;

        remainingBalance.Should().Be(2.00m);
    }

    [Fact]
    public void InsertMoneyAddsMoneyToSpend()
    {
        VendingMachine machine = new VendingMachine(getBank(), _priceCalculator, _coffeeFactory, _clock);

        machine.InsertMoney(new Twenty());
        machine.InsertMoney(new Dollar());

        var totalInserted = machine.TotalInsertedMoneyValue;

        totalInserted.Should().Be(21.0m);
    }

    [Fact]
    public void CurrentSpendableMoneyValue_0_When_NoMoneyInserted()
    {
        VendingMachine machine = new VendingMachine(getBank(), _priceCalculator, _coffeeFactory, _clock);


        var totalInserted = machine.TotalInsertedMoneyValue;

        totalInserted.Should().Be(0.0m);
    }


    [Fact]
    public void CurrentOrder_Returns_ReadonlyCoffeeOrderWithSingleMediumCoffeeWithCreamAndSugarAddons()
    {
        VendingMachine machine = new VendingMachine(getBank(), _priceCalculator, _coffeeFactory, _clock);
        machine.SelectCoffee(CoffeeSize.Medium, 1, 2);

        var currentOrders = machine.CurrentOrders;

        currentOrders.Should().HaveCount(1);
        currentOrders.Should().BeOfType<ReadOnlyCollection<CoffeeOrder>>();
    }

    [Fact]
    public void CompleteOrder_Returns_CoffeeAndChange_When_MoneyGreaterThanTotal()
    {
        VendingMachine machine = new VendingMachine(getBank(), _priceCalculator, _coffeeFactory, _clock);

        machine.SelectCoffee(CoffeeSize.Medium, 1, 2);
        machine.InsertMoney(new Twenty());

        var (coffee, change) = machine.CompleteOrder();

        coffee.Should().NotBeEmpty().And.ContainSingle();
        change.Should().NotBeEmpty();

    }

    [Fact]
    public void Cancel_Returns_MoneyInserted_When_HasMoneyAndSetsTransactionToEmpty()
    {
        VendingMachine machine = new VendingMachine(getBank(), _priceCalculator, _coffeeFactory, _clock);

        machine.SelectCoffee(CoffeeSize.Medium, 1, 2);
        var twenty = new Twenty();
        machine.InsertMoney(twenty);
        List<Money> money = machine.Cancel();

        machine.TotalCost.Should().Be(0.0m);
        machine.TotalInsertedMoneyValue.Should().Be(0.0m);
        money.Should().HaveCount(1);
        money[0].Should().NotBeNull()
            .And.BeOfType<Twenty>()
            .Which.Serial.Should().Be(twenty.Serial);
    }
}