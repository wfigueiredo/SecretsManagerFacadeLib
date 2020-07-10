using System;
using System.Collections.Generic;
using System.Text;

namespace SecretsManagerFacadeLib.Interfaces
{
    public interface ISecretsManagerFacade
    {
        string GetStringProperty(string secretName);
        T GetObjectProperty<T>(string secretName);
    }
}
