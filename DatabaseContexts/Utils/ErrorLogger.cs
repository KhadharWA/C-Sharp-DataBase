

using System.Diagnostics;
using Shared.Interfaces;

namespace Shared.Utils;

public class ErrorLogger(string filePath) : IErrorLogger
{
    private readonly string _filePath = filePath;

    public void Log(string message)
    {
        try
        {
            var logMessage = $"{DateTime.Now} :: {message} ";
            Debug.WriteLine($"{logMessage}");

            using var sw = new StreamWriter(_filePath, true);
            sw.WriteLine(logMessage);
        }
        catch (Exception ex) { Debug.WriteLine($"{DateTime.Now} :: Logger.Log() :: {LogTypes.Error} :: {ex.Message} "); }
    }

    public void Log(string message, LogTypes logTypes = LogTypes.Error)
    {
        try
        {
            var logMessage = $"{DateTime.Now} ::  {logTypes} :: {message} ";
            Debug.WriteLine($"{logMessage}");

            using var sw = new StreamWriter(_filePath, true);
            sw.WriteLine(logMessage);
        }
        catch (Exception ex) { Debug.WriteLine($"{DateTime.Now} :: Logger.Log() :: {LogTypes.Error} :: {ex.Message} "); }
    }

    public void Log(string message, string methodName = "", LogTypes logTypes = LogTypes.Error)
    {
        try
        {
            var logMessage = $"{DateTime.Now} :: {methodName} :: {logTypes} :: {message} ";
            Debug.WriteLine($"{logMessage}");

            using var sw = new StreamWriter(_filePath, true);
            sw.WriteLine(logMessage);
        }
        catch (Exception ex) { Debug.WriteLine($"{DateTime.Now} :: Logger.Log() :: {LogTypes.Error} :: {ex.Message} "); }
    }


}


