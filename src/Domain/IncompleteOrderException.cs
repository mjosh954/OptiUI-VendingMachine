
namespace VendingMachineOOO.Domain
{
    [Serializable]
    internal class IncompleteOrderException : Exception
    {
        public IncompleteOrderException()
        {
        }

        public IncompleteOrderException(string? message) : base(message)
        {
        }

        public IncompleteOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}