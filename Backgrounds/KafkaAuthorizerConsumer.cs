using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using EscortBookAuthorizerConsumer.Types;
using EscortBookAuthorizerConsumer.Repositories;

namespace EscortBookAuthorizerConsumer.Backgrounds
{
    public class KafkaAuthorizerConsumer : BackgroundService
    {
        #region snippet_Properties

        private readonly IConfiguration _configuration;

        private readonly ILogger _logger;

        private readonly IAccessTokenRepository _accessTokenRepository;

        private readonly IUserRepository _userRepository;

        #endregion

        #region snippet_Constructors

        public KafkaAuthorizerConsumer
        (
            IConfiguration configuration,
            ILogger<KafkaAuthorizerConsumer> logger,
            IAccessTokenRepository accessTokenRepository,
            IUserRepository userRepository
        )
        {
            _configuration = configuration;
            _logger = logger;
            _accessTokenRepository = accessTokenRepository;
            _userRepository = userRepository;
        }

        #endregion

        #region snippet_ActionMethods

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _configuration["Kafka:GroupId"],
                BootstrapServers = _configuration["Kafka:Servers"],
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true
            };

            using var builder = new ConsumerBuilder<Ignore, string>(config).Build();
            builder.Subscribe(_configuration["Kafka:Topics:BlockDeleteUser"]);

            var cancelToken = new CancellationTokenSource();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumer = builder.Consume(cancelToken.Token);
                    var kafkaBlockUserEvent = JsonConvert
                        .DeserializeObject<KafkaBlockUserEvent>(consumer.Message.Value);

                    var user = await _userRepository.GetByIdAsync(kafkaBlockUserEvent.UserId);

                    if (user is null) continue;

                    user.Block = kafkaBlockUserEvent.Status == "Locked";
                    user.Deactivated = kafkaBlockUserEvent.Status == "Deactivated";
                    user.Delete = kafkaBlockUserEvent.Status == "Deleted";

                    var tasks = new Task[]
                    {
                        _userRepository.UpdateAsync(u => u.Id == user.Id, user),
                        _accessTokenRepository.DeleteAsync(t => t.User == user.Email)
                    };

                    await Task.WhenAll(tasks);
                }
                catch (Exception e)
                {
                    _logger.LogError($"AN ERROR HAS OCCUR: {e.Message}");
                    builder.Close();
                }
            }
        }

        #endregion
    }
}
