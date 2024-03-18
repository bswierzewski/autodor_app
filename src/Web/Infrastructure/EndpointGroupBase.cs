namespace Web.Infrastructure;

public abstract class EndpointGroupBase
{
    public virtual string CustomPath { get; } = "";
    public abstract void Map(WebApplication app);
}
