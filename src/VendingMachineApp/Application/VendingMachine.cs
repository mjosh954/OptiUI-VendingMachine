using System.Collections.ObjectModel;
using VendingMachineApp.Domain;

namespace VendingMachineApp.Application;

public class VendingMachine : IVendingMachine
{
    Transaction _currentTransaction = Transaction.EmptyTransaction;

    private readonly Bank _bank;
    private readonly List<Money> _moneyInserted = [];
    private readonly Ledger _ledger = new();
    private readonly IPriceCalculator _priceCalculator;
    private readonly ICoffeeFactory _coffeeFactory;
    private readonly IClock _clock;

    public decimal TotalInsertedMoneyValue => _moneyInserted.Sum(m => m.Value);

    public ReadOnlyCollection<CoffeeOrder> CurrentOrders => _currentTransaction.CoffeeOrders
        .ToList()
        .AsReadOnly();

    public decimal RemainingBalance
    {
        get
        {
            var total = _priceCalculator.CalculateTotal(CurrentOrders);
            return Math.Max(0, total - _moneyInserted.Sum(s => s.Value));
        }
    }
    public decimal TotalCost => _priceCalculator.CalculateTotal(CurrentOrders);


    public VendingMachine(Bank bank, IPriceCalculator priceCalculator, ICoffeeFactory coffeeFactory, IClock clock)
    {

        _bank = bank;
        _priceCalculator = priceCalculator;
        _coffeeFactory = coffeeFactory;
        _clock = clock;
    }

    public List<Money> Cancel()
    {
        _currentTransaction = Transaction.EmptyTransaction;
        var toRefund = new List<Money>(_moneyInserted);
        _moneyInserted.Clear();
        return toRefund; // return any money inserted
    }

    public (List<Coffee>, List<Money>) CompleteOrder()
    {
        _currentTransaction.Complete(_clock.UtcNow);
        var coffeeOrders = _currentTransaction.CoffeeOrders;
        List<Coffee> coffees = coffeeOrders.Select(_coffeeFactory.BuildCoffee).ToList();

        var totalCost = TotalCost;

        var totalInsertedCash = TotalInsertedMoneyValue;
        var change = totalInsertedCash - totalCost;

        _ledger.RecordCompletedTransaction(_currentTransaction);
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

        CoffeeOrder coffee = new()
        {
            Size = size,
            CreamCount = creamCount,
            SugarCount = sugarCount
        };
        _currentTransaction.Add(coffee);
    }
}
