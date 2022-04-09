using System;

namespace Medium.Drafts.Core.Common.ReadTime
{
    public static class ReadTime
    {
        public static TimeSpan Get(int wordCount)
        {
            return new TimeSpan(0, wordCount / 150, 0);
        }
    }
}
