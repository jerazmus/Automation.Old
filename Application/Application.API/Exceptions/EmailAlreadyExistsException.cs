namespace Application.API.Exceptions
{
    [Serializable]
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException() { }
        public EmailAlreadyExistsException(string message) : base(message) { }
    }
}
