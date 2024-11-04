using avro;
using Avro.IO;
using Avro.Specific;
using Confluent.Kafka;

namespace Producer;

class Program
{
    public static async Task Main(string[] args)
    {
        var order1 = new Order
        {
            orderID = 1,
            createdAt = 1730730732,
            totalCost = 500,
            dishesList = new List<Dishes>
            {
                new() { dishId = 1, title = "Salad"},
                new() { dishId = 2, title = "First dish"}
            }
        };
        
        var order2 = new Order
        {
            orderID = 2,
            createdAt = 1730730855,
            totalCost = 600,
            dishesList = new List<Dishes>
            {
                new() { dishId = 1, title = "Salad"},
                new() { dishId = 3, title = "Second dish"}
            }
        };
        
        // Сериализация объекта User в байты с помощью Avro
        byte[] avroData1;
        using (var ms = new MemoryStream())
        {
            var writer = new BinaryEncoder(ms);
            var datumWriter = new SpecificDatumWriter<Order>(order1.Schema);
            datumWriter.Write(order1, writer);
            avroData1 = ms.ToArray();
        }
        
        byte[] avroData2;
        using (var ms = new MemoryStream())
        {
            var writer = new BinaryEncoder(ms);
            var datumWriter = new SpecificDatumWriter<Order>(order2.Schema);
            datumWriter.Write(order2, writer);
            avroData2 = ms.ToArray();
        }
        
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            LingerMs = 5,  // Задержка в миллисекундах перед отправкой пакета сообщений
            BatchSize = 16384, // Размер пакета в байтах
            RequestTimeoutMs = 10000 // Таймаут операции отправки запроса в Kafka
        };

        using (var producer = new ProducerBuilder<Null, byte[]>(config).Build())
        {
            var message = new Message<Null, byte[]>
            {
                Value = avroData1
            };

            var deliveryResult = producer.ProduceAsync("order-topic", message).GetAwaiter().GetResult();
            Console.WriteLine($"Message delivered to {deliveryResult.TopicPartitionOffset}");
            
            message = new Message<Null, byte[]>
            {
                Value = avroData2
            };

            deliveryResult = producer.ProduceAsync("order-topic", message).GetAwaiter().GetResult();
            Console.WriteLine($"Message delivered to {deliveryResult.TopicPartitionOffset}");
        }
    }
}