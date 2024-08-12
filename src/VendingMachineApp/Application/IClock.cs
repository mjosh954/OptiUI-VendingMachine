namespace VendingMachineApp.Application;

public interface IClock
{
    public DateTime UtcNow { get; }
}
