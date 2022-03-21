namespace Medium.Users.Core.Exceptions
{
    public static class ExceptionStrings
    {
        public static string OccupiedEmail => "This email is occupied by another user";

        public static string FailedLogIn => "Failed to log in, incorrect data"; 

        public static string UserNotFound => "The user was not found";
    }
}
