using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace NTT.Azure.Repository
{
    public class FileRepository
    {
        private string _accessKey = string.Empty;

        public FileRepository(string accesskey)
        {
            _accessKey = accesskey;
        }

        public string UploadFileToBlob(string strFileName, byte[] fileData, string fileMimeType, string container)
        {
            try
            {

                var _task = Task.Run(() => this.UploadFileToBlobAsync(strFileName, fileData, fileMimeType, container));
                _task.Wait();
                string fileUrl = _task.Result;
                return fileUrl;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<string> GetUrlsByFolder(string container, string folder)
        {
            var task = Task.Run(() => GetUrlsByFolderAsync(container, folder));
            task.Wait();
            var urls = task.Result;
            return urls;
        }

       public bool FileExist(string container, string file)
        {
            var task = Task.Run(() => FileExistAsync(container, file));
            task.Wait();
            return task.Result;
        }

        public async Task<List<string>> GetUrlsByFolderAsync(string container, string folder)
        {
            //Get storage account reference
            BlobContinuationToken continuationToken = null;
            var cloudStorageAccount = CloudStorageAccount.Parse(_accessKey);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(container);
            var urls = new List<string>();
            do
            {
                bool useFlatBlobListing = true;
                BlobListingDetails blobListingDetails = BlobListingDetails.None;
                int maxBlobsPerRequest = 500;
                var response = await cloudBlobContainer.ListBlobsSegmentedAsync($"{folder}/", useFlatBlobListing, blobListingDetails, maxBlobsPerRequest, continuationToken, null, null);
                continuationToken = response.ContinuationToken;
                urls.AddRange(response.Results.ToList().Select(item => item.StorageUri.PrimaryUri.AbsoluteUri));
            }
            while (continuationToken != null);
            return urls;
        }

        public async Task<bool> FileExistAsync(string container, string filename)
        {
            
            var cloudStorageAccount = CloudStorageAccount.Parse(_accessKey);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(container);
            
            return await cloudBlobContainer.GetBlockBlobReference(filename).ExistsAsync();
        }
        private async Task<string> UploadFileToBlobAsync(string fileName, byte[] fileData, string fileMimeType, string container)
        {
            try
            {
                var cloudStorageAccount = CloudStorageAccount.Parse(_accessKey);
                var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                var cloudBlobContainer = cloudBlobClient.GetContainerReference(container);

                if (await cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                }

                if (fileName != null && fileData != null)
                {
                    var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                    //Check if it exists on the blob container already, how to get correct string from container?

                        cloudBlockBlob.Properties.ContentType = fileMimeType;
                        await cloudBlockBlob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);
                        return cloudBlockBlob.Uri.AbsoluteUri;
                   
                    
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

    public static class BlobExtensions
    {
        public static bool Exists(this CloudBlob blob)
        {
            try
            {
                //Async suggested, normal FetchAttributes does not work
                blob.FetchAttributesAsync();
                return true;
            }
            catch (Exception e)
            {
                if (e != null)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
