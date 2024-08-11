using VendingMachineOOO.Application;

namespace VendingMachineOOO.Infrastructure;

public class Clock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
