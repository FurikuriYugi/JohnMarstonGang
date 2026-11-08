<!-- Test lines 1251-1292 of 1292 -->
```csharp
    {
        if (_promptErrorLogged)
        {
            return;
        }

        _promptErrorLogged = true;
        Log("PROMPT ERROR: " + message);
    }

    private void LogErrorOnce(string key, string message)
    {
        if (_loggedErrors.Add(key))
        {
            Log(message);
        }
    }

    private void Log(string message)
    {
        string line =
            DateTime.Now.ToString(
                "yyyy-MM-dd HH:mm:ss.fff",
                CultureInfo.InvariantCulture) +
            " " + message +
            Environment.NewLine;

        File.AppendAllText(_logPath, line);
    }

    private string GetInnermostMessage(Exception exception)
    {
        Exception current = exception;

        while (current.InnerException != null)
        {
            current = current.InnerException;
        }

        return current.Message;
    }
}
```
