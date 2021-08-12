
namespace JumpApp.Services
{
    public static class Constants
    {
        public static string ApplicationID = "ApplicationID";
        
        public static string[] Scopes = { ApplicationID };
        public static string SignUpSignInPolicy = "B2C_1A_signup_signin";
        public static string ResetPasswordPolicy = "B2C_1_reset";
        public static string Authority = "Authority";
        public static string AzureBackendUrl = "AzureBackendURL";
        //public static string AzureBackendUrl = "http://localhost:44444/";
        public static string StorageConnectionString = "Storage_ConnectionString";
        public static string StorageContainerName = "profileimages";
    }
}

