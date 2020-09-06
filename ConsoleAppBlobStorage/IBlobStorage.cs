using Microsoft.Azure.Storage.Blob;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleAppBlobStorage
{
    public interface IBlobStorage
    {
        Task<CloudBlockBlob> UploadBlobAsync(byte[] blobByteArray, string blobname, string title, string description);
        Task<bool> CheckIfBlobExistsAsync(string blobName);
        Task<IEnumerable<CloudBlockBlob>> ListBlobsAsync(string prefix = null, bool includeSnapshots = false);
        Task DownloadBlobAsync(CloudBlockBlob cloudBlockBlob, Stream targetStream);
        Task OverwriteBlobAsync(CloudBlockBlob cloudBlockBlob, byte[] blobByteArray, string name);
        Task DeleteBlobAsync(CloudBlockBlob cloudBlockBlob, string blobname);

        //Tip: Tuple<T>
        Task UpdateMetadataAsync(CloudBlockBlob cloudBlockBlob, string title, string description, string name);
       
        string GetBlobUriWithSasToken(CloudBlockBlob cloudBlockBlob);

        //Optional
        Task<string> AcquireOneMinuteLeaseAsync(CloudBlockBlob cloudBlockBlob);
        Task RenewLeaseAsync(CloudBlockBlob cloudBlockBlob, string leaseId);
        Task ReleaseLeaseAsync(CloudBlockBlob cloudBlockBlob, string leaseId);
       
    }
}
