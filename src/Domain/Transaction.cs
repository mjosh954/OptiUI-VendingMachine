using System.Collections.ObjectModel;
using VendingMachineOOO.Exceptions;

namespace VendingMachineOOO.Domain;

public class Transaction
{
    private readonly List<CoffeeOrder> _coffeeOrders = [];

    public ReadOnlyCollection<CoffeeOrder> CoffeeOrders => _coffeeOrders.AsReadOnly();

    public bool IsComplete { get; private set; }

    public DateTime? CompletedAt { get; private set; }

    public Guid Id { get; }

    public Transaction()
    {
        Id = Guid.NewGuid();
    }

    public void Add(CoffeeOrder item)
    {
        if (IsComplete)
        {
            throw new TransactionCompleteException();
        }
        _coffeeOrders.Add(item);
    }

    public void Complete(DateTime completedAt)
    {
        CompletedAt = completedAt;
        IsComplete = true;
    }

    public bool HasCoffeeOrder => _coffeeOrders.Count > 0;

    public static Transaction EmptyTransaction => new NoopTransaction();
}

class NoopTransaction : Transaction
{
}