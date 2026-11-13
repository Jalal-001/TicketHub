namespace TicketHub.Application.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        void Update(TEntity entity);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
