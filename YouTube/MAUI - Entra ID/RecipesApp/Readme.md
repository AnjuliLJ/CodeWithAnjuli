# Setup for connecting with Microsoft Entra ID
In this folder you will find a .NET MAUI app which you can use to connect with Microsoft Entra ID. I've used this project in my YouTube video, which you can find [here](https://www.youtube.com/watch?v=UaGNCPq5emQ). In my video I will explain how you can register and configure your app in the Microsoft Entra admin center, and where you can find the necessary information you need to use the code in this repository.

In the Microsoft Entra admin center, you need the following information:
- Application (Client) ID
- Tenant name
- Scopes (if you've defined your own)
- IOSKeychain security group (if you've defined your own)

## General setup
Open the EntraConfig.cs class and change the TENANT text in the Authority property to your tenant's name. Next, replace the CLIENT_ID with the Application (Client) ID that is linked to your registration in Microsoft Entra ID. If necessary, also add/change the Scopes and IOSKeychainSecurityGroup.

## Android-specific setup
Open the MsalActivity.cs class, which you can find in the Platforms/Android folder. Here you have to replace the CLIENT_ID part with the Application (Client) ID you fetched from the Microsoft Entra admin center. 

Next, open the AndroidManifest.xml. Here you will find two {CLIENT_ID} tags which you will also have to replace with the Application (Client) ID.
