using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FlightsToCharts.SharedLibrary.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightsToCharts.SharedLibrary.Utils
{
   public class ContainerData
   {
      // transfer link to vault
      private static BlobContainerClient container = new BlobContainerClient(System.Environment.GetEnvironmentVariable("ContainerConnString"), "data-to-process");

      public List<BlobMetadataExtended> GetAllBlobs()
      {
         var blobs = new List<BlobMetadataExtended>();

         try
         {
            var allBlobs = container.GetBlobs();
            foreach (var blob in allBlobs)
            {
               var newBlob = new BlobMetadataExtended();

               newBlob.Name = blob.Name;
               newBlob.LastModified = blob.Properties.LastModified.Value.LocalDateTime;
               newBlob.UploadDate = blob.Properties.CreatedOn.Value.LocalDateTime;

               blobs.Add(newBlob);
            }
            return blobs;
         }
         catch (Exception)
         {
            return null;
         }
      }
   }
}
