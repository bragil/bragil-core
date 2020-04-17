namespace Bragil.Core.Interfaces
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}
