using Microsoft.Extensions.Configuration;

namespace Persistance.Data.Configuration
{
    public class ConfigSettings
    {
        public string DbConnection { get; set; }
    }

    public class Configurator : IConfigurator
    {
        private string[] args;

        //private string _dbConnection = @"Data Source=.\SQLEXPRESS;Persist Security Info=True;User ID=system;Password=zwarteridder";

        private string _dbConnection = @"";

        public string DbConnection
        {
            get => _dbConnection;
            set
            {
                if (_dbConnection != value)
                {
                    _dbConnection = value;
                }
            }
        }

        public Configurator(string[] args)
        {
            this.args = args;

            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json",
                optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var config = new ConfigSettings();
            Configuration.Bind("ConfigSettings", config);
            DbConnection = config.DbConnection;
        }
    }
}