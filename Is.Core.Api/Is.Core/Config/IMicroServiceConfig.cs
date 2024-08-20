using Is.Core.Config.Database;

namespace Is.Core.Config
{
    public interface IMicroServiceConfig
    {
        DatabaseConfig? DatabaseConfig { get; set; }
        JwtConfig? JwtConfig { get; set; }
    }
}
