using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using JumpApp.Services;
using System.IO;
using JumpApp.Droid;
using JumpApp.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection))]
namespace JumpApp.Droid
{
   public class DatabaseConnection : IDatabaseConnection
    {
        public SQLiteConnection GetDbConnection()
        {
            //var dbName = "UserDb.db";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            // Documents folder  
            var path = Path.Combine(documentsPath, DatabaseHelper.DbFileName);
            //var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            
            return new SQLiteConnection(path);

            // Return the database connection  
            
        }
    }
}