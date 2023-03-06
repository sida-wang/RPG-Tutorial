namespace Engine.Exceptions
{
    [Serializable]
    internal class LocationNotFoundException : Exception
    {
        internal LocationNotFoundException() { }
        internal LocationNotFoundException(string message) : base(message) { }
        internal LocationNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
