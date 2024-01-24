# Entra External Identities Example
In this project you will find a .NET MAUI app that connects to Microsoft Entra ID to aquire a token. With this token the app can make requests to the API.

## Setup
In the constructor of the PublicClientSingleton class (line 52) you'll have to change this line of code to your projects name:
``` c#
 // Load config
 var assembly = Assembly.GetExecutingAssembly();
 using var stream = assembly.GetManifestResourceStream("YOUR_PROJECT_NAME.appsettings.json");
 AppConfiguration = new ConfigurationBuilder()
     .AddJsonStream(stream)
     .Build();
```


## Documentation
You can find more information about Microsoft Entra External Identities on the following pages:
- 