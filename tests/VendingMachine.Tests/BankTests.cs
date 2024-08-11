using FluentAssertions;
using VendingMachineOOO.Domain;
using VendingMachineOOO.Exceptions;

namespace VendingMachineTests;

public class BankTests
{
    [Fact]
    public void ReturnsAllTwentiesChange()
    {
        Bank bank = new LoadedBank();

        var money = bank.GetMoney(100);

        money.Should().HaveCount(5).And.AllBeOfType<Twenty>();
        bank.Total.Should().Be(3516m);
    }

    [Fact]
    public void GetMoney_Returns_0_When_Pass0()
    {
        Bank bank = new LoadedBank();

        var money = bank.GetMoney(0);

        money.Should().BeEmpty();
    }

    [Fact]
    public void ReturnsAllMixedChange()
    {
        Bank bank = new LoadedBank();

        var money = bank.GetMoney(72.15m);

        money.Where(s => s is Twenty).Should().HaveCount(3);
        money.Where(s => s is Ten).Should().HaveCount(1);
        money.Where(s => s is Dollar).Should().HaveCount(2);
        money.Where(s => s is Dime).Should().HaveCount(1);
        money.Where(s => s is Nickel).Should().HaveCount(1);
    }

    [Fact]
    public void Total_SetToSum_When_AddMoney()
    {
        var bank = new EmptyBank();

        bank.AddMoney(new Twenty());
        bank.AddMoney(new Ten());
        bank.AddMoney(new Five());
        bank.AddMoney(new Dollar());
        bank.AddMoney(new Quarter());
        bank.AddMoney(new Dime());
        bank.AddMoney(new Nickel());

        bank.Total.Should().Be(36.40m);
    }

    [Fact]
    public void ThrowInsufficientFunds_When_MoreMoneyAskedThanHeld()
    {
        var bank = new EmptyBank();
        bank.AddMoney(new Twenty());
        bank.AddMoney(new Twenty());
        bank.AddMoney(new Twenty());

        var exception = Record.Exception(() => bank.GetMoney(100m));
        exception.Should().NotBeNull().And.BeOfType<InsufficientFundsException>();
    }
}
