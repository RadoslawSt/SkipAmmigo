
namespace JumpApp.Services
{
    public static class Constants
    {
        public static string ApplicationID = "d92cd0a0-44b5-408c-9326-f6d55e4dc4c9";
        
        public static string[] Scopes = { ApplicationID };
        public static string SignUpSignInPolicy = "B2C_1A_signup_signin";
        public static string ResetPasswordPolicy = "B2C_1_reset";
        public static string Authority = "https://login.microsoftonline.com/ADTest123Tenant.onmicrosoft.com/";
        public static string AzureBackendUrl = "https://jumpappbackendservice.azurewebsites.net";
        //public static string AzureBackendUrl = "http://localhost:44444/";
        public static string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=jumpappbackendservice;AccountKey=ZcnPhUhTcsfIMFQGuh/YyCVGAHIRkbTk/cgxp1gXFnSdFBa9hA5uYPoq5hq3/MFxFo8PaMUbcbc4Ccju6VjdVA==;EndpointSuffix=core.windows.net";
        public static string StorageContainerName = "profileimages";
    }
}

//https://ADTest123Tenant.b2clogin.com/ADTest123Tenant.onmicrosoft.com/oauth2/v2.0/authorize?p=B2C_1A_signup_signin&client_id=d92cd0a0