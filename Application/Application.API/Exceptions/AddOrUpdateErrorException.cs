namespace Application.API.Exceptions
{
    [Serializable]
    public class AddOrUpdateErrorException : Exception
    {
        public AddOrUpdateErrorException() { }
        public AddOrUpdateErrorException(string message) : base(message) { }
    }
}
