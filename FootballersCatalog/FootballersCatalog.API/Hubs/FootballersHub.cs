using FootballersCatalog.API.Contracts;
using FootballersCatalog.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace FootballersCatalog.API.Hubs
{
    public class FootballersHub: Hub
    {
        public async Task Sender(FootballersResponse model)
            => await Clients.All.SendAsync("Receive", model);

        public async Task Editor(FootballersResponse model)
            => await Clients.All.SendAsync("ReceiveEdit", model);

        public async Task Deleter(string id)
            => await Clients.All.SendAsync("ReceiveDelete", id);
    }
}
