using Microsoft.Extensions.Configuration;
using SecretsManagerFacadeLib.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretsManagerFacadeLib.Interfaces.Clients.Impl
{
    public class AWSCredentialsFacade : ICredentialsFacade<AwsCredentials>
    {
        private readonly ISecretsManagerFacade _secretsManager;
        private readonly IConfiguration _config;

        public AWSCredentialsFacade(ISecretsManagerFacade secretsManager, IConfiguration config)
        {
            _secretsManager = secretsManager;
            _config = config;
        }

        public AwsCredentials GetCredentials()
        {
            return GetFromConfiguration() ?? GetFromSecretsManagerAsync();
        }

        private AwsCredentials GetFromSecretsManagerAsync()
        {
            var AwsSection = _config.GetSection("Aws");
            var SecretId = AwsSection["SecretsManager"];

            if (string.IsNullOrEmpty(SecretId))
                return null;

            return _secretsManager.GetObjectProperty<AwsCredentials>(SecretId);
        }

        private AwsCredentials GetFromConfiguration()
        {
            var section = _config.GetSection("Aws:Credentials");

            if (section.Exists())
            {
                return new AwsCredentials
                {
                    AccountId = section["AccountId"],
                    AccessKey = section["AccessKey"],
                    SecretKey = section["SecretKey"],
                    Region = section["Region"]
                };
            }

            return null;
        }
    }
}
