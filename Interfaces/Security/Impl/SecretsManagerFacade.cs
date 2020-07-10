using Newtonsoft.Json;
using SecretsManagerFacadeLib.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretsManagerFacadeLib.Interfaces.Impl
{
    public class SecretsManagerFacade : ISecretsManagerFacade
    {
        private readonly ISecretsManagerClient _client;

        public SecretsManagerFacade(ISecretsManagerClient client)
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
