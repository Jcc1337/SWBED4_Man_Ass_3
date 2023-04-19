using Microsoft.AspNetCore.SignalR;

namespace SW4BED_Man_Ass_3.hub
{
    public class HubKitchen : Hub
    {
        public async Task kitchenReaload() 
        {
            await Clients.All.kitchenReload();
        }
    }
}
