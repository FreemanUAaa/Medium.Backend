using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Medium.Users.Core.Common.Password
{
    public static class PasswordHasher
    {
        public static int SaltSize => 128 / 8;

        public static int IterationCount => 10;

        public static int NumBytesRequested => 256 / 8;

        public static PasswordHashResponse HashPassword(string password, byte[] salt)
        {
            if (salt == null)
            {
                using RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
                rngCsp.GetNonZeroBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password, salt: salt, prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: IterationCount, numBytesRequested: NumBytesRequested));

            return new PasswordHashResponse() { Hash = hashed, Salt = salt };
        }
    }
}
