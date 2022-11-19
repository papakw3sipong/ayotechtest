namespace aYoTechTest.BR.Classes
{
    public class ApiSetting
    {
        public string TokenSecret { get; set; }
        public string TokenIssuer { get; set; }
        public string TokenAudience { get; set; }
        public string AllowedUserNameChars { get; set; }
        public int PasswordLength { get; set; }
        public int LockoutTimeInMinutes { get; set; }
        public int AllowdFailedAttempts { get; set; }
    }
}
