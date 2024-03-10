using Elumini.Tarefa.Domain.Interfaces;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace Elumini.Tarefa.Application.Services
{
    public class MensageriaService : IMensageriaService
    {
        private readonly ConnectionFactory _connectionFactory;

        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MensageriaService()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest", // Altere conforme necessário
                Password = "guest"  // Altere conforme necessário
            };

            // Alocar instância de conexão e canal no construtor
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();


        }

        public async Task<bool> InserirOuAtualizar(byte[] body, MensageriaQueue queueName)
        {
            try
            {
                // Declaração da fila
                _channel.QueueDeclare(queue: queueName.ToString(), durable: true, exclusive: false, autoDelete: false, arguments: null);

                // Lógica para inserir ou atualizar mensagem na fila
                _channel.BasicPublish(exchange: "", routingKey: queueName.ToString(), basicProperties: null, body: body);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public byte[] SerializeObjectToBytes(object obj) => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));

        public string ObterMensagemAssync(MensageriaQueue queueName)
        {
            try
            {
                BasicGetResult result = _channel.BasicGet(queueName.ToString(), autoAck: true);

                if (result != null)
                {
                    var corpo = result.Body.ToArray();
                    var mensagem = Encoding.UTF8.GetString(corpo);
                    return mensagem;
                }
                else
                {
                    // Retorna null se não houver mensagem na fila
                    return null;
                }
            }
            catch (Exception e)
            {
                // Lida com exceções, faça o tratamento apropriado para sua aplicação
                throw;
            }
        }


        // Dispose para liberar recursos quando a instância da classe for descartada
        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
