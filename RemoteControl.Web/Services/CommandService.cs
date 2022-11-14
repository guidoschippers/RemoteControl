using Grpc.Core;
using RemoteControl.Web.Enums;
using RemoteControl.Web.Interfaces;

namespace RemoteControl.Web.Services
{
    public class CommandService : Command.CommandBase
    {
        private readonly ILogger<CommandService> logger;
        private readonly IRemoteCommandHandler commandHandler;

        public CommandService(ILogger<CommandService> logger, IRemoteCommandHandler commandHandler)
        {
            this.logger = logger;
            this.commandHandler = commandHandler;
        }

        public override async Task<CommandReply> InvokeCommand(CommandRequest request, ServerCallContext context)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            await commandHandler.SendRemoteCommandAsync(CommandType.Shutdown, "test");

            return new CommandReply { Response = "Okidoki" };
        }
    }
}