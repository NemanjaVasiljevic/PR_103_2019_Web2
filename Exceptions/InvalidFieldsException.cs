namespace PR_103_2019.Exceptions
{
    public class InvalidFieldsException : Exception
    {
        public InvalidFieldsException()
        {
        }

        public InvalidFieldsException(string message) : base(message)
        {
        }

        public InvalidFieldsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
