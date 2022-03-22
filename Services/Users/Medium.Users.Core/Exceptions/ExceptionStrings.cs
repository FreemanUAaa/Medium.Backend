namespace Medium.Users.Core.Exceptions
{
    public static class ExceptionStrings
    {
        public static string FileExtensionNotSupported => "File extension not supported";

        public static string OccupiedEmail => "This email is occupied by another user";

        public static string FailedLogIn => "Failed to log in, incorrect data";

        public static string FailedUploadPhoto => "Failed to upload photo";

        public static string FailedDeletePhoto => "Failed to delete photo";

        public static string UserNotFound => "The user was not found";

        public static string NotFound => "Page not found";
    }
}
