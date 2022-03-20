namespace Medium.Users.Core.Common.Password
{
    public class PasswordHashResponse
    {
        public string Hash { get; set; }

        public byte[] Salt { get; set; }
    }
}
