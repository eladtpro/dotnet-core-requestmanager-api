namespace RequestManager.Directory
{
    public class LdapConnectionSettings
    {
        public string User { get; set; }
        public string Password { get; set; }

        public static LdapConnectionSettings Current = null;
    }
}
