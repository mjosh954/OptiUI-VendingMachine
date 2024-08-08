namespace OptiUI_VendingMachine.Domain;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException() : base("There is not enough money")
    {
    }
}
