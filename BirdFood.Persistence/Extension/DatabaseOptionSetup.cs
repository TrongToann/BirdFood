using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BirdFood.Persistence.Extension
{
    public class DatabaseOptionSetup : IConfigureOptions<DatabaseOptions>
    {
        public readonly IConfiguration _configuration;
        private const string ConfigurationSectionName = "DatabaseOptions";
        private const string ConnectionString = "ShopShopConnect";
        public DatabaseOptionSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(DatabaseOptions options)
        {
            var connectionString = _configuration.GetConnectionString(ConnectionString);
            options.ConnectionString = connectionString;
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}
