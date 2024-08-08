namespace VendingMachineOOO.Domain;

public class CoffeeOrder
{
    public virtual CoffeeSize Size { get; set; }

    public virtual int CreamCount { get; set; }

    public virtual int SugarCount { get; set; }

    public ReadOnlyCoffeeOrder AsReadOnly() => new(this);
}

public class ReadOnlyCoffeeOrder : CoffeeOrder
{
    public override int CreamCount { get => base.CreamCount; set => throw new InvalidOperationException(); }

    public override int SugarCount { get => base.SugarCount; set => throw new InvalidOperationException(); }

    public override CoffeeSize Size { get => base.Size; set => throw new InvalidOperationException(); }

    public ReadOnlyCoffeeOrder(CoffeeOrder order)
    {
        base.Size = order.Size;
        base.CreamCount = order.CreamCount;
        base.SugarCount = order.SugarCount;
    }
}