using ConsoleAppBlobStorage.Settings;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;

namespace ConsoleAppBlobStorage
{
    public static class BlobSAS
    {
        private static readonly string policyName = "testpolicy";

        public static void SassingTheBLOB()
        {
            CloudStorageAccount storageAccount = Common.CreateStorageAccountFromConnectionString();

            // Create a blob client for interacting with the blob service.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // get a reference to the container
            var blobContainer = blobClient.GetContainerReference("sampleblob");
            blobContainer.CreateIfNotExists();

            // create the stored policy we will use, with the relevant permissions and expiry time
            var storedPolicy = new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(10),
                Permissions = SharedAccessBlobPermissions.Read |
                              SharedAccessBlobPermissions.Write |
                              SharedAccessBlobPermissions.List
            };

            // get the existing permissions (alternatively create new BlobContainerPermissions())
            var permissions = blobContainer.GetPermissions();

            // optionally clear out any existing policies on this container
            permissions.SharedAccessPolicies.Clear();
            // add in the new one
            permissions.SharedAccessPolicies.Add(policyName, storedPolicy);
            // save back to the container
            blobContainer.SetPermissions(permissions);

            // Now we are ready to create a shared access signature based on the stored access policy
            var containerSignature = blobContainer.GetSharedAccessSignature(null, policyName);
            // create the URI a client can use to get access to just this container
            var uri = blobContainer.Uri + containerSignature;
        }
    }
}
