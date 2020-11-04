using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsToCharts.SharedLibrary.Domains
{
   public class ResponseMessage
   {
      public enum StatusCodeEnum
      {
         Ok = 0,
         Error = 1
      }

      public StatusCodeEnum StatusCode { get; set; }

      public string Data { get; set; }

      public string Message { get; set; }
   }
}
