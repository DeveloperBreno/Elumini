using Elumini.Tarefa.Domain.Interfaces;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics.SymbolStore;

namespace Elumini.Tarefa.Application.Services
{
    public class MensageriaService : IMensageriaService
    {

        private readonly ConnectionFactory _connectionFactory;
        private const string RabbitMQHostName = "localhost";

        public MensageriaService()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = RabbitMQHostName,
                Port = 5672,
                UserName = "guest", // Altere conforme necessário
                Password = "guest"  // Altere conforme necessário
            };
        }

        public async Task<bool> Inserir(byte[] body, MensageriaQueue queueName)
        {
            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName.ToString(), durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.BasicPublish(exchange: "", routingKey: queueName.ToString(), basicProperties: null, body: body);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public byte[] SerializeObjectToBytes(object obj) => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
    }
}