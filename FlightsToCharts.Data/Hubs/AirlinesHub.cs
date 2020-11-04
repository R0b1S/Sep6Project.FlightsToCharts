using FlightsToCharts.Data;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsToCharts.Data.Hubs
{
    public class AirlinesHub : Hub
    {
        private readonly SepDbContext _context;

        public AirlinesHub(SepDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
