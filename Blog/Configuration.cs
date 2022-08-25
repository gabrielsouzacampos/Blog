namespace Blog;
public static class Configuration
{
    public static string JwtKey = "ZmVkYWY3ZDg4NjNiNDhlMTk3YjkyODdkNDkyYjcwOGU=";
    public static string ApiKeyName = "api_key";
    public static string ApiKey = "api_FMJSnrRJjF0f/WkV0Fv1ipG92nlq9Z9B1IDRHAmdBHk=";

    public static SmtpConfiguration Smtp { get; set; } = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}