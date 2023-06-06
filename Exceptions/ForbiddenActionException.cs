namespace PR_103_2019.Exceptions
{
    public class ForbiddenActionException : Exception
    {
        public ForbiddenActionException()
        {
        }

        public ForbiddenActionException(string message) : base(message)
        {
        }

        public ForbiddenActionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
