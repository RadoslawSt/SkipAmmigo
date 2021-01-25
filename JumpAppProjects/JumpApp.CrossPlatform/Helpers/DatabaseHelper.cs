using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using JumpApp.Services;
using JumpApp.Models;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices;

using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Diagnostics;

namespace JumpApp.Helpers
{
    public class DatabaseHelper
    {
        static SQLiteConnection sqliteconnection;
        public const string DbFileName = "User.db";
        //public MobileServiceClient MobileService { get; private set; }
        //private IMobileServiceSyncTable<CoreUserInfo> coreUserInfoTable;
        public DatabaseHelper()
        {
            if(sqliteconnection == null)
            {
                sqliteconnection = DependencyService.Get<IDatabaseConnection>().GetDbConnection();
            }
            
            //sqliteconnection.DropTable<CoreUserInfo>();
            //sqliteconnection.CreateTable<CoreUserInfo>();
            //sqliteconnection.DropTable<WorkoutSession>();
            //sqliteconnection.CreateTable<WorkoutSession>();
            //sqliteconnection.DropTable<WorkoutSessionPerMin>();
            sqliteconnection.CreateTable<WorkoutSessionPerMin>();
           
            
            //string stop = "";
            //this.MobileService = new MobileServiceClient(Constants.AzureBackendUrl);
            ////this.MobileService.SerializerSettings.Converters.Add(new MobileFileJsonConverter(this.MobileService));
            //var store = new MobileServiceSQLiteStore(DbFileName);
            //store.DefineTable<CoreUserInfo>();
            //this.coreUserInfoTable = this.MobileService.GetSyncTable<CoreUserInfo>();
        }
       
        //public async Task SyncCoreUserInfo()
        //{
        //    try
        //    {
        //        await MobileService.SyncContext.PushAsync();
                

        //        await coreUserInfoTable.PullAsync("coreUserInformation", this.coreUserInfoTable.CreateQuery());
        //    }

        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(@"Sync Failed: {0}", e.Message);
        //    }
        //}
        //public List<CoreUserInfo> GetAllUserInfoData()
        //{
        //    return (from data in sqliteconnection.Table<CoreUserInfo>()
        //            select data).ToList();
        //}
        // Insert new Contact to DB   
        //public void InsertUser(CoreUserInfo user)
        //{
        //    sqliteconnection.Insert(user);
        //}

        // Update Contact Data  
        //public void UpdateUser(CoreUserInfo user)
        //{
        //    sqliteconnection.Update(user);
        //}
        //public void DeleteUserInfo(CoreUserInfo user)
        //{
        //    sqliteconnection.Delete(user);
        //}
        //public void InsertPublicUser(UserInfo user)
        //{
        //    sqliteconnection.Insert(user);
        //}

        // Update Contact Data  
        //public void UpdatePublicUser(UserInfo user)
        //{
        //    sqliteconnection.Update(user);
        //}
        //public void DeletePublicUser(UserInfo user)
        //{
        //    sqliteconnection.Delete(user);
        //}
        //public void InsertWorkoutSession(WorkoutSession session)
        //{
        //    sqliteconnection.Insert(session);
        //}
        public void InsertWorkoutSessionDetails(WorkoutSessionPerMin sessionPerMin)
        {
            sqliteconnection.Insert(sessionPerMin);
        }
        //public List<WorkoutSession> GetWorkoutSessions()
        //{
        //    return (from data in sqliteconnection.Table<WorkoutSession>()
        //            select data).ToList();
        //}
        //public WorkoutSession GetIndividualWorkoutSession(int Id)
        //{
        //    return (from data in sqliteconnection.Table<WorkoutSession>()
        //            where data.WorkoutSessionId == Id
        //            select data).FirstOrDefault();
        //}
        public List<WorkoutSessionPerMin> GetWorkoutSessionDetails(int workoutID)
        {
            return (from data in sqliteconnection.Table<WorkoutSessionPerMin>()
                    where data.WorkoutId == workoutID
                    select data).ToList();
        }
        public List<int> GetAllAvailableWorkoutSessionsPerMin()
        {
            return (from data in sqliteconnection.Table<WorkoutSessionPerMin>()
                    select data.WorkoutId).ToList();
        }
        //// Get All Contact data      
        //public List<ContactInfo> GetAllContactsData()
        //{
        //    return (from data in sqliteconnection.Table<ContactInfo>()
        //            select data).ToList();
        //}

        ////Get Specific Contact data  
        //public ContactInfo GetContactData(int id)
        //{
        //    return sqliteconnection.Table<ContactInfo>().FirstOrDefault(t => t.Id == id);
        //}

        //// Delete all Contacts Data  
        //public void DeleteAllContacts()
        //{
        //    sqliteconnection.DeleteAll<ContactInfo>();
        //}

        //// Delete Specific Contact  
        //public void DeleteContact(int id)
        //{
        //    sqliteconnection.Delete<ContactInfo>(id);
        //}
    }
}
