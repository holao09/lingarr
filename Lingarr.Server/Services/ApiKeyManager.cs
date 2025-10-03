using System.Collections.Concurrent;

namespace Lingarr.Server.Services;

public static class ApiKeyManager
{
    private static readonly ConcurrentDictionary<string, int> _keyIndexes = new();

    public static string? GetNextApiKey(string? apiKeyString)
    {
        if (string.IsNullOrWhiteSpace(apiKeyString))
        {
            return null;
        }

        var keys = apiKeyString.Split('|', StringSplitOptions.RemoveEmptyEntries);
        if (keys.Length == 0)
        {
            return null;
        }

        var index = _keyIndexes.AddOrUpdate(apiKeyString, 0, (_, oldIndex) => (oldIndex + 1) % keys.Length);
        return keys[index].Trim();
    }
}