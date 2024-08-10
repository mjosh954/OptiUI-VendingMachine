namespace VendingMachineOOO.Domain;

public class VendingMachine : IVendingMachine
{
    Transaction _currentTransaction = Transaction.EmptyTransaction;
    private Bank _bank;
    private List<Money> _moneyInserted = [];
    private Ledger _ledger = new();
    private readonly IPriceCalculator _priceCalculator;
    private readonly ICoffeeFactory _coffeeFactory;
    public decimal TotalInsertedMoneyValue => _moneyInserted.Sum(m => m.Value);

    public List<ReadOnlyCoffeeOrder> CurrentOrders => _currentTransaction.CoffeeOrders
        .Select(s => s.AsReadOnly())
        .ToList();

    public decimal RemainingBalance
    {
        get
        {
            var total = _priceCalculator.CalculateTotal(CurrentOrders.AsReadOnly());
            return Math.Max(0, total - _moneyInserted.Sum(s => s.Value));
        }
    }
    public decimal TotalCost => _priceCalculator.CalculateTotal(CurrentOrders.AsReadOnly());


    public VendingMachine(Bank bank, IPriceCalculator priceCalculator, ICoffeeFactory coffeeFactory)
    {

        _bank = bank;
        _priceCalculator = priceCalculator;
        _coffeeFactory = coffeeFactory;
    }

    public VendingMachine() : this(new Bank(Bank.CreateDefaultTill()), new PriceCalculator(new Prices()), new CoffeeFactory())
    {
    }

    public List<Money> Cancel()
    {
        _currentTransaction = Transaction.EmptyTransaction;
        return []; // return any money inserted
    }

    public (List<Coffee>, List<Money>) CompleteOrder()
    {
        _currentTransaction.Complete();
        var coffeeOrders = _currentTransaction.CoffeeOrders.Select(s => s.AsReadOnly()).ToList();
        List<Coffee> coffees = coffeeOrders.Select(_coffeeFactory.BuildCoffee).ToList();

        var totalCost = TotalCost;

        var totalInsertedCash = TotalInsertedMoneyValue;
        var change = totalInsertedCash - totalCost;

        _ledger.AddTransaction(_currentTransaction);
        _currentTransaction = Transaction.EmptyTransaction;

        return (coffees, _bank.GetMoney(change));
    }

    public void InsertMoney(Money money)
    {
        // Lock order changing, till transaction is complete
        var total = _priceCalculator.CalculateTotal(CurrentOrders.AsReadOnly());
        _moneyInserted.Add(money);
        if (_moneyInserted.Sum(s => s.Value) >= total)
        {
            // complete transaction. 
            // Add money to bank
            // Dispense coffee
        }
    }

    /// <summary>
    /// Input order for coffee
    /// </summary>
    /// <param name="size">Sm - 1.75, Md - 2.00, Lg - 2.25</param>
    /// <param name="creamCount">0.50/cream</param>
    /// <param name="sugarCount">0.25/sugar</param>
    public void SelectCoffee(CoffeeSize size, int creamCount = 0, int sugarCount = 0)
    {
        if (_currentTransaction is NoopTransaction)
        {
            _currentTransaction = new Transaction();
        }

        var coffee = new CoffeeOrder();
        coffee.Size = size;
        coffee.CreamCount = creamCount;
        coffee.SugarCount = sugarCount;
        _currentTransaction.Add(coffee);
    }
}
