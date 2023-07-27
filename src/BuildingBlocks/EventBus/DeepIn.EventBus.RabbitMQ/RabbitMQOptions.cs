namespace DeepIn.EventBus.RabbitMQ
{
    public class RabbitMQOptions
    {
        public static string ProviderKey = "RabbitMQ";
        public string HostName { get; set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
    }
}