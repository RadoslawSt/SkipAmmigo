using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

namespace JumpApp.MobileAppService
{
    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        var host = new WebHostBuilder()
    //            .UseKestrel()
    //            .UseContentRoot(Directory.GetCurrentDirectory())
    //            .UseIISIntegration()
    //            .UseStartup<Startup>()

    //            .Build();

    //        host.Run();
    //    }
    //    public static IWebHost BuildWebHost(string[] args) =>
    //WebHost.CreateDefaultBuilder(args)
    //    .UseStartup<Startup>()
    //    .Build();
    //}
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            
                .UseStartup<Startup>()
                .Build();




        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //    .ConfigureAppConfiguration((context, config) =>
        //    {
        //        var builtConfiguration = config.Build();

        //        string KeyVaultURL = builtConfiguration["KeyVaultConfig:KeyVaultURL"];
        //        string tenantId = builtConfiguration["KeyVaultConfig:TenantId"];
        //        string clientId = builtConfiguration["KeyVaultConfig:ClientId"];
        //        string clientSecret = builtConfiguration["KeyVaultConfig:ClientSecretId"];

        //        var credentials = new ClientSecretCredential(tenantId, clientId, clientSecret);

        //        var client = new SecretClient(new Uri(KeyVaultURL), credentials);
        //        config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
        //    })
        //        .UseStartup<Startup>()
        //        .Build();
    }
}
