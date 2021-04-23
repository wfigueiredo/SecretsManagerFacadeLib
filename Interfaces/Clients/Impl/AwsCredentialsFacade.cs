using Microsoft.Extensions.Configuration;
using SecretsManagerFacadeLib.Contracts;
using SecretsManagerFacadeLib.Interfaces.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretsManagerFacadeLib.Interfaces.Clients.Impl
{
    public class AWSCredentialsFacade : ICredentialsFacade<AwsCredentials>
    {
        private readonly ISecretsManagerService _secretsManager;

        public AWSCredentialsFacade(ISecretsManagerService secretsManager)
        {
            _secretsManager = secretsManager;
        }

        public AwsCredentials GetCredentials(IConfigurationSection Section)
        {
            return GetFromConfiguration(Section) ?? GetFromSecretsManagerAsync(Section) ?? throw new Exception($"Could not retrieve any data from Configuration Section {Section.Path}");
        }

        private AwsCredentials GetFromConfiguration(IConfigurationSection AwsSection)
        {
            var CredentialSubSection = AwsSection.GetSection("Credentials");

            if (CredentialSubSection.Exists())
            {
                return new AwsCredentials
                {
                    AccountId = CredentialSubSection["AccountId"],
                    AccessKey = CredentialSubSection["AccessKey"],
                    SecretKey = CredentialSubSection["SecretKey"],
                    Region = CredentialSubSection["Region"]
                };
            }

            return null;
        }

        private AwsCredentials GetFromSecretsManagerAsync(IConfigurationSection AwsSection)
        {
            var SecretId = AwsSection["SecretsManager"];

            if (string.IsNullOrEmpty(SecretId))
                return null;

            return _secretsManager.GetObjectProperty<AwsCredentials>(SecretId);
        }
    }
}
