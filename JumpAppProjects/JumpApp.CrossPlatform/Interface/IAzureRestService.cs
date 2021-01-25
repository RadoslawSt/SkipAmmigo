using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JumpApp.Models;
namespace JumpApp.Services
{
    public interface IAzureRestService
    {
        Task<bool> AddItemAsync(Item item);
        Task<bool> UpdateItemAsync(Item item);
        Task<bool> DeleteItemAsync(string id);
        Task<Item> GetItemAsync(string id);
        Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false);

        Task<CoreUserInfo> GetCoreUserInfo(string loginID);
        Task<bool> AddCoreUserInfo(CoreUserInfo coreUserInfo);
        Task<bool> UpdateCoreUserInfo(CoreUserInfo coreUserinfo);

        Task<UserInfo> GetPublicUserInfo(string loginID);
        Task<bool> AddPublicUserInfo(UserInfo coreUserInfo);
        Task<bool> UpdatePublicUserInfo(UserInfo coreUserinfo);

        Task<bool> AddWorkoutSession(IEnumerable<WorkoutSession> workoutSessions);
        Task<IEnumerable<WorkoutSession>> GetWorkoutSessions(string loginId);
        Task<WorkoutSession> GetWorkoutSessionById(int workoutSessionId);
    }
}
