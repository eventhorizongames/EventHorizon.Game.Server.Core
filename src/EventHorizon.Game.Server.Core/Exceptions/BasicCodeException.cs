namespace EventHorizon.Game.Server.Core.Exceptions
{
    [System.Serializable]
    public class BasicCodeException : System.Exception
    {
        public string Code { get; }
        private BasicCodeException() { this.Code = "invalid"; }
        public BasicCodeException(string code) { this.Code = code; }
        public BasicCodeException(string code, string message) : base(message) { this.Code = code; }
        public BasicCodeException(string code, string message, System.Exception inner) : base(message, inner) { this.Code = code; }
        protected BasicCodeException(string code,
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { this.Code = code; }
    }
}