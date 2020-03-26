namespace EventHorizon.Identity.Exceptions
{
    [System.Serializable]
    public class IdentityServerRequestException : System.Exception
    {
        public IdentityServerRequestException(string message)
            : base(message) { }
    }
}