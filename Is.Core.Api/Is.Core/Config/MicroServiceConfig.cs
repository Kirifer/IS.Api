using Is.Core.Config.Database;

namespace Is.Core.Config
{
    public class MicroServiceConfig : IMicroServiceConfig
    {
        public DatabaseConfig? DatabaseConfig { get; set; }
    }
}
