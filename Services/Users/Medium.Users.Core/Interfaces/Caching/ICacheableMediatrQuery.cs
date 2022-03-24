using System;

namespace Medium.Users.Core.Interfaces.Caching
{
    public interface ICacheableMediatrQuery
    {
        string CacheKey { get; }
    }
}
