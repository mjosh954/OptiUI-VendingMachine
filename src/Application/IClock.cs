namespace VendingMachineOOO.Application;

public interface IClock
{
    public DateTime UtcNow { get; }
}
