using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SecretsManagerFacadeLib.Contracts
{
    [DataContract]
    public class AwsCredentials
    {
        [DataMember(Name = "AccountId")]
        public string AccountId { get; set; }

        [DataMember(Name = "AccessKey")]
        public string AccessKey { get; set; }

        [DataMember(Name = "SecretKey")]
        public string SecretKey { get; set; }

        [DataMember(Name = "Region")]
        public string Region { get; set; }
    }
}
