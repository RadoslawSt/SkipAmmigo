﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace JumpApp.Services
{
   public abstract class BaseBlobStorageService
    {
        #region Constant Fields
        readonly static Lazy<CloudStorageAccount> _cloudStorageAccountHolder = new Lazy<CloudStorageAccount>(() => CloudStorageAccount.Parse(Constants.StorageConnectionString));
        readonly static Lazy<CloudBlobClient> _blobClientHolder = new Lazy<CloudBlobClient>(_cloudStorageAccountHolder.Value.CreateCloudBlobClient);
        #endregion

        #region Properties
        static CloudBlobClient BlobClient => _blobClientHolder.Value;
        #endregion

        #region Methods
        protected static async Task<CloudBlockBlob> SaveBlockBlob(string containerName, byte[] blob, string blobTitle)
        {
            var blobContainer = GetBlobContainer(containerName);

            var blockBlob = blobContainer.GetBlockBlobReference(blobTitle);
            await blockBlob.UploadFromByteArrayAsync(blob, 0, blob.Length).ConfigureAwait(false);

            return blockBlob;
        }
        public static async Task<bool> DeleteBlobFileAsync(string containerName, string name)
        {
            //var container = GetContainer(containerType);
            var blobContainer = GetBlobContainer(containerName);
            var blob = blobContainer.GetBlobReference(name);
            return await blob.DeleteIfExistsAsync();
        }
        protected static async Task<List<T>> GetBlobs<T>(string containerName, string prefix = "", int? maxresultsPerQuery = null, BlobListingDetails blobListingDetails = BlobListingDetails.None) where T : ICloudBlob
        {
            var blobContainer = GetBlobContainer(containerName);

            var blobList = new List<T>();
            BlobContinuationToken continuationToken = null;

            try
            {
                do
                {
                    var response = await blobContainer.ListBlobsSegmentedAsync(prefix,
                                                                               true,
                                                                               blobListingDetails,
                                                                               maxresultsPerQuery,
                                                                               continuationToken,
                                                                               null,
                                                                               null).ConfigureAwait(false);
                    continuationToken = response?.ContinuationToken;

                    foreach (var blob in response?.Results?.OfType<T>())
                    {
                        blobList.Add(blob);
                    }

                } while (continuationToken != null);
            }
            catch (Exception e)
            {
                //DebugServices.Log(e);
            }

            return blobList;
        }

        static CloudBlobContainer GetBlobContainer(string containerName) => BlobClient.GetContainerReference(containerName);
        #endregion
    }
}
