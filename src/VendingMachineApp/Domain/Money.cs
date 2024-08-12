namespace VendingMachineApp.Domain;

public abstract record Money(decimal Value);

public abstract record Bill(decimal Value) : Money(Value)
{
    private readonly Guid _serial = Guid.NewGuid();
    public Guid Serial => _serial;
};

public abstract record Coin(decimal Value) : Money(Value);

public record Twenty() : Bill(20.00m);
public record Ten() : Bill(10.00m);
public record Five() : Bill(5.00m);
public record Dollar() : Bill(1.00m);
public record Quarter() : Coin(0.25m);
public record Dime() : Coin(0.10m);
public record Nickel() : Coin(0.05m);
