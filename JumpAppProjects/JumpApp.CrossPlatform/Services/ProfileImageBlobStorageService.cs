using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JumpApp.Models;
using Microsoft.WindowsAzure.Storage.Blob;

namespace JumpApp.Services
{
   public abstract class ProfileImageBlobStorageService : BaseBlobStorageService
    {
        #region Methods
        public static async Task<ProfileImage> SavePhoto(byte[] photo, string photoTitle)
        {
            try
            {
                var photoBlob = await SaveBlockBlob(Constants.StorageContainerName, photo, photoTitle).ConfigureAwait(false);
                return new ProfileImage { Title = photoBlob.Name, Uri = photoBlob.Uri };
            }
            catch (Exception e)
            {
                //DebugServices.Log(e);
                return null;
            }
        }
        public static async Task<bool> DeleteBlobFileAsync(string name)
        {
            return await DeleteBlobFileAsync(Constants.StorageContainerName, name).ConfigureAwait(false);
        }
        public static async Task<List<ProfileImage>> GetPhotos()
        {
            var blobList = await GetBlobs<CloudBlockBlob>(Constants.StorageContainerName).ConfigureAwait(false);

            return blobList.Select(x => new ProfileImage { Title = x.Name, Uri = x.Uri }).ToList();
        }
        //public static async Task<List<ProfileImage>> GetUserPhotos()
        //{
        //    var blobList = await GetBlobs<CloudBlockBlob>(Constants.StorageContainerName).ConfigureAwait(false);

        //    blobList.Select(x => new ProfileImage { Title = x.Name, Uri = x.Uri }).ToList();
            
        //}
        #endregion
    }
}
