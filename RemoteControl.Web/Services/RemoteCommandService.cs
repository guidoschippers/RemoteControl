using Grpc.Core;
using RemoteControl.Web.Enums;
using RemoteControl.Web.Interfaces;

namespace RemoteControl.Web.Services
{
    public class RemoteCommandService : RemoteCommand.RemoteCommandBase
    {
        private readonly ILogger<RemoteCommandService> logger;
        private readonly IRemoteCommandHandler commandHandler;

        public RemoteCommandService(ILogger<RemoteCommandService> logger, IRemoteCommandHandler commandHandler)
        {
            this.logger = logger;
            this.commandHandler = commandHandler;
        }

        public override async Task<RemoteCommandReply> InvokeCommand(RemoteCommandRequest request, ServerCallContext context)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Name.ToLowerInvariant() == "shutdown")
                await commandHandler.SendRemoteCommandAsync(CommandType.Shutdown, request.Argument, force: request.Force);

            return new RemoteCommandReply { Response = "Okidoki" };
        }
    }
}