using VendingMachineApp.Application;

namespace VendingMachineApp.Infrastructure;

public class Clock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
