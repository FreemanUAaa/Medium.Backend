using System;

namespace Medium.Users.Core.Redis
{
    public static class RedisKeys
    {
        public static string GetUserDetailsKey(Guid id) => $"user-details:{id}";

        public static string GetUserPhotoKey(Guid id) => $"user-photo:{id}";

        public static string GetUserBioKey(Guid id) => $"user-bio:{id}";
    }
}
