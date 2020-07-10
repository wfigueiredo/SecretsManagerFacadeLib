using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SecretsManagerFacadeLib.Contracts
{
    [DataContract]
    public class BasicCredentials
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}
