using Azure.Storage.Blobs.Models;
using FlightsToCharts.Data.Utils;
using FlightsToCharts.SharedLibrary.Domains;
using FlightsToCharts.SharedLibrary.Utils;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightsToCharts.Data.Hubs
{
   public class SharedHub : Hub
   {
      private readonly SepDbContext _context;
      private readonly ContainerData _containerData;

      public SharedHub(SepDbContext context)
      {
         _context = context;
         _containerData = new ContainerData();
      }

      public async Task SendMessage(string user, string message)
      {
         await Clients.All.SendAsync("ReceiveMessage", user, message);
      }

      public async Task GetAllBlobsMetaData()
      {
         try
         {
            var blobsMetadata = _containerData.GetAllBlobs();
            await DbUtils.GetTableDataCount(_context, blobsMetadata);
            await Clients.Caller.SendAsync("SendBlobsMetadata", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(blobsMetadata), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendBlobsMetadata", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
   }
}
