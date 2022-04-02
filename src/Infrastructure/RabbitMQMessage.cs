namespace Infrastructure
{
    public class RabbitMQMessage
    {
        public string MessageId { get; set; } = default!;

        public string MessageType { get; set; } = default!;

        public string Message { get; set; } = default!;
    }
}
