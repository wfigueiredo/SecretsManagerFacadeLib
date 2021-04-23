using Newtonsoft.Json;
using SecretsManagerFacadeLib.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretsManagerFacadeLib.Interfaces.Impl
{
    public interface ISecretsManagerService
    {
        string GetStringProperty(string secretName);
        T GetObjectProperty<T>(string secretName);
    }

    public class SecretsManagerService : ISecretsManagerService
    {
        private readonly ISecretsManagerClient _client;

        public SecretsManagerService(ISecretsManagerClient client)
        {
            _client = client;
        }

        public string GetStringProperty(string secretId)
        {
            return _client.GetSecret(secretId, x => x).Result;
        }

        public T GetObjectProperty<T>(string secretId)
        {
            return _client.GetSecret(secretId, JsonConvert.DeserializeObject<T>).Result;
        }
    }
}
