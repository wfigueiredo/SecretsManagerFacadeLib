using Microsoft.Extensions.Configuration;
using SecretsManagerFacadeLib.Contracts;
using SecretsManagerFacadeLib.Interfaces.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretsManagerFacadeLib.Interfaces.Clients.Impl
{
    public class BasicCredentialsFacade : ICredentialsFacade<BasicCredentials>
    {
        private readonly ISecretsManagerService _secretsManager;

        public BasicCredentialsFacade(ISecretsManagerService secretsManager)
        {
            _secretsManager = secretsManager;
        }

        public BasicCredentials GetCredentials(IConfigurationSection Section)
        {
            return GetFromConfiguration(Section) ?? GetFromSecretsManagerAsync(Section) ?? throw new Exception($"Could not retrieve any data from Configuration Section {Section.Path}");
        }

        private BasicCredentials GetFromConfiguration(IConfigurationSection Section)
        {
            return new BasicCredentials
            {
                Username = Section["Username"],
                Password = Section["Password"]
            };
        }

        private BasicCredentials GetFromSecretsManagerAsync(IConfigurationSection Section)
        {
            var secretId = Section["SecretsManager"];

            if (string.IsNullOrEmpty(Section["SecretsManager"]))
                return null;

            return _secretsManager.GetObjectProperty<BasicCredentials>(secretId);
        }
    }
}
