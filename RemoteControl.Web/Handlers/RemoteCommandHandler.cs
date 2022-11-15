using Microsoft.AspNetCore.SignalR;
using RemoteControl.Web.Enums;
using RemoteControl.Web.Hubs;
using RemoteControl.Web.Interfaces;

namespace RemoteControl.Web.Handlers
{
    public class RemoteCommandHandler : IRemoteCommandHandler
    {
        private readonly IHubContext<CommandHub> hubContext;

        public RemoteCommandHandler(IHubContext<CommandHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task SendRemoteCommandAsync(CommandType commandType, string argument, bool force)
        {
            if (argument == null)
                throw new ArgumentNullException(nameof(argument));

            await hubContext.Clients.All.SendAsync(commandType.ToString(), argument);
        }
    }
}