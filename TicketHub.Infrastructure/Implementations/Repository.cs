using Microsoft.EntityFrameworkCore;
using TicketHub.Application.Abstractions;
using TicketHub.Application.Exceptions;
using TicketHub.Infrastructure.Databases.MSSQL.DbContexts;

namespace TicketHub.Infrastructure.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly TicketHubDbContext _ticketHubDbContext;

        public Repository(TicketHubDbContext ticketHubDbContext)
        {
            _ticketHubDbContext = ticketHubDbContext;
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _ticketHubDbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _ticketHubDbContext.Set<TEntity>().FindAsync(id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException($"Entity with id {id} not found.");
            }
            _ticketHubDbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _ticketHubDbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _ticketHubDbContext.Set<TEntity>().FindAsync(id, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _ticketHubDbContext.Set<TEntity>().Update(entity);
        }
    }
}
