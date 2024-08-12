namespace VendingMachineApp.Exceptions;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException() : base("There is not enough money")
    {
    }
}
