namespace InOut.Common.Exceptions
{
    public class CryptException : Exception
    {
        public CryptException(string message) 
            : base(message)
        { }

        public CryptException(string message, Exception exception)
            : base(message, exception)
        { }
    }
}
