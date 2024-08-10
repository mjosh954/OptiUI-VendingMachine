using OptiUI_VendingMachine.Domain;

namespace VendingMachineOOO.Domain;

public class Bank
{
    protected List<Money> _monies = new List<Money>();
    public decimal Total => _monies.Sum(s => s.Value);

    public Bank() : this([])
    {
    }

    public Bank(List<Money> monies)
    {
        _monies = monies;
    }

    public void AddMoney(Money mon)
    {
        _monies.Add(mon);
    }

    public List<Money> GetMoney(decimal amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException("Can not get negative money from bank");

        if (amount == 0) return Enumerable.Empty<Money>().ToList();

        if (amount > Total)
        {
            throw new InsufficientFundsException();
        }

        List<Money> money = [];
        // 
        var amountToReturn = amount;
        while (amountToReturn > 0)
        {
            if (amountToReturn - 20.0m >= 0)
            {
                amountToReturn -= 20.0m;
                var m = _monies.First(s => s is Twenty);
                _monies.Remove(m);
                money.Add(m);
            }
            else if (amountToReturn - 10.0m >= 0)
            {

                amountToReturn -= 10.0m;
                var m = _monies.First(s => s is Ten);
                _monies.Remove(m);
                money.Add(m);
            }
            else if (amountToReturn - 5.0m >= 0)
            {

                amountToReturn -= 5.0m;
                var m = _monies.First(s => s is Five);
                _monies.Remove(m);
                money.Add(m);
            }
            else if (amountToReturn - 1.0m >= 0)
            {

                amountToReturn -= 1.0m;
                var m = _monies.First(s => s is Dollar);
                _monies.Remove(m);
                money.Add(m);
            }

            else if (amountToReturn - 0.25m >= 0)
            {

                amountToReturn -= 0.25m;
                var m = _monies.First(s => s is Quarter);
                _monies.Remove(m);
                money.Add(m);
            }
            else if (amountToReturn - 0.10m >= 0)
            {

                amountToReturn -= 0.10m;
                var m = _monies.First(s => s is Dime);
                _monies.Remove(m);
                money.Add(m);
            }
            else if (amountToReturn - 0.05m >= 0)
            {

                amountToReturn -= 0.05m;
                var m = _monies.First(s => s is Nickel);
                _monies.Remove(m);
                money.Add(m);
            }
        }

        return money;
    }

    public static List<Money> CreateQuarterRoll()
        => Enumerable.Range(0, 40).Select(s => new Quarter() as Money).ToList();

    public static List<Money> CreateDimeRoll()
        => Enumerable.Range(0, 40).Select(s => new Dime() as Money).ToList();
    public static List<Money> CreateNickelRoll()
        => Enumerable.Range(0, 40).Select(s => new Nickel() as Money).ToList();

    public static List<Money> CreateTwentyStack()
        => Enumerable.Range(0, 100).Select(s => new Twenty() as Money).ToList();

    public static List<Money> CreateTenStack()
        => Enumerable.Range(0, 100).Select(s => new Ten() as Money).ToList();

    public static List<Money> CreateFiveStack()
        => Enumerable.Range(0, 100).Select(s => new Five() as Money).ToList();

    public static List<Money> CreateDollarStack()
        => Enumerable.Range(0, 100).Select(s => new Dollar() as Money).ToList();

    public static List<Money> CreateDefaultTill()
        => CreateTwentyStack()
        .Concat(CreateTenStack())
        .Concat(CreateFiveStack())
        .Concat(CreateDollarStack())
        .Concat(CreateQuarterRoll())
        .Concat(CreateDimeRoll())
        .Concat(CreateNickelRoll())
        .ToList();

}

public class LoadedBank : Bank
{
    public LoadedBank() : base(CreateDefaultTill())
    {
    }
}

public class EmptyBank : Bank
{
    public EmptyBank() : base([])
    {

    }
}