using System;

namespace Medium.Drafts.Core.Redis
{
    public static class RedisKeys
    {
        public static string GetDraftDetailsKey(Guid draftId) => $"draft-details:{draftId}";

        public static string GetDraftListKey(Guid userId) => $"draft-list:{userId}";
    }
}
