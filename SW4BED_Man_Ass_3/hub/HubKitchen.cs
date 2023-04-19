using Microsoft.AspNetCore.SignalR;

namespace SW4BED_Man_Ass_3.hub
{
    public class HubKitchen : Hub<IHubKitchen>
    {
        public async Task KitchenReload() 
        {
            await Clients.All.KitchenReload();
        }
    }
}
