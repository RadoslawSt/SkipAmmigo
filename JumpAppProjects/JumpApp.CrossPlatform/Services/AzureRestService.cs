using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using JumpApp.Models;

namespace JumpApp.Services
{
    public class AzureRestService : IAzureRestService
    {
        HttpClient client;
        IEnumerable<Item> items;
       //IEnumerable<WorkoutSession> workoutSessions;

        public AzureRestService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{Constants.AzureBackendUrl}/");

            items = new List<Item>();
            //workoutSessions = new List<WorkoutSession>();
        }

       

        public async Task<Item> GetItemAsync(string id)
        {
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            if (item == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            if (item == null || item.Id == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var httpContent = new StringContent(serializedItem, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/item/{item.Id}", httpContent);

            return response.IsSuccessStatusCode;




        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> AddCoreUserInfo(CoreUserInfo coreUserInfo)
        {
            if (coreUserInfo == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(coreUserInfo);

            var response = await client.PostAsync($"api/CoreUserInfo", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> AddPublicUserInfo(UserInfo publicUserInfo)
        {
            if (publicUserInfo == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(publicUserInfo);

            var response = await client.PostAsync($"api/UserInfo", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateCoreUserInfo(CoreUserInfo coreUserInfo)
        {
            if (coreUserInfo == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(coreUserInfo);
            var httpContent = new StringContent(serializedItem, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/CoreUserInfo/{coreUserInfo.CoreUserId}", httpContent);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdatePublicUserInfo(UserInfo publicUserInfo)
        {
            if (publicUserInfo == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(publicUserInfo);
            var httpContent = new StringContent(serializedItem, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/UserInfo/{publicUserInfo.Id}", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<CoreUserInfo> GetCoreUserInfo(string loginId)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/CoreUserInfo/GetCoreUserInfoLogin/{loginId}");
                return await Task.Run(() => JsonConvert.DeserializeObject<CoreUserInfo>(json));
            }

            return null;
        }
        public async Task<UserInfo> GetPublicUserInfo(string loginId)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/UserInfo/GetPublicUserInfoLogin/{loginId}");
                return await Task.Run(() => JsonConvert.DeserializeObject<UserInfo>(json));
            }

            return null;
        }
        public async Task<UserInfo> GetPublicUserInfoFriendlyLogin(string friendlyLoginId)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/UserInfo/GetPublicUserInfoFriendlyLogin/{friendlyLoginId}");
                return await Task.Run(() => JsonConvert.DeserializeObject<UserInfo>(json));
            }

            return null;
        }
        public async Task<UserInfo> GetPublicUserInfo(int Id)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/UserInfo/GetPublicUserInfoId/{Id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<UserInfo>(json));
            }
            return null;
        }
        public async Task<WorkoutSession> GetWorkoutSessionById(int workoutSessionId)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/WorkoutSession/Id?Id={workoutSessionId}");
                return await Task.Run(() => JsonConvert.DeserializeObject<WorkoutSession>(json));
            }
            return null;
        }
        
        public async Task<bool> AddWorkoutSession(IEnumerable<WorkoutSession> workoutSessions)
        {
            if (workoutSessions.Equals(0) || !CrossConnectivity.Current.IsConnected)
                return false;
            var serializedItem = JsonConvert.SerializeObject(workoutSessions);
            var response = await client.PostAsync($"api/WorkoutSession", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
        public async Task<IEnumerable<WorkoutSession>> GetWorkoutSessions(string loginId)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/WorkoutSession?loginId={loginId}");
                return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<WorkoutSession>>(json));
            }
            return null;
        }
        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/item");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
            }

            return items;
        }
    }
}