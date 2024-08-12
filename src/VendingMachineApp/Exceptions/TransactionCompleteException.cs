namespace VendingMachineApp.Exceptions
{
    [Serializable]
    internal class TransactionCompleteException : Exception
    {
        public TransactionCompleteException()
        {
        }

        public TransactionCompleteException(string? message) : base(message)
        {
        }

        public TransactionCompleteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}