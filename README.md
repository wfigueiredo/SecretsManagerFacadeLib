# SecretsManagerFacadeLib
Convenience lib that offers support to retrieve Aws Credentials and Aws Secrets Manager stored properties.  
It uses client side caching (supported by official Aws cache lib) to prevent further http calls.

### Usage ###

**1. Declare components in Startup.cs :**

```
services.AddSingleton<ISecretsManagerClient, SecretsManagerClient>();
services.AddSingleton<ISecretsManagerFacade, SecretsManagerFacade>();
services.AddSingleton<ICredentialsFacade<AwsCredentials>, AWSCredentialsFacade>();
```

**2. Inject the Facade client in your class :**

```
public SqsClient(ICredentialsFacade<AwsCredentials> credentialsFacade)
{
    _credentialsFacade = credentialsFacade;
}
```

**3. Retrieve the desired credentials**

### For AwsCredentials ###
```
var awsCredentials = _credentialsFacade.GetCredentials();
```

Of course: you have to store the credentials in Aws Secrets Manager using a Json format, for example:  

```
{
  "accountid": "YOUR-ACCOUNT-ID",
  "accesskey": "YOUR-ACCESS-KEY",
  "secretkey": "YOUR-SECRET-KEY",
  "region": "YOUR-REGION-ID"
}
```

### For string-based properties ###

```
var mySecretValue = _credentialsFacade.GetStringProperty("YOUR-SECRET-ID");
```

### For object-based properties ###

```
var mySecretValue = _credentialsFacade.GetObjectProperty<YourObjectType>("YOUR-SECRET-ID");
```

Again, you have to store the object in Aws Secrets Manager using a Json format, for example:

```
{
  "username": "YOUR-USERNAME",
  "password": "YOUR-PASSWORD"
}
```


### IMPORTANT ###

Aws section must be declared in appsettings.json like this:

```
"Aws": {
    "SecretsManager": "YOUR-SECRET-ID"
}
```

It works parsing hardcoded values too, but is strongly suggested that you use Secrets Manager stored values:

```
"Aws": {
    "AccountId": "YOUR-ACCOUNT-ID",
    "AccessKey": "YOUR-ACCESS-KEY",
    "SecretKey": "YOUR-SECRET-KEY",
    "Region": "YOUR-REGION-ID"
}
```
