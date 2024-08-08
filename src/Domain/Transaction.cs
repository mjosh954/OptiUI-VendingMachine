using System.Collections.ObjectModel;

namespace VendingMachineOOO.Domain;

public class Transaction
{
    private List<CoffeeOrder> _coffeeOrders = [];

    public ReadOnlyCollection<CoffeeOrder> CoffeeOrders => _coffeeOrders.AsReadOnly();
    private bool _isComplete = false;
    public bool IsComplete => _isComplete;
    public Guid Id { get; }
    public Transaction()
    {
        Id = Guid.NewGuid();
    }

    public void Add(CoffeeOrder item)
    {
        if (_isComplete)
        {
            throw new TransactionCompleteException();
        }
        _coffeeOrders.Add(item);
    }

    public void Complete() => _isComplete = true;

    public bool HasCoffeeOrder => _coffeeOrders.Count > 0;

    public static Transaction EmptyTransaction => new NoopTransaction();
}

class NoopTransaction : Transaction
{
}