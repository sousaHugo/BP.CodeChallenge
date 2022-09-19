namespace BluePrism.TechnicalTest.Common.Exceptions
{
    public  class ArgumentInvalidException : AggregateException
    {
        public ArgumentInvalidException() : base() { }

        public ArgumentInvalidException(string message) : base(message) { }
        public ArgumentInvalidException(string Message, IEnumerable<ArgumentInvalidException> Exceptions) : base(Message, Exceptions) { }

    }

    public static class ArgumentInvalidExceptionExtensions
    {
        public static IEnumerable<string> GetErrors(this ArgumentInvalidException Exception)
        {
            return Exception.InnerExceptions.Select(a => a.Message).AsEnumerable();
        }
    }
}
