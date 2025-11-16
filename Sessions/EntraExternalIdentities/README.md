# Entra External Identities Example
In this project you will find a .NET Blazor app (MovieApp) that connects to Microsoft Entra ID to aquire a token. With this token the app can make requests to the MovieAPI.

## Steps to setup your .NET Blazor Application
To setup your Blazor application, you have to add some configuration in your Blazor project and in the Entra ID portal.

### MovieApp project (Blazor)
In the .NET Blazor project you have to install the Microsoft.Identity.Web NuGet package. After that you should enable authentication in your Program.cs file.
``` c#
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("EntraID", options.ProviderOptions.Authentication);
});
```

The next step is to add the EntraID section to the appsettings.json file. You need the Authority, ClientId and ValidateAuthority fields. If you want to create a more secure connection, you should also add a ClientSecret. But for this demo, I will leave it out.

``` json
  "EntraID": {
    "Authority": "https://YOUR_SUBDOMAIN.ciamlogin.com/YOUR_SUBDOMAIN.onmicrosoft.com",
    "ClientId": "CLIENT_ID",
    "ValidateAuthority": false
  }
```

### Entra ID Blazor App Registration
In the Entra ID portal you have to register a new application. You have to go to the tab called 'App Registrations' and click on 'New Registration'. 
Here you can choose a name for your application and select the supported account types. For external tenants, it is recommended to select **Accounts in this organizational directory only (Your_Tenant_Name only - Single tenant)**.

The last thing you have to do is to add a redirect URI. This is the URL where the user will be redirected after the authentication process. Because this is a Blazor application, you have to choose the option 'Single-page Application (SPA)' as the Platform type. In this example I will use the following URL: `https://localhost:7290/authentication/login-callback`.
I'm using localhost in this example because I'm running the Blazor app on my local machine. If you want to run this app on a server, you have to change the URL to the URL of your server. You can find the port number in the launchSettings.json file in this project (Properties/LaunchSettings.json).

Now you can click on 'Register' to finish the registration process. You will automatically be redirected to the 'Overview' page of your application. Here you can find the Application (Client) ID and the Tenant ID. Copy the Client ID and paste it in the appsettings.json file of the Blazor project.

### Enable SPA (Single Page Application) settings
When your app registration is finished, you have to enable the SPA settings. To do this, navigate to the 'Authentication' tab and click on 'Settings'. Here you have to enable the two checkboxes underneath 'Implicit grant and hybrid flows', these checkboxes are called 'Access tokens (used for implicit flows)'
and 'ID tokens (used for implicit and hybrid flows)'.

### Entra ID User Flow
Most of the time, you want your users to be able to create an account on your application. To do this, you have to create a user flow. In the Entra ID portal, navigate to the 'External Identities' tab and click on 'User flows'. Here you can create a new user flow by clicking on the 'New user flow' button.

On the Create user flow page, you can choose a Name for the user flow and select the Identity providers you want to use. For this example, I didn't configure any identity providers. This means that you can only select the 'Email accounts' option. You have the choice to choose whether you want to let the users sign up with a password or with a one-time passcode. Keep in mind that when you choose the password option, your users will also recieve a one-time passcode via email, to verify their email address.

The last thing you can configure is the user attributes. Here you can choose which attributes you want to collect from your users. For this example, I only want to collect the Display Name. When you click on 'Show  more', you'll see that Email Address is also included by default. The user attributes you choose will be returned in the ID token. The user will be prompted to enter the User Attributes during the registration process.

When you're done, click on 'Create' to finish the user flow creation process. This will navigate back to the User flows page. Click on the newly created user flow to open the overview page. Here you should click on the 'Applications' tab and add your Blazor app to the list of applications.

At this point you should be able to run your Blazor application and create a new account when clicking on the Login button. 

## Steps to setup your WebAPI
In a real-world scenario, you will want to access your API from your Blazor application. The following steps will show you how to protect your API with Microsoft Entra ID and how you can access the API from your Blazor application.

## MovieAPI project (WebAPI)
In the .NET WebAPI project you have to install the Microsoft.Identity.Web Nuget package. After that you should enable Authentication and Authorization in your Program.cs:
``` c#
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("EntraID"));
    
    ...
    
    app.UseAuthorization();
```

This piece of code refers to the builder.Configuration.GetSection("EntraID") line. This line will load the EntraID section from the appsettings.json file. In your appsettings.json file, you need to define the following fields:
``` json 
  "EntraID": {
    "Instance": "https://YOUR_SUBDOMAIN.ciamlogin.com/",
    "TenantId": "",
    "ClientId": "",
    "Scopes": {
      "Read": [ "YOUR_PERMISSION" ]
    }
  }
```

In the Entra ID portal, navigate to the 'Overview' tab and click on the 'Overview' button. Here you can find the Tenant ID and the Primary domain properties. Add the Tenant ID in the appsettings.json file.
For the Instance field, you have to change YOUR_SUBDOMAIN to your Entra ID subdomain. If your subdomain is 'moviefans.onmicrosoft.com', you have to replace YOUR_SUBDOMAIN with 'moviefans'.

## Entra ID API Registration
In the Entra ID portal, navigate to the 'App Registrations' tab and click on 'New Registration'. Here you can choose a name for your application and select the supported account types. 
For external tenants, it is recommended to select **Accounts in this organizational directory only (Your_Tenant_Name only - Single tenant)**. Don't configure a redirect URI for this application.

When you click 'Register', you will be redirected to the 'Overview' page of your application. Here you can find the Application (Client) ID. Copy the Client ID and paste it in the appsettings.json file of the WebAPI project.

## Add a custom scope
In the Entra ID portal, navigate to the 'Expose an API' tab and click on 'Add a scope'. This will open a window where you will be asked to set a Application ID URI, if this is not already set. This is the endpoint where the scopes will be made available to your application.
You can click 'Save and Continue'. 

After that, you can choose a Scope name. For this example, you can choose 'read_movies'. There are a lot of other settings that need to be set: all of these settings are related with who is giving consent to add this scope (permission) to the application.
- Who can consent: this can be set to 'Admins and users' or 'Admins only', and it defines who can give consent to add this scope to the application.
- Admin consent display name: this is the name that will be displayed to the admin when giving consent to add this scope to the application.
- Admin consent description: this is the description that will be displayed to the admin when giving consent to add this scope to the application.
- User consent display name: this is the name that will be displayed to the user when giving consent to add this scope to the application.
- User consent description: this is the description that will be displayed to the user when giving consent to add this scope to the application.
- State: this is the state of the scope. This should be set to 'Enabled' to enable the scope.

After you've set all the settings, click 'Add scope'. Then you can see the scope in the list of scopes. Here you can copy the scope value and paste it in the appsettings.json file of the WebAPI project.

## Add permission to the Blazor application
In the Entra ID portal, go to the 'App Registrations' tab and click on the registration of the Blazor application. Here you can click on the 'API permissions' tab and click on 'Add a permission'. Here you can click on the tab 'APIs my organization uses'. In the search field, you can search for the WebAPI project. After you've found the WebAPI project, click on it. In the 'Delegated permissions' section, you can select the scope you created in the previous step. For example: 'read_movies'.
The next step is to Grant admin consent for 'YOUR_TENANT_NAME'. If you aren't an admin it is possible that you have to ask an admin to grant consent.

In the Blazor application, you have to add the permission to the API in the Program.cs file.
```c#
    builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("EntraID", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("SCOPE_URL");
});
```
You can now try to run your Blazor application and access the API, by logging in and clicking on the 'Get name' button.