using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SecretsManagerFacadeLib.Contracts
{
    [DataContract]
    public class AwsCredentials
    {
        [DataMember(Name = "accountid")]
        public string AccountId { get; set; }

        [DataMember(Name = "accesskey")]
        public string AccessKey { get; set; }

        [DataMember(Name = "secretkey")]
        public string SecretKey { get; set; }

        [DataMember(Name = "region")]
        public string Region { get; set; }
    }
}
