namespace Web.Infrastructure;

public abstract class EndpointGroupBase
{
    public virtual string RoutePrefix { get; } = "/api";
    public abstract void Map(WebApplication app);
}
