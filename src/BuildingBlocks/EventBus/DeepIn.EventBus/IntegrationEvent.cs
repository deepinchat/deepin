namespace DeepIn.EventBus;

public abstract record IntegrationEvent
{
    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreatedDateTime = DateTime.UtcNow;
    } 
    public Guid Id { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
}
