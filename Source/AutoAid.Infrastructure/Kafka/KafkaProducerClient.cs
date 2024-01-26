using Confluent.Kafka;

namespace AutoAid.Infrastructure.Kafka
{
    public class KafkaProducerClient 
    {
        public Task<bool> Produce(string message)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var result = producer.ProduceAsync("test-topic", new Message<Null, string> { Value = message.ToString() }).GetAwaiter().GetResult();
                    return Task.FromResult(result.Status == PersistenceStatus.Persisted);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Delivery failed: {ex.Message}");
                    return Task.FromResult(false);
                }
            }
        }
    }
}
