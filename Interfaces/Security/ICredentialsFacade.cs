using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretsManagerFacadeLib.Interfaces
{
    public interface ICredentialsFacade<T>
    {
        T GetCredentials(IConfigurationSection Section);
    }
}
