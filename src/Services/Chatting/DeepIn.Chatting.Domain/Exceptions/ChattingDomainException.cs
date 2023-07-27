namespace DeepIn.Chatting.Domain.Exceptions;

public class ChattingDomainException : Exception
{
    public ChattingDomainException()
    { }

    public ChattingDomainException(string message)
        : base(message)
    { }

    public ChattingDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
