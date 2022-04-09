namespace Medium.Users.Core.Common.Passwords
{
    public class PasswordHashResponse
    {
        public string Hash { get; set; }

        public byte[] Salt { get; set; }
    }
}
