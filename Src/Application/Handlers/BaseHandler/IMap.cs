namespace Application.Handlers.BaseHandler
{
    public interface IMap
    {
        TDest Map<TDest>(object source);
    }
}
