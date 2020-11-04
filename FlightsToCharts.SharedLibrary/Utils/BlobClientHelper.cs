using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FlightsToCharts.SharedLibrary.Helpers
{
   public class BlobClientHelper
   {
      private static BlobContainerClient container = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=sep6;AccountKey=VguC59lkAyXJZ2simnaEsaYxuaTMfM5dMuMroj6gYwYl4enZHMjH5IlI3LFJLt6ApRGhsRxGdw37AUG5hu7yuw==;EndpointSuffix=core.windows.net", "airplane-project");

      public async Task<Response<BlobDownloadInfo>> GetBlob(string blobName)
      {
         try
         {
            BlobContainerProperties properties = container.GetProperties();
            foreach (var metadate in properties.Metadata)
            {
               Console.WriteLine(string.Format($"{metadate.Key}: {metadate.Value}"));
            }

            var blob = await new BlobClient(new Uri("https://sep6.blob.core.windows.net/airplane-project/" + blobName)).DownloadAsync();

            return blob;
         }
         catch (Exception e)
         {
            return null;
         }
      }

   }
}
