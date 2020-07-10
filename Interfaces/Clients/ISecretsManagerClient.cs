using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretsManagerFacadeLib.Interfaces.Clients
{
    public interface ISecretsManagerClient
    {
        Task<T> GetSecret<T>(string secretName, Func<string, T> valueFunction);
    }
}
