namespace VendingMachineOOO.Domain;

public class VendingMachine : IVendingMachine
{
    Transaction _currentTransaction = Transaction.EmptyTransaction;
    private Bank _bank;
    private List<Money> _moneyInserted = [];
    private Ledger _ledger = new();
    private readonly PriceCalculator _priceCalculator;
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


    public VendingMachine(Bank bank, PriceCalculator priceCalculator)
    {

        _bank = bank;
        _priceCalculator = priceCalculator;
    }

    public VendingMachine() : this(new Bank(Bank.CreateDefaultTill()), new PriceCalculator(new Prices()))
    {
    }

    public List<Money> Cancel()
    {
        _currentTransaction = Transaction.EmptyTransaction;
        return []; // return any money inserted
    }

    public (Coffee[], Money[]) CompleteOrder()
    {

        throw new NotImplementedException(); // return coffee and any change
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
        coffee.CreamCount = creamCount;
        coffee.SugarCount = sugarCount;
        _currentTransaction.Add(coffee);
    }
}
