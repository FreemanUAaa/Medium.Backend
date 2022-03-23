using System.IO;

namespace Medium.Users.Tests.Assets
{
    public static class Photo
    {
        public static byte[] Bytes { get; set; }

        static Photo()
        {
            Bytes = File.ReadAllBytes(@"D:\sharp\Medium\Medium\Services\Users\Medium.Users.Tests\Assets\photo.jpg");
        }
    }
}
