using System;

namespace Medium.Users.Core.Models
{
    public class BioPhoto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FileName { get; set; }
    }
}
