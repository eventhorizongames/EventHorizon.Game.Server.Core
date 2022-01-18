namespace EventHorizon.Platform.State;

using System.Collections.Concurrent;
using System.Collections.Generic;

using EventHorizon.Platform.Api;

public class InMemoryPlatformAnalytics : PlatformAnalytics
{
    public IDictionary<string, string> Analytics { get; } =
        new ConcurrentDictionary<string, string>();

    public void Remove(string key)
    {
        Analytics.Remove(key);
    }

    public void Set(string key, string value)
    {
        Analytics[key] = value;
    }
}
