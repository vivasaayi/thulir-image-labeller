namespace ImageLabeller.Dals
{
    public class PostgresConfig
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DataBase { get; set; }

        public PostgresConfig(string host, string userName, string password, string dataBase)
        {
            Host = host;
            UserName = userName;
            Password = password;
            DataBase = dataBase;
        }

        public string GetConnectionString()
        {
            return string.Format("Host={0};Username={1};Password={2};Database={3}", Host, UserName, Password, DataBase);
        }
    }
}