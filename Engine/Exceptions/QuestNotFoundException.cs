namespace Engine.Exceptions
{
    internal class QuestNotFoundException : Exception
    {
        internal QuestNotFoundException() { }
        internal QuestNotFoundException(string message) : base(message) { }
        internal QuestNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
