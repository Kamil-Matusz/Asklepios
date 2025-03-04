namespace Asklepios.Infrastructure.Redis;

internal sealed class RedisOptions
{
    public string ConnectionString { get; set; }
    public string InstanceName { get; set; }
}