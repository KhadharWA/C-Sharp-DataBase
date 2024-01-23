using Shared.Utils;

namespace Shared.Interfaces
{
    public interface IErrorLogger
    {
        void Log(string message);
        void Log(string message, LogTypes logTypes = LogTypes.Error);
        void Log(string message, string methodName = "", LogTypes logTypes = LogTypes.Error);
    }
}