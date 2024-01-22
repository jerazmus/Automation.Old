namespace Automation.TestApp.Core.Utilities
{
    public static class UserProvider
    {
        // Class that provides all of the application users based on pre-created User objects
        
        public static User StandardUser 
            => new ("standard_user");
        public static User LockedOutUser
            => new ("locked_out_user");
        public static User ProblemUser
            => new("problem_user");
        public static User PerformanceGlitchUser
             => new("performance_glitch_user");

        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }

            public User(string username)
            {
                Username = username;
                Password = "secret_sauce";
            }
        }
    }
}
