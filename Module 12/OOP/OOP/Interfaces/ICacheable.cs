using Microsoft.Extensions.Caching.Memory;

namespace OOP.Interfaces;

internal interface ICacheable
{
    public MemoryCacheEntryOptions CacheOption { get; }
}
