namespace EventHorizon.Platform.Api;

using System.Collections.Generic;

public interface PlatformAnalytics
{
    IDictionary<string, string> Analytics { get; }
    void Set(string key, string value);
    void Remove(string key);
}
