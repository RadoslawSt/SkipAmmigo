namespace JumpApp.Features.ADB2C
{
    public static class B2CConstants
    {
        // Azure AD B2C Coordinates
        public static string Tenant = "skipammigob2c.onmicrosoft.com";
        public static string AzureADB2CHostname = "skipammigob2c.b2clogin.com";
        public static string ClientID = "Client_ID";
        public static string PolicySignUpSignIn = "B2C_1A_signup_signin";
        public static string PolicyEditProfile = "B2C_1A_ProfileEdit";
        public static string PolicyResetPassword = "B2C_1A_PasswordReset";

        public static string[] Scopes = { "Scopes" };

        public static string AuthorityBase = $"https://{AzureADB2CHostname}/tfp/{Tenant}/";
        public static string AuthoritySignInSignUp = $"{AuthorityBase}{PolicySignUpSignIn}";
        public static string AuthorityEditProfile = $"{AuthorityBase}{PolicyEditProfile}";
        public static string AuthorityPasswordReset = $"{AuthorityBase}{PolicyResetPassword}";
        public static string IOSKeyChainGroup = "com.microsoft.adalcache";
    }
}
