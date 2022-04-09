using System;

namespace Medium.Drafts.Core.Models
{
    public class Draft
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Body { get; set; }
        
        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime LastEditDate { get; set; }

        public TimeSpan ReadTime { get; set; }

        public int WordCount { get; set; }
    }
}
