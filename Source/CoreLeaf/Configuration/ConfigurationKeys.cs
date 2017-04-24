namespace CoreLeaf.Configuration
{
    public static class ConfigurationKeys
    {
        //items contained in appSettings.json
        public static string DefaultHttpTimeout { get { return nameof(DefaultHttpTimeout); } }
        public static string NissanBaseUri { get { return nameof(NissanBaseUri); } }
        public static string CountryRoute { get { return nameof(CountryRoute); } }
        public static string InitialAppRoute { get { return nameof(InitialAppRoute); } }
        public static string LoginRoute { get { return nameof(LoginRoute); } }


        //items contained in appSecrets.json
        public static string NissanApiKey { get { return nameof(NissanApiKey); } }
        public static string UserName { get { return nameof(UserName); } }
        public static string Password { get { return nameof(Password); } }
    }
}
