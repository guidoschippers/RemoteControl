using RemoteControl.Web.Enums;

namespace RemoteControl.Web.Interfaces
{
    public interface IRemoteCommandHandler
    {
        Task SendRemoteCommandAsync(CommandType commandType, string argument, bool force);
    }
}