# SecretsManagerFacadeLib
Convenience lib that offers support to retrieve Aws Credentials and Aws Secrets Manager stored properties.  
It uses client side caching (supported by official Aws cache lib) to prevent further http calls.

### Usage ###

**1. Declare components in Startup.cs :**

```
services.AddSingleton(RegionEndpoint.SAEast1);  // inject the desired aws region
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

#### For AwsCredentials ####
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

#### For general string-based properties ####

Example: to retrieve a hypothetical ApiKey value (stored in plain string in Secrets Manager), you'll want your appsettings.json like this:

```
"MySection": {
    "SecretsManager": "YOUR-SECRET-ID"
}
```

Then call it in your code:

```
var MySection = _configuration.GetSection("MySection");
var SecretId = MySection["SecretsManager"];
var ApiKey = _credentialsFacade.GetStringProperty(SecretId);
```

#### For general object-based properties ####

Example: to retrieve a hypothetical object with username and password properties, you'll want your appsettings.json like this:

```
"Credentials": {
    "SecretsManager": "YOUR-SECRET-ID"
}
```

Then call it in your code:
```
var MyCredentialsSection = _configuration.GetSection("Credentials");
var SecretId = MySection["SecretsManager"];
var mySecretValue = _credentialsFacade.GetObjectProperty<T>(SecretId);
```

Again, you have to store the object in Aws Secrets Manager using a Json format. In this case:

```
{
  "username": "YOUR-USERNAME",
  "password": "YOUR-PASSWORD"
}
```


### IMPORTANT ###

For Aws credentials, the section must be declared in appsettings.json **like this**:

```
"Aws": {
    "SecretsManager": "YOUR-SECRET-ID"
}
```

It works parsing hardcoded values too:

```
"Aws": {
    "AccountId": "YOUR-ACCOUNT-ID",
    "AccessKey": "YOUR-ACCESS-KEY",
    "SecretKey": "YOUR-SECRET-KEY",
    "Region": "YOUR-REGION-ID"
}
```
although, for security reasons, is strongly suggested that you use Secrets Manager stored property values.
