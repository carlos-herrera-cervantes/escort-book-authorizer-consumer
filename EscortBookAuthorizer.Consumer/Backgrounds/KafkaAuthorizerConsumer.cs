using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MongoDB.Driver;
using Confluent.Kafka;
using EscortBookAuthorizer.Consumer.Types;
using EscortBookAuthorizer.Consumer.Repositories;
using EscortBookAuthorizer.Consumer.Constants;
using EscortBookAuthorizer.Consumer.Models;

namespace EscortBookAuthorizer.Consumer.Backgrounds;

public class KafkaAuthorizerConsumer : BackgroundService
{
    #region snippet_Properties

    private readonly ILogger _logger;

    private readonly IAccessTokenRepository _accessTokenRepository;

    private readonly IUserRepository _userRepository;

    private readonly IConsumer<Ignore, string> _consumer;

    #endregion

    #region snippet_Constructors

    public KafkaAuthorizerConsumer(
        ILogger<KafkaAuthorizerConsumer> logger,
        IAccessTokenRepository accessTokenRepository,
        IUserRepository userRepository,
        IConsumer<Ignore, string> consumer
    )
    {
        _logger = logger;
        _accessTokenRepository = accessTokenRepository;
        _userRepository = userRepository;
        _consumer = consumer;
    }

    #endregion

    #region snippet_ActionMethods

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(KafkaTopic.BlockDeleteUser);

        var cancelToken = new CancellationTokenSource();

        while (!stoppingToken.IsCancellationRequested)
        {
            await UpdateAccountStatus(cancelToken);
        }
    }

    private async Task UpdateAccountStatus(CancellationTokenSource cancelToken)
    {
        try
        {
            var consumer = _consumer.Consume(cancelToken.Token);
            var kafkaBlockUserEvent = JsonConvert.DeserializeObject<KafkaBlockUserEvent>(consumer.Message.Value);

            var user = await _userRepository.GetAsync(Builders<User>.Filter.Eq(u => u.Id, kafkaBlockUserEvent.UserId));

            if (user is null) return;

            user.Block = kafkaBlockUserEvent.Status == "Locked";
            user.Deactivated = kafkaBlockUserEvent.Status == "Deactivated";
            user.Delete = kafkaBlockUserEvent.Status == "Deleted";

            var tasks = new Task[]
            {
                _userRepository.UpdateAsync(Builders<User>.Filter.Eq(u => u.Id, user.Id), user),
                _accessTokenRepository.DeleteManyAsync(Builders<AccessToken>.Filter.Eq(t => t.User, user.Email))
            };

            await Task.WhenAll(tasks);
        }
        catch (Exception e)
        {
            _logger.LogError($"AN ERROR HAS OCCUR: {e.Message}");
            _consumer.Close();
        }
    }

    #endregion
}
