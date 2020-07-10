using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Extensions.Caching;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretsManagerFacadeLib.Interfaces.Clients.Impl
{
    public class SecretsManagerClient : ISecretsManagerClient, IDisposable
    {
        private readonly ILogger<SecretsManagerClient> _logger;
        private readonly IAmazonSecretsManager _client;
        private readonly SecretsManagerCache _cache;

        public SecretsManagerClient(string awsRegion, ILogger<SecretsManagerClient> logger)
        {
            _logger = logger;
            var regionEndpoint = RegionEndpoint.GetBySystemName(awsRegion);
            _client = new AmazonSecretsManagerClient(regionEndpoint);
            _cache = new SecretsManagerCache(_client);
        }

        public async Task<T> GetSecret<T>(string secretName, Func<string, T> valueFunction)
        {
            if (string.IsNullOrEmpty(secretName))
                throw new ArgumentNullException(nameof(secretName));

            try
            {
                var response = await _cache.GetSecretString(secretName);
                return valueFunction(response);
            }
            catch (ResourceNotFoundException)
            {
                _logger.LogError($"The requested secret {secretName} was not found");
                throw;
            }
            catch (InvalidRequestException ex)
            {
                _logger.LogError($"The request to secret was invalid due to: {ex.Message}");
                throw;
            }
            catch (InvalidParameterException ex)
            {
                _logger.LogError($"The request to secret had invalid params: {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            _client.Dispose();
            _cache.Dispose();
        }
    }
}
