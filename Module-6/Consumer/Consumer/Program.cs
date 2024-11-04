using avro;
using Avro.IO;
using Avro.Specific;
using Confluent.Kafka;

namespace Consumer;

class Program
{
    private static readonly DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static async Task Main(string[] args)
    {
        var consumerConfig = new ConsumerConfig
        {
            GroupId = "order-consumer-group",  // Уникальная группа потребителей
            BootstrapServers = "localhost:9092",  // Адрес Kafka брокера
            AutoOffsetReset = AutoOffsetReset.Earliest,  // Начало чтения с самого раннего сообщения
            EnableAutoCommit = false  // Отключаем автоматическое коммитирование смещений
        };

        // Создание Consumer для получения сообщений
        using var consumer = new ConsumerBuilder<Ignore, byte[]>(consumerConfig).Build();
        consumer.Subscribe("order-topic");  // Подписка на топик

        Console.WriteLine("Waiting for messages...");

        while (true)
        {
            try
            {
                // Получение сообщения из Kafka
                var consumeResult = consumer.Consume(CancellationToken.None);

                // Десериализация сообщения вручную с использованием Avro
                var order = DeserializeAvroMessage(consumeResult.Message.Value);

                // Вывод данных пользователя
                Console.WriteLine($"Received Order: ID = {order.orderID}, TotalCost = {order.totalCost}, CreatedAt = {start.AddSeconds(order.createdAt).ToLocalTime() }");
                Console.WriteLine("Dishes: ");
                foreach (var dish in order.dishesList)
                {
                    Console.WriteLine($"DishId = {dish.dishId}, Title = {dish.title}");
                }

                // Фиксируем смещение после успешной обработки сообщения
                consumer.Commit(consumeResult);
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Consume error: {e.Error.Reason}");
            }
        }
    }

    // Метод для десериализации Avro-сообщения
    static Order DeserializeAvroMessage(byte[] avroData)
    {
        var orderSchema = Order._SCHEMA;

        // Десериализация Avro-сообщения из двоичных данных
        using (var stream = new MemoryStream(avroData))
        {
            var reader = new BinaryDecoder(stream);
            var datumReader = new SpecificDatumReader<Order>(orderSchema, orderSchema);

            // Чтение и десериализация данных
            return datumReader.Read(null, reader);
        }
    }
}
